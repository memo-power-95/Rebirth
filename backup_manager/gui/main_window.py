"""
gui/main_window.py
Ventana principal: 3 pestañas (Backup, Historial, Verificar).
El trabajo pesado corre en threads para no congelar la interfaz.
"""

import tkinter as tk
from tkinter import ttk, filedialog, messagebox, simpledialog
import threading
import os
import json
from datetime import datetime, timedelta
from pathlib import Path

from database import Database
from backup_engine import BackupEngine
from restore_engine import RestoreEngine
from verifier import Verifier, OK, MODIFICADO, CORRUPTO, FALTA


class AdminAccessDialog(tk.Toplevel):
    def __init__(self, app):
        super().__init__(app)
        self.app = app
        self.title("Acceso especial")
        self.geometry("360x220")
        self.resizable(False, False)
        self.transient(app)
        self.grab_set()

        ttk.Label(self, text="Usuario administrador:").pack(anchor="w", padx=15, pady=(15, 0))
        self.username_var = tk.StringVar()
        self.user_entry = ttk.Entry(self, textvariable=self.username_var, width=30)
        self.user_entry.pack(fill="x", padx=15, pady=5)

        ttk.Label(self, text="Contraseña:").pack(anchor="w", padx=15, pady=(10, 0))
        self.password_var = tk.StringVar()
        self.pass_entry = ttk.Entry(self, textvariable=self.password_var, show="*", width=30)
        self.pass_entry.pack(fill="x", padx=15, pady=5)

        ttk.Button(self, text="Entrar", command=self._login).pack(pady=(12, 8))
        self.bind("<Return>", lambda event: self._login())
        self.user_entry.focus_set()

    def _login(self):
        username = self.username_var.get().strip()
        password = self.password_var.get()
        user = self.app.db.authenticate_user(username, password)

        if user is None:
            self.app.db.log_event(None, "login_failed", f"Intento de acceso especial para usuario: {username}")
            messagebox.showerror("Acceso denegado", "Usuario o contraseña incorrectos.")
            return

        if user["role"] not in {"admin", "supervisor"} and not user["can_manage_security"]:
            self.app.db.log_event(user["id"], "login_forbidden", "Intento de acceso especial sin permisos.")
            messagebox.showerror("Sin permisos", "Este usuario no tiene acceso a permisos especiales.")
            return

        self.app.db.log_event(user["id"], "login_special_access", "Acceso a permisos especiales autorizado.")
        self.destroy()
        SpecialAccessPanel(self.app, user)


