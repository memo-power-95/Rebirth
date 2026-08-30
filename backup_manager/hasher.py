"""
hasher.py
Calcula hashes SHA-256 de archivos, leyendo en bloques para no
cargar archivos grandes completos en memoria.
"""

import hashlib

CHUNK_SIZE = 1024 * 1024  # 1 MB


def hash_file(path):
    """Devuelve el hash SHA-256 (hex string) de un archivo."""
    sha256 = hashlib.sha256()
    with open(path, "rb") as f:
        while True:
            chunk = f.read(CHUNK_SIZE)
            if not chunk:
                break
            sha256.update(chunk)
    return sha256.hexdigest()
