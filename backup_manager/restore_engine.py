"""
restore_engine.py
Restaura los archivos de un snapshot a la ruta que elija el usuario.
También permite restaurar un solo archivo (usado por el verificador
para "reparar" un archivo corrupto sin restaurar todo).
"""

import zlib
from pathlib import Path


class RestoreEngine:
    def __init__(self, db):
        self.db = db

    def restore_snapshot(self, snapshot_id, dest_dir, progress_callback=None, cancel_event=None):
        dest_dir = Path(dest_dir)
        dest_dir.mkdir(parents=True, exist_ok=True)

        versions = self.db.get_file_versions(snapshot_id)
        total = len(versions)

        for i, row in enumerate(versions, start=1):
            if cancel_event and cancel_event.is_set():
                raise RuntimeError("Restauracion cancelada por el usuario.")
            if progress_callback:
                progress_callback(i, total, row["relative_path"])
            self._restore_single(row["archive_location"], dest_dir / row["relative_path"])

        return total

    def restore_single_file(self, snapshot_id, relative_path, dest_dir):
        """Restaura un único archivo (por su ruta relativa dentro del snapshot)."""
        versions = self.db.get_file_versions(snapshot_id)
        for row in versions:
            if row["relative_path"] == relative_path:
                self._restore_single(row["archive_location"], Path(dest_dir) / relative_path)
                return True
        return False

    def restore_files(self, snapshot_id, relative_paths, dest_dir):
        """Restaura varios archivos del snapshot en la ruta indicada."""
        dest_dir = Path(dest_dir)
        dest_dir.mkdir(parents=True, exist_ok=True)

        versions = self.db.get_file_versions(snapshot_id)
        lookup = {row["relative_path"]: row for row in versions}
        restored = []

        for rel_path in relative_paths:
            row = lookup.get(rel_path)
            if row is None:
                continue
            self._restore_single(row["archive_location"], dest_dir / rel_path)
            restored.append(rel_path)

        return restored

    def _restore_single(self, archive_location, dest_path):
        dest_path.parent.mkdir(parents=True, exist_ok=True)
        with open(archive_location, "rb") as f_in:
            compressed = f_in.read()
        data = zlib.decompress(compressed)
        with open(dest_path, "wb") as f_out:
            f_out.write(data)
