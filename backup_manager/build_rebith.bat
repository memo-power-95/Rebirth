@echo off
cd /d "%~dp0"

em set "APP_NAME=rebith_prod"
python -m PyInstaller --clean --noconfirm --onefile --windowed --name %APP_NAME% main.py

echo.
echo =====================================================
echo Build completado.
echo El ejecutable esta disponible en dist\%APP_NAME%.exe.
echo =====================================================
pause
