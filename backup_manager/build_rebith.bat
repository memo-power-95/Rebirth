@echo off
cd /d "%~dp0"

REM MOD@@ Guillermo Carrillo - corregido "em set" -> "set"; agregado chequeo de error
set "APP_NAME=rebith_prod"
python -m PyInstaller --clean --noconfirm --onefile --windowed --name %APP_NAME% main.py

if errorlevel 1 (
    echo.
    echo =====================================================
    echo ERROR: el build fallo. Revisa los mensajes de arriba.
    echo =====================================================
    pause
    exit /b 1
)

echo.
echo =====================================================
echo Build completado.
echo El ejecutable esta disponible en dist\%APP_NAME%.exe.
echo =====================================================
pause
