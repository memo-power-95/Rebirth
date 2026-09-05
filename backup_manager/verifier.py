"""
verifier.py
Compara los archivos reales en disco contra lo registrado en un
snapshot, SIN descomprimir los backups. Solo recalcula el hash si
el tamaño o la fecha de modificación cambiaron (rápido).
"""

import zlib
import hashlib
from pathlib import Path
from hasher import hash_file

OK = "OK"
MODIFICADO = "MODIFICADO"
CORRUPTO = "CORRUPTO"
FALTA = "FALTA"


class Verifier:
    def __init__(self, db):
        self.db = db

    def verify(self, source_path, snapshot_id, progress_callback=None, cancel_event=None):
        """
        Devuelve una lista de dicts: {relative_path, status}
        status es uno de: OK, MODIFICADO, CORRUPTO, FALTA
        """
        source_path = Path(source_path)
        versions = self.db.get_file_versions(snapshot_id)
        total = len(versions)
        results = []

        for i, row in enumerate(versions, start=1):
            if cancel_event and cancel_event.is_set():
                raise RuntimeError("Verificacion cancelada por el usuario.")
            rel_path = row["relative_path"]
            if progress_callback:
                progress_callback(i, total, rel_path)

            real_path = source_path / rel_path if source_path.is_dir() else source_path

            if not real_path.exists():
                results.append({"relative_path": rel_path, "status": FALTA})
                continue

            stat = real_path.stat()
            # Atajo rápido: si tamaño y mtime coinciden, asumimos OK sin hashear
            if stat.st_size == row["size"] and abs(stat.st_mtime - row["mtime"]) < 1:
                results.append({"relative_path": rel_path, "status": OK})
                continue

            # Una diferencia en el archivo original es un cambio del usuario,
            # no corrupción del backup. La corrupción del archivo comprimido
            # se comprueba en verify_snapshot_archive().
            real_hash = hash_file(real_path)
            if real_hash == row["hash"]:
                results.append({"relative_path": rel_path, "status": OK})
            else:
                results.append({"relative_path": rel_path, "status": MODIFICADO})

        return results

    def verify_snapshot_archive(self, snapshot_id, progress_callback=None, cancel_event=None):
        """Comprueba que cada archivo comprimido del snapshot se pueda leer."""
        versions = self.db.get_file_versions(snapshot_id)
        results = []
        total = len(versions)
        for i, row in enumerate(versions, start=1):
            if cancel_event and cancel_event.is_set():
                raise RuntimeError("Verificacion cancelada por el usuario.")
            if progress_callback:
                progress_callback(i, total, row["relative_path"])
            try:
                with open(row["archive_location"], "rb") as archive:
                    data = zlib.decompress(archive.read())
                status = OK if hashlib.sha256(data).hexdigest() == row["hash"] else CORRUPTO
            except (OSError, zlib.error):
                status = CORRUPTO
            results.append({"relative_path": row["relative_path"], "status": status})
        return results
