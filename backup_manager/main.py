"""
main.py
Punto de entrada. Ejecuta: python main.py
"""

import sys
from pathlib import Path

sys.path.insert(0, str(Path(__file__).parent))
sys.path.insert(0, str(Path(__file__).parent / "gui"))

from gui.main_window import MainWindow

if __name__ == "__main__":
    app = MainWindow()
    app.mainloop()
