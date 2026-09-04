"""
database.py
Acceso a la base de datos SQLite. Guarda:
 - targets: carpetas/archivos que el usuario respalda
 - snapshots: cada vez que se hizo un backup de un target
 - files: catálogo de contenido único (deduplicado por hash)
 - file_versions: qué archivo (ruta relativa) tenía qué hash en qué snapshot
 
MEJORADO: Ahora file_versions incluye is_dynamic para clasificar archivos críticos vs dinámicos.
"""

import hashlib
import sqlite3
import threading
import os
from pathlib import Path
from datetime import datetime


class Database:
    def __init__(self, db_path="backups.db"):
        self.db_path = db_path
        self._lock = threading.RLock()
        # SQLite connections are normally bound to the thread that created them.
        # The backup app opens the DB in the main thread and reads/writes it in
        # worker threads, so we allow reuse across threads and serialize access.
        self.conn = sqlite3.connect(db_path, check_same_thread=False, timeout=30.0)
        self.conn.execute("PRAGMA foreign_keys = ON")
        self.conn.execute("PRAGMA journal_mode = WAL")  # Write-Ahead Logging para mejor concurrencia
        self.conn.execute("PRAGMA synchronous = NORMAL")  # Balance entre seguridad y velocidad
        self.conn.row_factory = sqlite3.Row
        self._create_tables()
        self.ensure_default_admin()

    def _create_tables(self):
        cur = self.conn.cursor()
        cur.executescript("""
        CREATE TABLE IF NOT EXISTS targets (
            id INTEGER PRIMARY KEY AUTOINCREMENT,
            name TEXT NOT NULL,
            source_path TEXT NOT NULL,
            is_folder INTEGER NOT NULL,
            created_at TEXT NOT NULL
        );

        CREATE TABLE IF NOT EXISTS snapshots (
            id INTEGER PRIMARY KEY AUTOINCREMENT,
            target_id INTEGER NOT NULL,
            timestamp TEXT NOT NULL,
            note TEXT,
            FOREIGN KEY (target_id) REFERENCES targets(id) ON DELETE CASCADE
        );

        CREATE TABLE IF NOT EXISTS files (
            hash TEXT PRIMARY KEY,
            size INTEGER NOT NULL,
            archive_location TEXT NOT NULL,
            first_seen_at TEXT NOT NULL
        );

        CREATE TABLE IF NOT EXISTS file_versions (
            id INTEGER PRIMARY KEY AUTOINCREMENT,
            snapshot_id INTEGER NOT NULL,
            relative_path TEXT NOT NULL,
            hash TEXT NOT NULL,
            mtime REAL NOT NULL,
            is_dynamic INTEGER NOT NULL DEFAULT 0,
            FOREIGN KEY (snapshot_id) REFERENCES snapshots(id) ON DELETE CASCADE,
            FOREIGN KEY (hash) REFERENCES files(hash)
        );

        CREATE TABLE IF NOT EXISTS users (
            id INTEGER PRIMARY KEY AUTOINCREMENT,
            username TEXT NOT NULL UNIQUE,
            password_hash TEXT NOT NULL,
            role TEXT NOT NULL DEFAULT 'operator',
            is_active INTEGER NOT NULL DEFAULT 1,
            can_backup INTEGER NOT NULL DEFAULT 1,
            can_restore INTEGER NOT NULL DEFAULT 1,
            can_manage_users INTEGER NOT NULL DEFAULT 0,
            can_manage_security INTEGER NOT NULL DEFAULT 0,
            created_at TEXT NOT NULL
        );

        CREATE TABLE IF NOT EXISTS audit_log (
            id INTEGER PRIMARY KEY AUTOINCREMENT,
            user_id INTEGER,
            action TEXT NOT NULL,
            details TEXT,
            created_at TEXT NOT NULL,
            FOREIGN KEY (user_id) REFERENCES users(id)
        );
        """)
        
        # Migración: Agregar columna is_dynamic si no existe
        try:
            cur.execute("ALTER TABLE file_versions ADD COLUMN is_dynamic INTEGER NOT NULL DEFAULT 0")
        except sqlite3.OperationalError:
            # La columna ya existe, no hacer nada
            pass
        
        self.conn.commit()

    @staticmethod
    def _hash_password(password: str) -> str:
        return hashlib.sha256(password.strip().encode("utf-8")).hexdigest()

    def ensure_default_admin(self):
        with self._lock:
            user = self.get_user("admin")
            if user is not None:
                return user
            self.create_user(
                username="admin",
                password="admin123",
                role="admin",
                can_backup=True,
                can_restore=True,
                can_manage_users=True,
                can_manage_security=True,
            )
            self.log_event(None, "system_default_admin_created", "Cuenta administrativa creada por primera vez.")
            return self.get_user("admin")

    def create_user(self, username, password, role="operator", can_backup=True, can_restore=True,
                    can_manage_users=False, can_manage_security=False, is_active=True):
        username = (username or "").strip()
        if not username:
            raise ValueError("El nombre de usuario es obligatorio.")
        if not password:
            raise ValueError("La contraseña es obligatoria.")
        if self.get_user(username) is not None:
            raise ValueError(f"El usuario '{username}' ya existe.")
        with self._lock:
            cur = self.conn.cursor()
            cur.execute(
                "INSERT INTO users (username, password_hash, role, is_active, can_backup, can_restore, can_manage_users, can_manage_security, created_at) VALUES (?,?,?,?,?,?,?,?,?)",
                (
                    username,
                    self._hash_password(password),
                    role,
                    int(bool(is_active)),
                    int(bool(can_backup)),
                    int(bool(can_restore)),
                    int(bool(can_manage_users)),
                    int(bool(can_manage_security)),
                    datetime.now().isoformat(timespec="seconds"),
                ),
            )
            self.conn.commit()
            return cur.lastrowid

    def get_user(self, username):
        with self._lock:
            row = self.conn.execute(
                "SELECT * FROM users WHERE username=?",
                (username.strip(),),
            ).fetchone()
            return dict(row) if row is not None else None

    def get_user_by_id(self, user_id):
        with self._lock:
            row = self.conn.execute(
                "SELECT * FROM users WHERE id=?",
                (user_id,),
            ).fetchone()
            return dict(row) if row is not None else None

    def update_user(self, user_id, *, username=None, password=None, role=None, is_active=None,
                    can_backup=None, can_restore=None, can_manage_users=None, can_manage_security=None):
        user = self.get_user_by_id(user_id)
        if user is None:
            raise ValueError("Usuario no encontrado.")

        new_username = (username or user["username"]).strip()
        if not new_username:
            raise ValueError("El nombre de usuario es obligatorio.")

        with self._lock:
            if new_username != user["username"] and self.get_user(new_username) is not None:
                raise ValueError(f"El usuario '{new_username}' ya existe.")

            sql_parts = ["username = ?", "role = ?", "is_active = ?", "can_backup = ?", "can_restore = ?", "can_manage_users = ?", "can_manage_security = ?"]
            params = [
                new_username,
                role or user["role"],
                int(bool(is_active if is_active is not None else user["is_active"])),
                int(bool(can_backup if can_backup is not None else user["can_backup"])),
                int(bool(can_restore if can_restore is not None else user["can_restore"])),
                int(bool(can_manage_users if can_manage_users is not None else user["can_manage_users"])),
                int(bool(can_manage_security if can_manage_security is not None else user["can_manage_security"])),
            ]

            if password:
                sql_parts.insert(0, "password_hash = ?")
                params.insert(0, self._hash_password(password))

            params.append(user_id)
            self.conn.execute(
                f"UPDATE users SET {', '.join(sql_parts)} WHERE id = ?",
                tuple(params),
            )
            self.conn.commit()
            return self.get_user_by_id(user_id)

    def block_user(self, user_id, blocked=True):
        user = self.get_user_by_id(user_id)
        if user is None:
            raise ValueError("Usuario no encontrado.")
        return self.update_user(user_id, is_active=not blocked)

    def authenticate_user(self, username, password):
        user = self.get_user(username or "")
        if user is None:
            return None
        if not bool(user["is_active"]):
            return None
        if user["password_hash"] != self._hash_password(password or ""):
            return None
        return user

    def list_users(self):
        with self._lock:
            return self.conn.execute(
                "SELECT id, username, role, is_active, can_backup, can_restore, can_manage_users, can_manage_security, created_at FROM users ORDER BY username"
            ).fetchall()

    def log_event(self, user_id, action, details):
        with self._lock:
            self.conn.execute(
                "INSERT INTO audit_log (user_id, action, details, created_at) VALUES (?,?,?,?)",
                (user_id, action, details, datetime.now().isoformat(timespec="seconds")),
            )
            self.conn.commit()

    def get_audit_log(self, limit=50):
        with self._lock:
            return self.conn.execute(
                "SELECT a.id, u.username, a.action, a.details, a.created_at FROM audit_log a LEFT JOIN users u ON u.id=a.user_id ORDER BY a.id DESC LIMIT ?",
                (limit,),
            ).fetchall()

    # ---------- targets ----------
    def create_target(self, name, source_path, is_folder):
        with self._lock:
            cur = self.conn.cursor()
            cur.execute(
                "INSERT INTO targets (name, source_path, is_folder, created_at) VALUES (?,?,?,?)",
                (name, str(source_path), int(is_folder), datetime.now().isoformat()),
            )
            self.conn.commit()
            return cur.lastrowid

    def get_targets(self):
        with self._lock:
            return self.conn.execute("SELECT * FROM targets ORDER BY name").fetchall()

    def get_target(self, target_id):
        with self._lock:
            return self.conn.execute("SELECT * FROM targets WHERE id=?", (target_id,)).fetchone()

    # ---------- snapshots ----------
    def create_snapshot(self, target_id, note=""):
        with self._lock:
            cur = self.conn.cursor()
            cur.execute(
                "INSERT INTO snapshots (target_id, timestamp, note) VALUES (?,?,?)",
                (target_id, datetime.now().isoformat(), note),
            )
            self.conn.commit()
            return cur.lastrowid

    def get_snapshots(self, target_id):
        with self._lock:
            return self.conn.execute(
                "SELECT * FROM snapshots WHERE target_id=? ORDER BY timestamp DESC", (target_id,)
            ).fetchall()

    def delete_snapshot(self, snapshot_id):
        with self._lock:
            rows = self.get_file_versions(snapshot_id)
            self.conn.execute("DELETE FROM snapshots WHERE id=?", (snapshot_id,))
            self.conn.commit()
            removed = []
            for row in rows:
                still_used = self.conn.execute(
                    "SELECT 1 FROM file_versions WHERE hash=? LIMIT 1", (row["hash"],)
                ).fetchone()
                if still_used:
                    continue
                self.conn.execute("DELETE FROM files WHERE hash=?", (row["hash"],))
                try:
                    os.remove(row["archive_location"])
                    removed.append(row["archive_location"])
                except FileNotFoundError:
                    pass
            self.conn.commit()
            return len(removed)

    def delete_old_snapshots(self, target_id, keep_count):
        snapshots = self.get_snapshots(target_id)
        removed = 0
        for snapshot in snapshots[int(keep_count):]:
            self.delete_snapshot(snapshot["id"])
            removed += 1
        return removed

    def get_storage_stats(self):
        with self._lock:
            snapshot_count = self.conn.execute("SELECT COUNT(*) FROM snapshots").fetchone()[0]
            file_count = self.conn.execute("SELECT COUNT(*) FROM files").fetchone()[0]
            latest = self.conn.execute("SELECT timestamp FROM snapshots ORDER BY timestamp DESC LIMIT 1").fetchone()
            total_size = 0
            for row in self.conn.execute("SELECT archive_location FROM files"):
                if os.path.exists(row[0]):
                    total_size += os.path.getsize(row[0])
            return {"snapshots": snapshot_count, "files": file_count, "size": total_size,
                    "latest": latest[0] if latest else None}

    def get_snapshot_count(self, target_id):
        with self._lock:
            return self.conn.execute("SELECT COUNT(*) FROM snapshots WHERE target_id=?", (target_id,)).fetchone()[0]

    def get_snapshot(self, snapshot_id):
        with self._lock:
            return self.conn.execute("SELECT * FROM snapshots WHERE id=?", (snapshot_id,)).fetchone()

    # ---------- files (contenido deduplicado) ----------
    def get_file_by_hash(self, file_hash):
        with self._lock:
            return self.conn.execute("SELECT * FROM files WHERE hash=?", (file_hash,)).fetchone()

    def register_file(self, file_hash, size, archive_location):
        with self._lock:
            self.conn.execute(
                "INSERT OR IGNORE INTO files (hash, size, archive_location, first_seen_at) VALUES (?,?,?,?)",
                (file_hash, size, archive_location, datetime.now().isoformat()),
            )
            self.conn.commit()

    # ---------- file_versions ----------
    def add_file_version(self, snapshot_id, relative_path, file_hash, mtime, is_dynamic=False):
        with self._lock:
            self.conn.execute(
                "INSERT INTO file_versions (snapshot_id, relative_path, hash, mtime, is_dynamic) VALUES (?,?,?,?,?)",
                (snapshot_id, relative_path, file_hash, mtime, int(bool(is_dynamic))),
            )
            self.conn.commit()

    def get_file_versions(self, snapshot_id, only_critical=False):
        with self._lock:
            query = """SELECT fv.relative_path, fv.hash, fv.mtime, f.size, f.archive_location, fv.is_dynamic
                       FROM file_versions fv
                       JOIN files f ON f.hash = fv.hash
                       WHERE fv.snapshot_id=?"""
            if only_critical:
                query += " AND fv.is_dynamic = 0"
            
            return self.conn.execute(query, (snapshot_id,)).fetchall()

    def get_file_versions_by_type(self, snapshot_id):
        """Devuelve dict con 'critical' y 'dynamic' listas de file_versions"""
        with self._lock:
            versions = self.conn.execute(
                """SELECT fv.relative_path, fv.hash, fv.mtime, f.size, f.archive_location, fv.is_dynamic
                   FROM file_versions fv
                   JOIN files f ON f.hash = fv.hash
                   WHERE fv.snapshot_id=?""",
                (snapshot_id,)
            ).fetchall()
            
            critical = [v for v in versions if not v["is_dynamic"]]
            dynamic = [v for v in versions if v["is_dynamic"]]
            
            return {"critical": critical, "dynamic": dynamic}

    def close(self):
        self.conn.close()
