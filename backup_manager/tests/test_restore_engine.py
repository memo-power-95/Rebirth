import zlib

from backup_manager.restore_engine import RestoreEngine


class FakeDB:
    def __init__(self, rows):
        self.rows = rows

    def get_file_versions(self, snapshot_id):
        return self.rows


def test_restore_files_restores_all_requested_files(tmp_path):
    archive_a = tmp_path / "a.bin"
    archive_b = tmp_path / "b.bin"
    archive_a.write_bytes(zlib.compress(b"hello"))
    archive_b.write_bytes(zlib.compress(b"world"))

    rows = [
        {"relative_path": "folder/a.txt", "archive_location": str(archive_a)},
        {"relative_path": "folder/b.txt", "archive_location": str(archive_b)},
    ]

    engine = RestoreEngine(FakeDB(rows))
    restored = engine.restore_files(1, ["folder/a.txt", "folder/b.txt"], tmp_path / "restore")

    assert restored == ["folder/a.txt", "folder/b.txt"]
    assert (tmp_path / "restore" / "folder" / "a.txt").read_bytes() == b"hello"
    assert (tmp_path / "restore" / "folder" / "b.txt").read_bytes() == b"world"
