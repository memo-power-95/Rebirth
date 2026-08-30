@echo off
cd /d "%~dp0"

python -m PyInstaller --clean --noconfirm --onefile --windowed --name rebith main.py

echo.
echo =====================================================
echo Build completado.
echo El ejecutable esta disponible en dist\rebith.exe.
echo =====================================================
pause
