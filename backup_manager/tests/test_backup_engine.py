from backup_manager.backup_engine import BackupEngine


class FakeDB:
    def __init__(self, target):
        self.target = target
        self.snapshot_id = 1
        self.versions = []
        self.files = {}

    def get_target(self, target_id):
        return self.target

    def create_snapshot(self, target_id, note):
        return self.snapshot_id

    def get_file_by_hash(self, file_hash):
        return self.files.get(file_hash)

    def register_file(self, file_hash, size, archive_location):
        self.files[file_hash] = {"archive_location": archive_location}

    def add_file_version(self, snapshot_id, relative_path, file_hash, mtime):
        self.versions.append(relative_path)

    def delete_snapshot(self, snapshot_id):
        self.versions.clear()


def test_backup_excludes_matching_patterns(tmp_path):
    source = tmp_path / "source"
    source.mkdir()
    (source / "important.txt").write_text("keep", encoding="utf-8")
    (source / "activity.log").write_text("ignore", encoding="utf-8")
    (source / "cache").mkdir()
    (source / "cache" / "data.bin").write_bytes(b"ignore")
    db = FakeDB({"source_path": str(source)})

    snapshot_id = BackupEngine(db, tmp_path / "storage").backup_target(
        1, exclude_patterns=["*.log", "cache/*"]
    )

    assert snapshot_id == 1
    assert db.versions == ["important.txt"]