from backup_manager.hasher import hash_file
from backup_manager.verifier import FALTA, MODIFICADO, OK, Verifier


class FakeDB:
    def __init__(self, rows):
        self.rows = rows

    def get_file_versions(self, snapshot_id):
        return self.rows


def test_verify_distinguishes_modified_and_missing_files(tmp_path):
    unchanged = tmp_path / "unchanged.txt"
    modified = tmp_path / "modified.txt"
    unchanged.write_text("same", encoding="utf-8")
    modified.write_text("new content", encoding="utf-8")

    rows = [
        {
            "relative_path": "unchanged.txt",
            "hash": hash_file(unchanged),
            "size": unchanged.stat().st_size,
            "mtime": unchanged.stat().st_mtime,
        },
        {
            "relative_path": "modified.txt",
            "hash": "0" * 64,
            "size": 1,
            "mtime": 0,
        },
        {
            "relative_path": "missing.txt",
            "hash": "1" * 64,
            "size": 1,
            "mtime": 0,
        },
    ]

    results = Verifier(FakeDB(rows)).verify(tmp_path, 1)
    statuses = {result["relative_path"]: result["status"] for result in results}

    assert statuses == {
        "unchanged.txt": OK,
        "modified.txt": MODIFICADO,
        "missing.txt": FALTA,
    }