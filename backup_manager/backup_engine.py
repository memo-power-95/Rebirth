"""
backup_engine.py
Recorre un target (carpeta o archivo), calcula hashes, deduplica
contra la base de datos y comprime solo el contenido nuevo.
"""

import zlib
from pathlib import Path

from hasher import hash_file


class BackupEngine:
    def __init__(self, db, storage_dir="storage"):
        self.db = db
        self.storage_dir = Path(storage_dir)
        self.storage_dir.mkdir(exist_ok=True)

    def _archive_path_for_hash(self, file_hash):
        sub = self.storage_dir / file_hash[:2]
        sub.mkdir(exist_ok=True)
        return sub / f"{file_hash}.bin"

    def _iter_files(self, source_path: Path):
        if source_path.is_file():
            yield source_path, source_path.name
        else:
            for p in source_path.rglob("*"):
                if p.is_file():
                    yield p, str(p.relative_to(source_path))

    def backup_target(self, target_id, note="", progress_callback=None, cancel_event=None):
        """
        Crea un snapshot nuevo para target_id.
        progress_callback(current, total, filename) se llama por cada archivo, si se provee.
        Devuelve snapshot_id.
        """
        target = self.db.get_target(target_id)
        source_path = Path(target["source_path"])
        snapshot_id = self.db.create_snapshot(target_id, note)

        files = list(self._iter_files(source_path))
        total = len(files)

        for i, (full_path, rel_path) in enumerate(files, start=1):
            if cancel_event and cancel_event.is_set():
                raise RuntimeError("Backup cancelado por el usuario.")
            if progress_callback:
                progress_callback(i, total, rel_path)

            file_hash = hash_file(full_path)
            mtime = full_path.stat().st_mtime
            size = full_path.stat().st_size

            existing = self.db.get_file_by_hash(file_hash)
            if not existing:
                # Contenido nuevo: comprimir y guardar en storage/
                archive_path = self._archive_path_for_hash(file_hash)
                with open(full_path, "rb") as f_in:
                    data = f_in.read()
                compressed = zlib.compress(data, level=6)
                with open(archive_path, "wb") as f_out:
                    f_out.write(compressed)
                self.db.register_file(file_hash, size, str(archive_path))

            # Ya sea nuevo o existente, se registra la versión en este snapshot
            self.db.add_file_version(snapshot_id, rel_path, file_hash, mtime)

        return snapshot_id