class SpecialAccessPanel(tk.Toplevel):
    def __init__(self, app, user):
        super().__init__(app)
        self.app = app
        self.user = user
        self.title("Permisos especiales")
        self.geometry("560x420")
        self.resizable(False, False)
        self.transient(app)
        self.grab_set()

        ttk.Label(self, text=f"Sesión: {user['username']} ({user['role']})", font=("Segoe UI", 10, "bold")).pack(anchor="w", padx=15, pady=(15, 5))

        buttons = ttk.Frame(self)
        buttons.pack(fill="x", padx=15, pady=10)
        ttk.Button(buttons, text="Usuarios", command=self._show_users).pack(side="left", padx=(0, 8))
        ttk.Button(buttons, text="Auditoría", command=self._show_audit).pack(side="left", padx=(0, 8))
        ttk.Button(buttons, text="Cerrar", command=self.destroy).pack(side="left")

        ttk.Label(self, text="Acciones permitidas:", font=("Segoe UI", 9, "bold")).pack(anchor="w", padx=15, pady=(10, 0))
        info = ttk.Frame(self)
        info.pack(fill="both", expand=True, padx=15, pady=10)

        perms = [
            ("Backup", bool(user["can_backup"])),
            ("Restaurar", bool(user["can_restore"])),
            ("Gestionar usuarios", bool(user["can_manage_users"])),
            ("Gestionar seguridad", bool(user["can_manage_security"])),
        ]

        for label, enabled in perms:
            state = "HABILITADO" if enabled else "DESHABILITADO"
            ttk.Label(info, text=f"- {label}: {state}").pack(anchor="w", pady=4)

    def _show_users(self):
        if not self.user["can_manage_users"] and self.user["role"] != "admin":
            messagebox.showwarning("Sin permisos", "No tienes permiso para gestionar usuarios.")
            return

        win = tk.Toplevel(self)
        win.title("Usuarios")
        win.geometry("760x320")
        tree = ttk.Treeview(win, columns=("usuario", "rol", "estado", "backup", "restore", "usuarios", "seguridad"), show="headings")
        headers = ["usuario", "rol", "estado", "backup", "restore", "usuarios", "seguridad"]
        for col, title in zip(headers, ["Usuario", "Rol", "Estado", "Backup", "Restore", "Usuarios", "Seguridad"]):
            tree.heading(col, text=title)
            tree.column(col, width=90)
        tree.pack(fill="both", expand=True, padx=10, pady=(10, 5))

        for row in self.app.db.list_users():
            tree.insert("", "end", values=(
                row["username"],
                row["role"],
                "Activo" if row["is_active"] else "Bloqueado",
                "Sí" if row["can_backup"] else "No",
                "Sí" if row["can_restore"] else "No",
                "Sí" if row["can_manage_users"] else "No",
                "Sí" if row["can_manage_security"] else "No",
            ))

        btns = ttk.Frame(win)
        btns.pack(fill="x", padx=10, pady=(0, 10))
        ttk.Button(btns, text="Crear usuario", command=self._create_user_dialog).pack(side="left", padx=(0, 8))
        ttk.Button(btns, text="Editar usuario", command=lambda: self._edit_selected_user(tree)).pack(side="left", padx=(0, 8))
        ttk.Button(btns, text="Bloquear / desbloquear", command=lambda: self._toggle_selected_user(tree)).pack(side="left")

    def _create_user_dialog(self):
        self._open_user_editor(mode="create")

    def _edit_selected_user(self, tree):
        selection = tree.selection()
        if not selection:
            messagebox.showwarning("Nada seleccionado", "Elige un usuario de la lista.")
            return
        username = tree.item(selection[0], "values")[0]
        user = self.app.db.get_user(username)
        self._open_user_editor(mode="edit", user=user)

    def _toggle_selected_user(self, tree):
        selection = tree.selection()
        if not selection:
            messagebox.showwarning("Nada seleccionado", "Elige un usuario de la lista.")
            return
        username = tree.item(selection[0], "values")[0]
        user = self.app.db.get_user(username)
        if user is None:
            return
        toggled = self.app.db.block_user(user["id"], blocked=bool(user["is_active"]))
        status = "desbloqueado" if toggled["is_active"] else "bloqueado"
        self.app.db.log_event(self.user["id"], "user_block_toggled", f"Usuario {username} {status}.")
        messagebox.showinfo("Usuario actualizado", f"El usuario {username} quedó {status}.")

    def _open_user_editor(self, mode="create", user=None):
        if not self.user["can_manage_users"] and self.user["role"] != "admin":
            messagebox.showwarning("Sin permisos", "No tienes permiso para administrar usuarios.")
            return

        win = tk.Toplevel(self)
        win.title("Crear usuario" if mode == "create" else "Editar usuario")
        win.geometry("420x320")
        win.transient(self)
        win.grab_set()

        fields = {
            "username": tk.StringVar(value=user["username"] if user else ""),
            "password": tk.StringVar(),
            "role": tk.StringVar(value=user["role"] if user else "operator"),
            "backup": tk.BooleanVar(value=bool(user["can_backup"]) if user else True),
            "restore": tk.BooleanVar(value=bool(user["can_restore"]) if user else True),
            "manage_users": tk.BooleanVar(value=bool(user["can_manage_users"]) if user else False),
            "manage_security": tk.BooleanVar(value=bool(user["can_manage_security"]) if user else False),
            "is_active": tk.BooleanVar(value=bool(user["is_active"]) if user else True),
        }

        ttk.Label(win, text="Usuario:").pack(anchor="w", padx=15, pady=(15, 0))
        ttk.Entry(win, textvariable=fields["username"]).pack(fill="x", padx=15)

        ttk.Label(win, text="Contraseña:").pack(anchor="w", padx=15, pady=(10, 0))
        ttk.Entry(win, textvariable=fields["password"], show="*").pack(fill="x", padx=15)

        ttk.Label(win, text="Rol:").pack(anchor="w", padx=15, pady=(10, 0))
        ttk.Combobox(win, textvariable=fields["role"], values=["operator", "supervisor", "admin"], state="readonly").pack(fill="x", padx=15)

        chk = ttk.Frame(win)
        chk.pack(fill="x", padx=15, pady=10)
        ttk.Checkbutton(chk, text="Permitir Backup", variable=fields["backup"]).pack(anchor="w")
        ttk.Checkbutton(chk, text="Permitir Restauración", variable=fields["restore"]).pack(anchor="w")
        ttk.Checkbutton(chk, text="Gestionar usuarios", variable=fields["manage_users"]).pack(anchor="w")
        ttk.Checkbutton(chk, text="Gestionar seguridad", variable=fields["manage_security"]).pack(anchor="w")
        ttk.Checkbutton(chk, text="Usuario activo", variable=fields["is_active"]).pack(anchor="w")

        def save():
            try:
                if mode == "create":
                    self.app.db.create_user(
                        username=fields["username"].get(),
                        password=fields["password"].get(),
                        role=fields["role"].get(),
                        can_backup=fields["backup"].get(),
                        can_restore=fields["restore"].get(),
                        can_manage_users=fields["manage_users"].get(),
                        can_manage_security=fields["manage_security"].get(),
                        is_active=fields["is_active"].get(),
                    )
                    self.app.db.log_event(self.user["id"], "user_created", f"Usuario {fields['username'].get()} creado.")
                    messagebox.showinfo("Usuario creado", "El usuario fue creado correctamente.")
                else:
                    self.app.db.update_user(
                        user["id"],
                        username=fields["username"].get(),
                        password=fields["password"].get() or None,
                        role=fields["role"].get(),
                        can_backup=fields["backup"].get(),
                        can_restore=fields["restore"].get(),
                        can_manage_users=fields["manage_users"].get(),
                        can_manage_security=fields["manage_security"].get(),
                        is_active=fields["is_active"].get(),
                    )
                    self.app.db.log_event(self.user["id"], "user_updated", f"Usuario {fields['username'].get()} actualizado.")
                    messagebox.showinfo("Usuario actualizado", "Los datos del usuario fueron guardados.")
                win.destroy()
            except Exception as exc:
                messagebox.showerror("Error", str(exc))

        ttk.Button(win, text="Guardar", command=save).pack(pady=(0, 12))

    def _show_audit(self):
        if not self.user["can_manage_security"] and self.user["role"] != "admin":
            messagebox.showwarning("Sin permisos", "No tienes permiso para ver auditoría.")
            return

        win = tk.Toplevel(self)
        win.title("Auditoría")
        win.geometry("700x300")
        tree = ttk.Treeview(win, columns=("usuario", "accion", "detalle", "fecha"), show="headings")
        tree.heading("usuario", text="Usuario")
        tree.heading("accion", text="Acción")
        tree.heading("detalle", text="Detalle")
        tree.heading("fecha", text="Fecha")
        tree.pack(fill="both", expand=True, padx=10, pady=10)

        for row in self.app.db.get_audit_log(limit=100):
            tree.insert("", "end", values=(row["username"] or "-", row["action"], row["details"] or "", row["created_at"]))


