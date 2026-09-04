"""
backup_engine.py
Recorre un target (carpeta o archivo), calcula hashes, deduplica
contra la base de datos y comprime solo el contenido nuevo.

MEJORADO:
- Clasificación automática de archivos críticos vs dinámicos
- Threading pool para procesar múltiples archivos en paralelo
- Retry con backoff exponencial para fallos temporales
- Compresión adaptativa según tamaño de archivo
- Manejo robusto de errores en alta demanda
"""

import zlib
import time
import logging
from pathlib import Path
from concurrent.futures import ThreadPoolExecutor, as_completed
from threading import Lock

from hasher import hash_file

logger = logging.getLogger(__name__)


class BackupEngine:
    # Patrones para clasificar archivos dinámicos vs críticos
    DYNAMIC_PATTERNS = {
        '.log', '.tmp', '.cache', '.pid', '.lock',
        '.temp', '.bak', '.swp', '.~',
    }
    DYNAMIC_DIRS = {'logs', 'tmp', 'temp', 'cache', '__pycache__', '.cache', '.tmp'}
    
    CRITICAL_EXTENSIONS = {
        '.exe', '.dll', '.so', '.dylib', '.conf', '.cfg',
        '.json', '.yaml', '.yml', '.xml', '.ini', '.config',
        '.dat', '.db', '.sqlite', '.mdb', '.key', '.pem',
    }

    def __init__(self, db, storage_dir="storage", max_workers=4):
        self.db = db
        self.storage_dir = Path(storage_dir)
        self.storage_dir.mkdir(exist_ok=True)
        self.max_workers = max_workers
        self._lock = Lock()
        self.compression_stats = {"total": 0, "compressed": 0, "saved_bytes": 0}

    def _archive_path_for_hash(self, file_hash):
        sub = self.storage_dir / file_hash[:2]
        sub.mkdir(exist_ok=True)
        return sub / f"{file_hash}.bin"

    def _iter_files(self, source_path: Path):
        """Itera sobre archivos, recursivamente si es carpeta"""
        if source_path.is_file():
            yield source_path, source_path.name
        else:
            for p in source_path.rglob("*"):
                if p.is_file():
                    yield p, str(p.relative_to(source_path))

    def _classify_file(self, relative_path: str) -> bool:
        """
        Clasifica un archivo como dinámico (True) o crítico (False).
        Un archivo es dinámico si:
        - Su extensión está en DYNAMIC_PATTERNS
        - Está en un directorio dinámico
        - No tiene extensión crítica
        """
        path = Path(relative_path)
        
        # Revisar si está en directorio dinámico
        for part in path.parts:
            if part.lower() in self.DYNAMIC_DIRS:
                return True
        
        # Revisar extensión
        suffix = path.suffix.lower()
        if suffix in self.DYNAMIC_PATTERNS:
            return True
        
        # Si tiene extensión crítica, NO es dinámico
        if suffix in self.CRITICAL_EXTENSIONS:
            return False
        
        # Por defecto, archivos sin extensión o desconocidos son críticos
        return False

    def _get_compression_level(self, file_size: int) -> int:
        """
        Ajusta nivel de compresión según tamaño:
        - <1MB: nivel 9 (máximo)
        - 1-10MB: nivel 6 (balanceado)
        - >10MB: nivel 1 (rápido, menos CPU)
        """
        if file_size < 1_000_000:
            return 9
        elif file_size < 10_000_000:
            return 6
        else:
            return 1

    def _compress_with_retry(self, file_path: Path, max_retries=3) -> bytes:
        """
        Comprime un archivo con retry automático en caso de fallos.
        Usa backoff exponencial: 1s, 2s, 4s
        """
        for attempt in range(max_retries):
            try:
                with open(file_path, "rb") as f:
                    data = f.read()
                
                compression_level = self._get_compression_level(len(data))
                compressed = zlib.compress(data, level=compression_level)
                
                # Estadísticas
                with self._lock:
                    self.compression_stats["total"] += 1
                    self.compression_stats["compressed"] += len(data)
                    self.compression_stats["saved_bytes"] += len(data) - len(compressed)
                
                return compressed
            
            except (IOError, OSError) as e:
                if attempt < max_retries - 1:
                    wait_time = 2 ** attempt  # 1, 2, 4 segundos
                    logger.warning(f"Retry {attempt + 1}/{max_retries} para {file_path}: {e}. Esperando {wait_time}s...")
                    time.sleep(wait_time)
                else:
                    logger.error(f"Fallo permanente comprimiendo {file_path} después de {max_retries} intentos: {e}")
                    raise RuntimeError(f"No se pudo comprimir {file_path}: {e}")

    def _process_single_file(self, full_path: Path, rel_path: str, snapshot_id: int):
        """
        Procesa un archivo individual: hashea, deduplica, comprime.
        Retorna (rel_path, is_dynamic, success) o (rel_path, None, False) si falla.
        """
        try:
            file_hash = hash_file(full_path)
            mtime = full_path.stat().st_mtime
            size = full_path.stat().st_size
            is_dynamic = self._classify_file(rel_path)

            existing = self.db.get_file_by_hash(file_hash)
            if not existing:
                # Contenido nuevo: comprimir y guardar en storage/
                archive_path = self._archive_path_for_hash(file_hash)
                if not archive_path.exists():  # Evitar duplicados en almacenamiento
                    compressed = self._compress_with_retry(full_path)
                    with open(archive_path, "wb") as f_out:
                        f_out.write(compressed)
                
                self.db.register_file(file_hash, size, str(archive_path))

            # Registrar versión (con clasificación dinámico/crítico)
            self.db.add_file_version(snapshot_id, rel_path, file_hash, mtime, is_dynamic=is_dynamic)
            
            return (rel_path, is_dynamic, True)
        
        except Exception as e:
            logger.error(f"Error procesando {rel_path}: {e}")
            return (rel_path, None, False)

    def backup_target(self, target_id, note="", progress_callback=None, cancel_event=None):
        """
        Crea un snapshot nuevo para target_id con procesamiento paralelo.
        progress_callback(current, total, filename, status) se llama por cada archivo.
        Devuelve (snapshot_id, stats_dict).
        """
        target = self.db.get_target(target_id)
        source_path = Path(target["source_path"])
        snapshot_id = self.db.create_snapshot(target_id, note)

        files = list(self._iter_files(source_path))
        total = len(files)
        
        if total == 0:
            return snapshot_id, {"total": 0, "critical": 0, "dynamic": 0, "errors": 0}

        processed = 0
        errors = 0
        critical_count = 0
        dynamic_count = 0

        # Pool de threads para procesamiento paralelo
        with ThreadPoolExecutor(max_workers=self.max_workers) as executor:
            # Enviar todos los trabajos
            futures = {
                executor.submit(self._process_single_file, full_path, rel_path, snapshot_id): rel_path
                for full_path, rel_path in files
            }

            # Procesar resultados conforme se completen
            for future in as_completed(futures):
                if cancel_event and cancel_event.is_set():
                    executor.shutdown(wait=False)
                    raise RuntimeError("Backup cancelado por el usuario.")
                
                rel_path, is_dynamic, success = future.result()
                processed += 1
                
                if success:
                    if is_dynamic:
                        dynamic_count += 1
                    else:
                        critical_count += 1
                else:
                    errors += 1

                if progress_callback:
                    status = "OK" if success else "ERROR"
                    progress_callback(processed, total, rel_path, status)

        stats = {
            "total": total,
            "critical": critical_count,
            "dynamic": dynamic_count,
            "errors": errors,
            "compression": self.compression_stats.copy()
        }
        
        logger.info(f"Backup completado: {total} archivos ({critical_count} críticos, {dynamic_count} dinámicos, {errors} errores)")
        
        return snapshot_id, stats
