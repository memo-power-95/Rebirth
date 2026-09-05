import zlib

import pytest

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


def test_restore_rejects_paths_outside_destination(tmp_path):
    archive = tmp_path / "file.bin"
    archive.write_bytes(zlib.compress(b"must stay inside"))
    rows = [{"relative_path": "../outside.txt", "archive_location": str(archive)}]

    engine = RestoreEngine(FakeDB(rows))
    with pytest.raises(ValueError):
        engine.restore_files(1, ["../outside.txt"], tmp_path / "restore")


def test_restore_does_not_replace_file_when_archive_is_corrupt(tmp_path):
    archive = tmp_path / "broken.bin"
    archive.write_bytes(b"not compressed data")
    destination = tmp_path / "restore" / "file.txt"
    destination.parent.mkdir()
    destination.write_bytes(b"original")
    rows = [{"relative_path": "file.txt", "archive_location": str(archive)}]

    engine = RestoreEngine(FakeDB(rows))
    with pytest.raises(zlib.error):
        engine.restore_files(1, ["file.txt"], tmp_path / "restore")

    assert destination.read_bytes() == b"original"


def test_restore_preserves_unicode_paths(tmp_path):
    archive = tmp_path / "unicode.bin"
    archive.write_bytes(zlib.compress("contenido de prueba".encode("utf-8")))
    relative_path = "資料/로그/archivo.txt"
    rows = [{"relative_path": relative_path, "archive_location": str(archive)}]

    restored = RestoreEngine(FakeDB(rows)).restore_files(1, [relative_path], tmp_path / "restore")

    assert restored == [relative_path]
    assert (tmp_path / "restore" / "資料" / "로그" / "archivo.txt").read_text(encoding="utf-8") == "contenido de prueba"