class MainWindow(tk.Tk):
    def __init__(self):
        super().__init__()
        self.title("Gestor de Backups")
        self.geometry("720x480")

        data_dir = Path(os.environ.get("LOCALAPPDATA", Path.home() / "AppData" / "Local")) / "rebith"
        data_dir.mkdir(parents=True, exist_ok=True)
        self.db = Database(data_dir / "backups.db")
        self.backup_engine = BackupEngine(self.db, storage_dir=data_dir / "storage")
        self.restore_engine = RestoreEngine(self.db)
        self.verifier = Verifier(self.db)
        self.current_user = self.db.ensure_default_admin()
        self.cancel_event = threading.Event()
        self.schedule_path = data_dir / "schedule.json"
        self.schedule = self._load_schedule()

        self.bind_all("<Control-Alt-f>", self._open_special_access)
        self.bind_all("<Control-Alt-F>", self._open_special_access)

        self.summary_var = tk.StringVar()
        summary_frame = ttk.Frame(self)
        summary_frame.pack(fill="x", padx=15, pady=(10, 0))
        ttk.Label(summary_frame, textvariable=self.summary_var, font=("Segoe UI", 10, "bold"), foreground="#24527a").pack(side="left")
        ttk.Button(summary_frame, text="Abrir carpeta de datos", command=lambda: os.startfile(data_dir)).pack(side="right")

        notebook = ttk.Notebook(self)
        notebook.pack(fill="both", expand=True)

        self.backup_tab = BackupTab(notebook, self)
        self.history_tab = HistoryTab(notebook, self)
        self.verify_tab = VerifyTab(notebook, self)

        notebook.add(self.backup_tab, text="Backup")
        notebook.add(self.history_tab, text="Historial")
        notebook.add(self.verify_tab, text="Verificar")

        self.notebook = notebook
        # Cuando cambian de pestaña, refrescamos combos de targets
        notebook.bind("<<NotebookTabChanged>>", self._on_tab_changed)
        self.after(100, self.refresh_dashboard)
        if self.schedule.get("run_on_startup") and self.schedule.get("target_id"):
            self.after(2000, self._run_scheduled_backup)
        self.after(60000, self._check_schedule)

    def _load_schedule(self):
        try:
            with open(self.schedule_path, "r", encoding="utf-8") as schedule_file:
                return json.load(schedule_file)
        except (OSError, json.JSONDecodeError):
            return {}

    def save_schedule(self, schedule):
        self.schedule = schedule
        with open(self.schedule_path, "w", encoding="utf-8") as schedule_file:
            json.dump(schedule, schedule_file, indent=2)

    def refresh_dashboard(self):
        stats = self.db.get_storage_stats()
        size_mb = stats["size"] / (1024 * 1024)
        latest = stats["latest"] or "Sin backups"
        self.summary_var.set(f"Snapshots: {stats['snapshots']}   |   Archivos: {stats['files']}   |   Espacio: {size_mb:.2f} MB   |   Ultimo: {latest}")

    def _check_schedule(self):
        if self.schedule.get("target_id") and self.schedule.get("frequency") in {"daily", "weekly"}:
            if datetime.now() >= datetime.fromisoformat(self.schedule["next_run"]):
                self._run_scheduled_backup()
        self.after(60000, self._check_schedule)

    def _run_scheduled_backup(self):
        target = self.db.get_target(self.schedule.get("target_id"))
        if target is None:
            return
        self.backup_tab.start_backup(target["id"], "Backup programado", self.schedule.get("exclude_patterns", []))
        days = 7 if self.schedule["frequency"] == "weekly" else 1
        self.schedule["next_run"] = (datetime.now() + timedelta(days=days)).isoformat(timespec="seconds")
        self.save_schedule(self.schedule)

    def _on_tab_changed(self, event):
        self.history_tab.refresh_targets()
        self.verify_tab.refresh_targets()

    def refresh_all(self):
        self.history_tab.refresh_targets()
        self.verify_tab.refresh_targets()
        self.refresh_dashboard()

    def call_on_ui(self, callback, *args, **kwargs):
        self.after(0, lambda: callback(*args, **kwargs))

    def _open_special_access(self, event=None):
        AdminAccessDialog(self)


