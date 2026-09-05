"""
Genera archivos .log y .csv para probar backups y verificacion.

Uso desde la raiz del proyecto:
    python "archivos de prueba/generador_archivos.py"

Detener con Ctrl+C. Por defecto genera una entrada cada 5 segundos.
"""

import argparse
import csv
import random
import time
from datetime import datetime
from pathlib import Path


DEFAULT_OUTPUT_DIR = Path(__file__).parent / "generados_dinamicos"


def ensure_csv_header(csv_path):
    if csv_path.exists() and csv_path.stat().st_size > 0:
        return

    with csv_path.open("w", newline="", encoding="utf-8") as csv_file:
        writer = csv.writer(csv_file)
        writer.writerow(["fecha", "evento", "valor", "estado"])


def generate_entry(log_path, csv_path, entry_number):
    timestamp = datetime.now().isoformat(timespec="seconds")
    events = ["inicio", "proceso", "actualizacion", "advertencia"]
    event = random.choice(events)
    value = random.randint(1, 100)
    status = "OK" if event != "advertencia" else "REVISION"

    with log_path.open("a", encoding="utf-8") as log_file:
        log_file.write(
            f"{timestamp} | entrada={entry_number} | evento={event} "
            f"| valor={value} | estado={status}\n"
        )

    with csv_path.open("a", newline="", encoding="utf-8") as csv_file:
        writer = csv.writer(csv_file)
        writer.writerow([timestamp, event, value, status])

    print(f"[{timestamp}] Entrada {entry_number}: log y CSV actualizados.", flush=True)


def main():
    parser = argparse.ArgumentParser(description="Genera archivos de prueba que cambian constantemente.")
    parser.add_argument(
        "--intervalo",
        type=float,
        default=5,
        help="Segundos entre entradas nuevas (por defecto: 5).",
    )
    parser.add_argument(
        "--duracion",
        type=float,
        default=0,
        help="Duracion en segundos; 0 significa ejecutar hasta Ctrl+C.",
    )
    parser.add_argument(
        "--salida",
        type=Path,
        default=DEFAULT_OUTPUT_DIR,
        help="Carpeta donde se guardaran los archivos generados.",
    )
    args = parser.parse_args()

    if args.intervalo <= 0:
        parser.error("--intervalo debe ser mayor que cero.")
    if args.duracion < 0:
        parser.error("--duracion no puede ser negativa.")

    args.salida.mkdir(parents=True, exist_ok=True)
    log_path = args.salida / "actividad.log"
    csv_path = args.salida / "actividad.csv"
    ensure_csv_header(csv_path)

    print(f"Generando archivos en: {args.salida}")
    print("Pulsa Ctrl+C para detener el generador.")

    start_time = time.monotonic()
    entry_number = 1
    try:
        while args.duracion == 0 or time.monotonic() - start_time < args.duracion:
            generate_entry(log_path, csv_path, entry_number)
            entry_number += 1
            remaining = args.duracion - (time.monotonic() - start_time)
            if args.duracion == 0:
                time.sleep(args.intervalo)
            elif remaining > 0:
                time.sleep(min(args.intervalo, remaining))
    except KeyboardInterrupt:
        print("Generador detenido.")


if __name__ == "__main__":
    main()
