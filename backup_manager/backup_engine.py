"""
backup_engine.py
Recorre un target (carpeta o archivo), calcula hashes, deduplica
contra la base de datos y comprime solo el contenido nuevo.
"""

import hashlib
import os
import tempfile
import time
import zlib
from pathlib import Path
from fnmatch import fnmatch

class BackupEngine:
    def __init__(self, db, storage_dir="storage"):
        self.db = db
        self.storage_dir = Path(storage_dir)
        self.storage_dir.mkdir(parents=True, exist_ok=True)

    def _archive_path_for_hash(self, file_hash):
        sub = self.storage_dir / file_hash[:2]
        sub.mkdir(exist_ok=True)
        return sub / f"{file_hash}.bin"

    def _iter_files(self, source_path: Path, exclude_patterns=()):
        if source_path.is_file():
            if not self._is_excluded(source_path.name, exclude_patterns):
                yield source_path, source_path.name
        else:
            for p in source_path.rglob("*"):
                if p.is_file():
                    relative_path = str(p.relative_to(source_path)).replace("\\", "/")
                    if not self._is_excluded(relative_path, exclude_patterns):
                        yield p, relative_path

    def backup_target(self, target_id, note="", progress_callback=None, cancel_event=None,
                      exclude_patterns=()):
        """
        Crea un snapshot nuevo para target_id.
        progress_callback(current, total, filename) se llama por cada archivo, si se provee.
        Devuelve snapshot_id.
        """
        target = self.db.get_target(target_id)
        if target is None:
            raise ValueError("Objetivo de backup no encontrado.")
        source_path = Path(target["source_path"])
        if not source_path.is_file() and not source_path.is_dir():
            raise FileNotFoundError(f"La ruta del objetivo no existe: {source_path}")

        files = list(self._iter_files(source_path, exclude_patterns))
        total = len(files)
        snapshot_id = self.db.create_snapshot(target_id, note)

        try:
            for i, (full_path, rel_path) in enumerate(files, start=1):
                if cancel_event and cancel_event.is_set():
                    raise RuntimeError("Backup cancelado por el usuario.")
                if progress_callback:
                    progress_callback(i, total, rel_path)

                data, file_hash, size, mtime = self._read_stable_file(full_path, cancel_event=cancel_event)

                existing = self.db.get_file_by_hash(file_hash)
                if not existing:
                    # Escribe en un temporal y publica el archivo solo al terminar.
                    archive_path = self._archive_path_for_hash(file_hash)
                    compressed = zlib.compress(data, level=6)
                    with tempfile.NamedTemporaryFile(
                            mode="wb", dir=archive_path.parent, delete=False) as temp_file:
                        temp_path = Path(temp_file.name)
                        temp_file.write(compressed)
                        temp_file.flush()
                        os.fsync(temp_file.fileno())
                    try:
                        os.replace(temp_path, archive_path)
                    except Exception:
                        temp_path.unlink(missing_ok=True)
                        raise
                    self.db.register_file(file_hash, size, str(archive_path))

                # Ya sea nuevo o existente, se registra la versión en este snapshot
                self.db.add_file_version(snapshot_id, rel_path, file_hash, mtime)
        except Exception:
            self.db.delete_snapshot(snapshot_id)
            raise

        return snapshot_id

    @staticmethod
    def _read_stable_file(file_path, attempts=3, retry_delay=0.2, cancel_event=None):
        last_error = None
        for attempt in range(attempts):
            try:
                before = file_path.stat()
                data = file_path.read_bytes()
                after = file_path.stat()
                if before.st_size == after.st_size and before.st_mtime_ns == after.st_mtime_ns:
                    file_hash = hashlib.sha256(data).hexdigest()
                    return data, file_hash, after.st_size, after.st_mtime
            except OSError as error:
                last_error = error
            if cancel_event and cancel_event.is_set():
                raise RuntimeError("Backup cancelado por el usuario.")
            if attempt + 1 < attempts:
                time.sleep(retry_delay)
        if last_error:
            raise last_error
        raise RuntimeError(f"El archivo cambia mientras se lee: {file_path}")

    @staticmethod
    def _is_excluded(relative_path, exclude_patterns):
        normalized_path = relative_path.replace("\\", "/")
        file_name = Path(normalized_path).name
        return any(
            fnmatch(normalized_path, pattern) or fnmatch(file_name, pattern)
            for pattern in exclude_patterns
            if pattern
        )