class BackupTab(ttk.Frame):
    def __init__(self, parent, app: MainWindow):
        super().__init__(parent, padding=15)
        self.app = app

        ttk.Label(self, text="Elegir carpeta o archivo a respaldar:").pack(anchor="w")

        btn_frame = ttk.Frame(self)
        btn_frame.pack(fill="x", pady=5)
        ttk.Button(btn_frame, text="Elegir carpeta...", command=self.choose_folder).pack(side="left", padx=5)
        ttk.Button(btn_frame, text="Elegir archivo...", command=self.choose_file).pack(side="left", padx=5)

        self.path_var = tk.StringVar(value="(nada seleccionado)")
        ttk.Label(self, textvariable=self.path_var, foreground="gray").pack(anchor="w", pady=5)

        ttk.Label(self, text="Nota para este backup (opcional):").pack(anchor="w", pady=(10, 0))
        self.note_entry = ttk.Entry(self)
        self.note_entry.pack(fill="x")

        action_frame = ttk.Frame(self)
        action_frame.pack(fill="x", pady=15)
        ttk.Button(action_frame, text="Hacer backup ahora", command=self.do_backup).pack(side="left")
        self.cancel_button = ttk.Button(action_frame, text="Cancelar", command=self.app.cancel_event.set, state="disabled")
        self.cancel_button.pack(side="left", padx=8)

        self.progress = ttk.Progressbar(self, mode="determinate")
        self.progress.pack(fill="x")
        self.status_var = tk.StringVar(value="")
        ttk.Label(self, textvariable=self.status_var).pack(anchor="w")

        ttk.Label(self, text="Excluir patrones (opcional, separados por comas):").pack(anchor="w", pady=(8, 0))
        saved_patterns = self.app.schedule.get("exclude_patterns", [])
        self.exclude_var = tk.StringVar(value=", ".join(saved_patterns))
        ttk.Entry(self, textvariable=self.exclude_var).pack(fill="x")
        ttk.Label(self, text="Ejemplo: *.log, *.tmp, cache/*", foreground="gray").pack(anchor="w")

        self.selected_path = None
        self.is_folder = False
        self.last_target_id = None
        schedule_frame = ttk.LabelFrame(self, text="Backup automatico")
        schedule_frame.pack(fill="x", pady=(12, 0))
        saved_frequency = {"daily": "Diario", "weekly": "Semanal"}.get(self.app.schedule.get("frequency"), "Desactivado")
        self.schedule_frequency = tk.StringVar(value=saved_frequency)
        ttk.Label(schedule_frame, text="Frecuencia:").pack(side="left", padx=(8, 4), pady=8)
        ttk.Combobox(schedule_frame, textvariable=self.schedule_frequency,
                 values=["Desactivado", "Diario", "Semanal"], state="readonly", width=12).pack(side="left")
        ttk.Button(schedule_frame, text="Guardar programacion", command=self.save_schedule).pack(side="left", padx=8)
        self.run_startup_var = tk.BooleanVar(value=bool(self.app.schedule.get("run_on_startup")))
        ttk.Checkbutton(schedule_frame, text="Ejecutar al iniciar rebith", variable=self.run_startup_var).pack(side="left")

    def choose_folder(self):
        path = filedialog.askdirectory()
        if path:
            self.selected_path = Path(path)
            self.is_folder = True
            self.path_var.set(str(path))

    def choose_file(self):
        path = filedialog.askopenfilename()
        if path:
            self.selected_path = Path(path)
            self.is_folder = False
            self.path_var.set(str(path))

    def do_backup(self):
        if not self.selected_path:
            messagebox.showwarning("Falta selección", "Primero elige una carpeta o archivo.")
            return

        name = self.selected_path.name
        target_id = self.app.db.create_target(name, self.selected_path, self.is_folder)
        self.last_target_id = target_id
        exclude_patterns = self._get_exclude_patterns()
        self.start_backup(target_id, self.note_entry.get(), exclude_patterns)

    def start_backup(self, target_id, note="", exclude_patterns=None):
        self.app.cancel_event.clear()
        self.cancel_button.configure(state="normal")
        self.status_var.set("Creando backup...")

        def worker():
            def progress_cb(current, total, filename):
                self.app.call_on_ui(self.progress.configure, maximum=total, value=current)
                self.app.call_on_ui(self.status_var.set, f"({current}/{total}) {filename}")

            snapshot_id = None
            try:
                snapshot_id = self.app.backup_engine.backup_target(
                    target_id, note, progress_cb, self.app.cancel_event,
                    exclude_patterns=exclude_patterns or [],
                )
                archive_results = self.app.verifier.verify_snapshot_archive(snapshot_id, cancel_event=self.app.cancel_event)
                corrupt = sum(result["status"] == CORRUPTO for result in archive_results)
                status = "Backup correcto" if corrupt == 0 else f"Backup con errores: {corrupt} archivo(s) corrupto(s)"
                self.app.call_on_ui(self.status_var.set, f"{status}. Snapshot #{snapshot_id}")
            except RuntimeError as error:
                if snapshot_id is not None:
                    self.app.db.delete_snapshot(snapshot_id)
                self.app.call_on_ui(self.status_var.set, str(error))
            except Exception as error:
                self.app.call_on_ui(messagebox.showerror, "Error en backup", str(error))
            self.app.call_on_ui(self.cancel_button.configure, state="disabled")
            self.app.call_on_ui(self.app.refresh_all)

        threading.Thread(target=worker, daemon=True).start()

    def save_schedule(self):
        if self.schedule_frequency.get() == "Desactivado":
            self.app.save_schedule({})
            self.status_var.set("Backup automatico desactivado")
            return
        if self.last_target_id is None:
            messagebox.showwarning("Falta backup", "Haz primero un backup manual para elegir el objetivo.")
            return
        frequency = "weekly" if self.schedule_frequency.get() == "Semanal" else "daily"
        days = 7 if frequency == "weekly" else 1
        self.app.save_schedule({"target_id": self.last_target_id, "frequency": frequency,
                                "next_run": (datetime.now() + timedelta(days=days)).isoformat(timespec="seconds"),
                                "run_on_startup": self.run_startup_var.get(),
                                "exclude_patterns": self._get_exclude_patterns()})
        self.status_var.set("Programacion guardada")

    def _get_exclude_patterns(self):
        return [pattern.strip() for pattern in self.exclude_var.get().split(",") if pattern.strip()]


