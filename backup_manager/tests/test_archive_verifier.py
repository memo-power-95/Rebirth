import hashlib
import zlib

from backup_manager.verifier import CORRUPTO, OK, Verifier


class FakeDB:
    def __init__(self, rows):
        self.rows = rows

    def get_file_versions(self, snapshot_id):
        return self.rows


def test_archive_verification_checks_content_hash(tmp_path):
    archive = tmp_path / "archive.bin"
    archive.write_bytes(zlib.compress(b"different content"))
    rows = [{
        "relative_path": "file.txt",
        "archive_location": str(archive),
        "hash": hashlib.sha256(b"expected content").hexdigest(),
    }]

    results = Verifier(FakeDB(rows)).verify_snapshot_archive(1)

    assert results == [{"relative_path": "file.txt", "status": CORRUPTO}]


def test_archive_verification_accepts_matching_content_hash(tmp_path):
    data = b"expected content"
    archive = tmp_path / "archive.bin"
    archive.write_bytes(zlib.compress(data))
    rows = [{
        "relative_path": "file.txt",
        "archive_location": str(archive),
        "hash": hashlib.sha256(data).hexdigest(),
    }]

    results = Verifier(FakeDB(rows)).verify_snapshot_archive(1)

    assert results == [{"relative_path": "file.txt", "status": OK}]