class HistoryTab(ttk.Frame):
    def __init__(self, parent, app: MainWindow):
        super().__init__(parent, padding=15)
        self.app = app

        top = ttk.Frame(self)
        top.pack(fill="x")
        ttk.Label(top, text="Programa/carpeta:").pack(side="left")
        self.target_combo = ttk.Combobox(top, state="readonly")
        self.target_combo.pack(side="left", padx=5, fill="x", expand=True)
        self.target_combo.bind("<<ComboboxSelected>>", lambda e: self.refresh_snapshots())

        self.tree = ttk.Treeview(self, columns=("fecha", "nota"), show="headings")
        self.tree.heading("fecha", text="Fecha")
        self.tree.heading("nota", text="Nota")
        self.tree.pack(fill="both", expand=True, pady=10)

        btns = ttk.Frame(self)
        btns.pack(fill="x")
        ttk.Button(btns, text="Restaurar esta versión...", command=self.restore_selected).pack(side="left")
        ttk.Button(btns, text="Eliminar snapshot", command=self.delete_selected).pack(side="left", padx=8)
        ttk.Button(btns, text="Conservar ultimos...", command=self.keep_recent).pack(side="left")
        self.cancel_button = ttk.Button(btns, text="Cancelar", command=self.app.cancel_event.set, state="disabled")
        self.cancel_button.pack(side="right")

        self._targets = []

    def refresh_targets(self):
        self._targets = self.app.db.get_targets()
        self.target_combo["values"] = [f'{t["id"]} - {t["name"]}' for t in self._targets]
        if self._targets and not self.target_combo.get():
            self.target_combo.current(0)
            self.refresh_snapshots()

    def refresh_snapshots(self):
        self.tree.delete(*self.tree.get_children())
        target_id = self._current_target_id()
        if target_id is None:
            return
        for snap in self.app.db.get_snapshots(target_id):
            self.tree.insert("", "end", iid=str(snap["id"]),
                              values=(snap["timestamp"], snap["note"] or ""))

    def _current_target_id(self):
        val = self.target_combo.get()
        if not val:
            return None
        return int(val.split(" - ")[0])

    def restore_selected(self):
        selection = self.tree.selection()
        if not selection:
            messagebox.showwarning("Nada seleccionado", "Elige una versión del historial.")
            return
        snapshot_id = int(selection[0])
        parent_dir = filedialog.askdirectory(title="Elegir carpeta donde guardar la restauracion")
        if not parent_dir:
            return

        target = self.app.db.get_target(self._current_target_id())
        restore_dir = Path(parent_dir) / target["name"]
        if restore_dir.exists():
            messagebox.showwarning("Carpeta existente", f"Ya existe la carpeta:\n{restore_dir}")
            return

        def worker():
            try:
                self.app.cancel_event.clear()
                self.app.call_on_ui(self.cancel_button.configure, state="normal")
                def progress_callback(current, total, filename):
                    self.app.call_on_ui(self.status_var.set, f"Restaurando ({current}/{total}) {filename}")
                count = self.app.restore_engine.restore_snapshot(snapshot_id, restore_dir,
                                                                 progress_callback, self.app.cancel_event)
            except Exception as error:
                self.app.call_on_ui(messagebox.showerror, "Error al restaurar", str(error))
                return
            finally:
                self.app.call_on_ui(self.cancel_button.configure, state="disabled")
            self.app.call_on_ui(messagebox.showinfo, "Restauracion completa", f"Se restauraron {count} archivo(s) en la carpeta:\n{restore_dir}")

        threading.Thread(target=worker, daemon=True).start()

    def delete_selected(self):
        selection = self.tree.selection()
        if not selection:
            messagebox.showwarning("Nada seleccionado", "Elige un snapshot del historial.")
            return
        snapshot_id = int(selection[0])
        if not messagebox.askyesno("Confirmar eliminacion", "Se eliminara este snapshot y los archivos que ya no use ningun otro backup. ¿Continuar?"):
            return
        self.app.db.delete_snapshot(snapshot_id)
        self.refresh_snapshots()
        self.app.refresh_dashboard()

    def keep_recent(self):
        target_id = self._current_target_id()
        if target_id is None:
            return
        keep = simpledialog.askinteger("Conservar snapshots", "¿Cuantos backups recientes quieres conservar?", minvalue=1, initialvalue=5)
        if keep is None:
            return
        removed = self.app.db.delete_old_snapshots(target_id, keep)
        self.refresh_snapshots()
        self.app.refresh_dashboard()
        messagebox.showinfo("Limpieza terminada", f"Se eliminaron {removed} snapshot(s).")


class VerifyTab(ttk.Frame):
    def __init__(self, parent, app: MainWindow):
        super().__init__(parent, padding=15)
        self.app = app

        top = ttk.Frame(self)
        top.pack(fill="x")
        ttk.Label(top, text="Programa/carpeta:").pack(side="left")
        self.target_combo = ttk.Combobox(top, state="readonly")
        self.target_combo.pack(side="left", padx=5, fill="x", expand=True)
        self.target_combo.bind("<<ComboboxSelected>>", lambda e: self.refresh_snapshots())

        top2 = ttk.Frame(self)
        top2.pack(fill="x", pady=5)
        ttk.Label(top2, text="Snapshot de referencia:").pack(side="left")
        self.snapshot_combo = ttk.Combobox(top2, state="readonly")
        self.snapshot_combo.pack(side="left", padx=5, fill="x", expand=True)

        ttk.Button(self, text="Verificar ahora", command=self.do_verify).pack(pady=5)
        self.cancel_button = ttk.Button(self, text="Cancelar verificacion", command=self.app.cancel_event.set, state="disabled")
        self.cancel_button.pack(pady=2)

        self.tree = ttk.Treeview(self, columns=("estado",), show="tree headings")
        self.tree.heading("#0", text="Archivo")
        self.tree.heading("estado", text="Estado")
        self.tree.tag_configure("ok", foreground="#167c3a")
        self.tree.tag_configure("modificado", foreground="#a15c00")
        self.tree.tag_configure("corrupto", foreground="#c62828", background="#ffebee")
        self.tree.tag_configure("falta", foreground="#7b1fa2", background="#f3e5f5")
        self.tree.pack(fill="both", expand=True, pady=10)

        repair_buttons = ttk.Frame(self)
        repair_buttons.pack(fill="x")
        ttk.Button(repair_buttons, text="Reparar seleccionados", command=self.repair_selected).pack(side="left")
        ttk.Button(repair_buttons, text="Reparar todos", command=self.repair_all).pack(side="left", padx=8)

        self._targets = []
        self._snapshots = []
        self.last_results = []
        self.last_snapshot_id = None

    def refresh_targets(self):
        self._targets = self.app.db.get_targets()
        self.target_combo["values"] = [f'{t["id"]} - {t["name"]}' for t in self._targets]
        if self._targets and not self.target_combo.get():
            self.target_combo.current(0)
            self.refresh_snapshots()

    def refresh_snapshots(self):
        target_id = self._current_target_id()
        if target_id is None:
            return
        self._snapshots = self.app.db.get_snapshots(target_id)
        self.snapshot_combo["values"] = [f'{s["id"]} - {s["timestamp"]}' for s in self._snapshots]
        if self._snapshots:
            self.snapshot_combo.current(0)

    def _current_target_id(self):
        val = self.target_combo.get()
        if not val:
            return None
        return int(val.split(" - ")[0])

    def _current_snapshot_id(self):
        val = self.snapshot_combo.get()
        if not val:
            return None
        return int(val.split(" - ")[0])

    def do_verify(self):
        target_id = self._current_target_id()
        snapshot_id = self._current_snapshot_id()
        if target_id is None or snapshot_id is None:
            messagebox.showwarning("Falta selección", "Elige programa y snapshot.")
            return
        target = self.app.db.get_target(target_id)

        def worker():
            try:
                self.app.cancel_event.clear()
                self.app.call_on_ui(self.cancel_button.configure, state="normal")
                results = self.app.verifier.verify(target["source_path"], snapshot_id, cancel_event=self.app.cancel_event)
            except Exception as error:
                self.app.call_on_ui(messagebox.showerror, "Error al verificar", str(error))
                return
            finally:
                self.app.call_on_ui(self.cancel_button.configure, state="disabled")
            self.app.call_on_ui(self._show_results, results, snapshot_id)

        threading.Thread(target=worker, daemon=True).start()

    def _show_results(self, results, snapshot_id):
        self.last_results = results
        self.last_snapshot_id = snapshot_id
        self.tree.delete(*self.tree.get_children())
        icons = {OK: "OK", MODIFICADO: "!", CORRUPTO: "X", FALTA: "-"}
        tags = {OK: "ok", MODIFICADO: "modificado", CORRUPTO: "corrupto", FALTA: "falta"}
        for result in results:
            status = result["status"]
            self.tree.insert("", "end", text=result["relative_path"],
                             values=(f'{icons.get(status, "")} {status}',),
                             tags=(tags.get(status, ""),))

    def repair_selected(self):
        self._repair_files(use_selection=True)

    def repair_all(self):
        self._repair_files(use_selection=False)

    def _repair_files(self, use_selection):
        if self.last_snapshot_id is None:
            messagebox.showwarning("Sin verificacion", "Verifica un snapshot antes de reparar archivos.")
            return

        target_id = self._current_target_id()
        target = self.app.db.get_target(target_id)
        dest_dir = target["source_path"] if target["is_folder"] else str(Path(target["source_path"]).parent)

        candidates = []
        if use_selection and self.tree.selection():
            candidates = [self.tree.item(item, "text") for item in self.tree.selection()]
            candidates = [rel_path for rel_path in candidates if any(
                result["relative_path"] == rel_path and result["status"] in (FALTA, MODIFICADO, CORRUPTO)
                for result in self.last_results
            )]
        else:
            if use_selection:
                messagebox.showwarning("Nada seleccionado", "Selecciona uno o más archivos faltantes o corruptos.")
                return
            candidates = [result["relative_path"] for result in self.last_results if result["status"] in (FALTA, CORRUPTO)]

        if not candidates:
            messagebox.showwarning("Nada que reparar", "No hay archivos faltantes ni corruptos para restaurar.")
            return

        to_restore = []
        for rel_path in candidates:
            real_path = Path(dest_dir, rel_path)
            if real_path.exists() and not messagebox.askyesno(
                    "Confirmar reparacion",
                    f"El archivo ya existe:\n{real_path}\n¿Quieres reemplazarlo?"):
                continue
            to_restore.append(rel_path)

        if not to_restore:
            return

        restored = self.app.restore_engine.restore_files(self.last_snapshot_id, to_restore, dest_dir)
        if restored:
            messagebox.showinfo("Reparacion completada", f"Se restauraron {len(restored)} archivo(s) faltantes/corruptos.")
        else:
            messagebox.showerror("Error", "No se encontraron archivos válidos en el snapshot para reparar.")
