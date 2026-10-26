using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Security.Cryptography;
using System.Threading;

namespace Rebith.Core
{
    public class ResultadoExportacion
    {
        public string Archivo = "";
        public int Archivos;
        public long BytesOriginales;
        public long TamanoZip;
        /// <summary>Archivos que el respaldo no pudo leer en su momento: no van en el .zip.</summary>
        public int OmitidosEnRespaldo;
        /// <summary>Contenidos que se tomaron de la replica porque el deposito principal estaba danado.</summary>
        public int DesdeReplica;
    }

    /// <summary>
    /// Saca un respaldo completo a un .zip normal, que se abre con Windows sin Rebith (para
    /// subirlo a la nube o llevarlo a otra PC). Reglas:
    ///  - Dentro va la carpeta del programa con su nombre, igual que "Enviar a > Carpeta
    ///    comprimida" de Windows, con las fechas originales de cada archivo.
    ///  - El .zip se arma en un temporal junto al destino y se publica al final: nunca queda
    ///    un .zip a medias con el nombre final (cancelar, disco lleno o un corte de luz).
    ///  - Cada archivo se descomprime del deposito comprobando su huella SHA-256. Si el
    ///    contenido esta danado y la replica lo tiene sano, se vuelve a armar con la replica;
    ///    si no, se detiene: no se entrega un .zip con un archivo danado.
    ///  - Al terminar se relee el .zip completo y cada archivo se compara con su huella.
    /// </summary>
    public class ZipExporter
    {
        private const int Bloque = 1024 * 1024;
        private const int VueltasMaximas = 5;

        // Ya vienen comprimidos: comprimirlos otra vez solo gasta tiempo.
        private static readonly HashSet<string> YaComprimidos = new HashSet<string>(StringComparer.OrdinalIgnoreCase)
        {
            ".zip", ".7z", ".rar", ".gz", ".jpg", ".jpeg", ".png", ".mp4", ".avi", ".docx", ".xlsx", ".pptx"
        };

        private readonly Repository repo;
        private readonly ObjectStore replica;

        public ZipExporter(Repository repo, ObjectStore replica)
        {
            this.repo = repo;
            this.replica = replica;
        }

        private class ContenidoDanadoException : Exception
        {
            public readonly string Hash;
            public readonly string Ruta;
            public ContenidoDanadoException(string hash, string ruta, string detalle) : base(detalle)
            {
                Hash = hash;
                Ruta = ruta;
            }
        }

        /// <param name="reemplazar">false = si el .zip ya existe, no se toca y se avisa.</param>
        public ResultadoExportacion Exportar(RepoLock candado, SnapshotManifest m, string archivoZip, bool reemplazar,
            IProgress<ProgressInfo> progreso, CancellationToken cancelar)
        {
            if (candado == null) throw new ArgumentNullException("candado");
            if (string.IsNullOrWhiteSpace(archivoZip)) throw new RebithException("Elija donde guardar el .zip.");
            string final = Path.GetFullPath(archivoZip);
            if (!final.EndsWith(".zip", StringComparison.OrdinalIgnoreCase)) final += ".zip";
            string carpeta = Path.GetDirectoryName(final);
            if (Rutas.SeTraslapan(carpeta, repo.Raiz))
                throw new RebithException("No se puede guardar el .zip dentro del deposito.");
            if (File.Exists(final) && !reemplazar)
                throw new RebithException("Ya existe " + final + ". Elija otro nombre.");
            if (m.Files.Count == 0) throw new RebithException("El respaldo no tiene archivos.");
            Directory.CreateDirectory(carpeta);

            string raiz = m.IsFolder ? NombreDeCarpeta(m) : "";
            var nombres = new List<string>(m.Files.Count);
            foreach (FileEntry f in m.Files) nombres.Add(NombreEnZip(raiz, f.Path));
            RevisarEspacio(m, carpeta);

            var usarReplica = new HashSet<string>(StringComparer.Ordinal);
            string temporal = Path.Combine(carpeta, "." + Path.GetFileName(final) + "." + Guid.NewGuid().ToString("N").Substring(0, 8) + ".rebith-tmp");
            try
            {
                for (int vuelta = 1; ; vuelta++)
                {
                    try
                    {
                        Armar(m, nombres, temporal, usarReplica, progreso, cancelar);
                        break;
                    }
                    catch (ContenidoDanadoException ex)
                    {
                        AtomicFile.BorrarSinError(temporal);
                        bool replicaSana = replica != null && !usarReplica.Contains(ex.Hash) && vuelta < VueltasMaximas &&
                            replica.Verificar(ex.Hash, -1, cancelar).Estado == EstadoObjeto.Ok;
                        if (!replicaSana)
                            throw new RebithException("El respaldo tiene un archivo danado: " + ex.Ruta + " (" + ex.Message +
                                "). Revise la integridad en Verificar. No se creo el .zip.");
                        Log.Advertencia("Exportar .zip: " + ex.Ruta + " esta danado en el deposito principal; se toma de la replica.");
                        usarReplica.Add(ex.Hash);
                    }
                }

                Comprobar(m, nombres, temporal, progreso, cancelar);

                if (File.Exists(final)) AtomicFile.Publicar(temporal, final, false);
                else File.Move(temporal, final);
            }
            finally
            {
                AtomicFile.BorrarSinError(temporal);
            }

            var r = new ResultadoExportacion
            {
                Archivo = final,
                Archivos = m.Files.Count,
                BytesOriginales = m.Files.Sum(f => f.Size),
                TamanoZip = new FileInfo(final).Length,
                OmitidosEnRespaldo = m.Skipped.Count,
                DesdeReplica = usarReplica.Count
            };
            Log.Info(string.Format("Respaldo {0} exportado a {1}: {2} archivos, {3}", m.Id, final, r.Archivos, Tamanos.Legible(r.TamanoZip)));
            return r;
        }

        private void Armar(SnapshotManifest m, List<string> nombres, string temporal, HashSet<string> usarReplica,
            IProgress<ProgressInfo> progreso, CancellationToken cancelar)
        {
            byte[] buffer = new byte[Bloque];
            using (var fs = new FileStream(temporal, FileMode.Create, FileAccess.ReadWrite, FileShare.None, Bloque))
            {
                using (var zip = new ZipArchive(fs, ZipArchiveMode.Create, true))
                {
                    for (int i = 0; i < m.Files.Count; i++)
                    {
                        cancelar.ThrowIfCancellationRequested();
                        FileEntry f = m.Files[i];
                        if (progreso != null) progreso.Report(new ProgressInfo { Current = i + 1, Total = m.Files.Count, Item = f.Path, Stage = "Comprimiendo" });
                        CompressionLevel nivel = YaComprimidos.Contains(Path.GetExtension(f.Path)) ? CompressionLevel.NoCompression : CompressionLevel.Optimal;
                        ZipArchiveEntry entrada = zip.CreateEntry(nombres[i], nivel);
                        PonerFecha(entrada, f.MtimeUtcTicks);
                        using (Stream salida = entrada.Open())
                            CopiarContenido(f, usarReplica.Contains(f.Hash) ? replica : repo.Objetos, salida, buffer, cancelar);
                    }
                }
                fs.Flush(true);
            }
        }

        /// <summary>Copia el contenido descomprimido al .zip calculando su huella.</summary>
        private void CopiarContenido(FileEntry f, ObjectStore almacen, Stream salida, byte[] buffer, CancellationToken cancelar)
        {
            if (!almacen.Existe(f.Hash))
            {
                if (almacen == repo.Objetos && replica != null && replica.Existe(f.Hash))
                    throw new ContenidoDanadoException(f.Hash, f.Path, "falta en el deposito principal");
                throw new RebithException("Falta el contenido de " + f.Path + " en el deposito. Revise la integridad en Verificar. No se creo el .zip.");
            }
            Stream entrada;
            try
            {
                entrada = almacen.AbrirContenido(f.Hash);
            }
            catch (InvalidDataException ex)
            {
                throw new ContenidoDanadoException(f.Hash, f.Path, ex.Message);
            }
            using (entrada)
            using (var sha = SHA256.Create())
            {
                long total = 0;
                while (true)
                {
                    cancelar.ThrowIfCancellationRequested();
                    int leidos;
                    try
                    {
                        leidos = entrada.Read(buffer, 0, buffer.Length);
                    }
                    catch (InvalidDataException ex)
                    {
                        throw new ContenidoDanadoException(f.Hash, f.Path, "no se puede descomprimir: " + ex.Message);
                    }
                    if (leidos <= 0) break;
                    sha.TransformBlock(buffer, 0, leidos, null, 0);
                    salida.Write(buffer, 0, leidos);
                    total += leidos;
                }
                sha.TransformFinalBlock(new byte[0], 0, 0);
                if (Hashes.Hex(sha.Hash) != f.Hash) throw new ContenidoDanadoException(f.Hash, f.Path, "la huella no coincide");
                if (total != f.Size) throw new ContenidoDanadoException(f.Hash, f.Path, "el tamano no coincide");
            }
        }

        /// <summary>Relee el .zip ya cerrado y compara cada archivo con su huella del respaldo.</summary>
        private static void Comprobar(SnapshotManifest m, List<string> nombres, string temporal,
            IProgress<ProgressInfo> progreso, CancellationToken cancelar)
        {
            var esperado = new Dictionary<string, FileEntry>(StringComparer.Ordinal);
            for (int i = 0; i < m.Files.Count; i++) esperado[nombres[i]] = m.Files[i];
            byte[] buffer = new byte[Bloque];
            int n = 0;
            using (var fs = new FileStream(temporal, FileMode.Open, FileAccess.Read, FileShare.Read, Bloque))
            using (var zip = new ZipArchive(fs, ZipArchiveMode.Read))
            {
                if (zip.Entries.Count != esperado.Count)
                    throw new RebithException("El .zip quedo con " + zip.Entries.Count + " archivos en vez de " + esperado.Count + ". No se entrego.");
                foreach (ZipArchiveEntry e in zip.Entries)
                {
                    cancelar.ThrowIfCancellationRequested();
                    n++;
                    if (progreso != null) progreso.Report(new ProgressInfo { Current = n, Total = esperado.Count, Item = e.FullName, Stage = "Comprobando el .zip" });
                    FileEntry f;
                    if (!esperado.TryGetValue(e.FullName, out f))
                        throw new RebithException("El .zip tiene un archivo que no esta en el respaldo: " + e.FullName + ". No se entrego.");
                    using (Stream s = e.Open())
                    using (var sha = SHA256.Create())
                    {
                        long total = 0;
                        int leidos;
                        while ((leidos = s.Read(buffer, 0, buffer.Length)) > 0)
                        {
                            cancelar.ThrowIfCancellationRequested();
                            sha.TransformBlock(buffer, 0, leidos, null, 0);
                            total += leidos;
                        }
                        sha.TransformFinalBlock(new byte[0], 0, 0);
                        if (Hashes.Hex(sha.Hash) != f.Hash || total != f.Size)
                            throw new RebithException("Al releer el .zip, " + e.FullName + " no coincide con el respaldo (revise el disco destino). No se entrego.");
                    }
                }
            }
        }

        /// <summary>Calcula con lo que ocupa cada contenido en el deposito (ya comprimido) y deja margen.</summary>
        private void RevisarEspacio(SnapshotManifest m, string carpeta)
        {
            long libre = Rutas.EspacioLibre(carpeta);
            if (libre < 0) return;
            long necesario = 10L * 1024 * 1024;
            foreach (FileEntry f in m.Files)
            {
                try
                {
                    var info = new FileInfo(repo.Objetos.RutaDe(f.Hash));
                    necesario += info.Exists ? info.Length : f.Size;
                }
                catch
                {
                    necesario += f.Size;
                }
            }
            necesario += necesario / 10;
            if (libre < necesario)
                throw new RebithException("No hay espacio suficiente para el .zip en " + carpeta + ": se necesitan unos " +
                    Tamanos.Legible(necesario) + " y hay " + Tamanos.Legible(libre) + ".");
        }

        private static void PonerFecha(ZipArchiveEntry entrada, long ticksUtc)
        {
            try
            {
                // El .zip guarda la hora local (formato DOS, de 1980 a 2107).
                DateTime local = new DateTime(ticksUtc, DateTimeKind.Utc).ToLocalTime();
                if (local.Year < 1980) local = new DateTime(1980, 1, 1, 0, 0, 0, DateTimeKind.Local);
                if (local.Year <= 2107) entrada.LastWriteTime = new DateTimeOffset(local);
            }
            catch
            {
                // La fecha es un detalle: el contenido es lo que importa.
            }
        }

        /// <summary>Nombre de la carpeta del programa (la ultima parte de la ruta respaldada).</summary>
        private static string NombreDeCarpeta(SnapshotManifest m)
        {
            string nombre = (m.SourcePath ?? "").Split(new[] { '\\', '/' }, StringSplitOptions.RemoveEmptyEntries).LastOrDefault() ?? "";
            if (nombre.EndsWith(":")) nombre = "";
            if (nombre.Length == 0) nombre = m.TargetName ?? "";
            foreach (char c in Path.GetInvalidFileNameChars()) nombre = nombre.Replace(c, '_');
            nombre = nombre.Replace('\\', '_').Replace('/', '_').Replace(':', '_').Trim();
            return nombre.Length == 0 ? "respaldo" : nombre;
        }

        /// <summary>Ruta dentro del .zip con "/", sin "..", ni rutas absolutas ni unidades.</summary>
        private static string NombreEnZip(string raiz, string relativa)
        {
            string limpia = (relativa ?? "").Replace('\\', '/');
            if (limpia.Length == 0 || limpia.StartsWith("/") || limpia.Contains(":"))
                throw new RebithException("Ruta no permitida en el respaldo: " + relativa);
            foreach (string parte in limpia.Split('/'))
            {
                if (parte.Length == 0 || parte == "." || parte == "..")
                    throw new RebithException("Ruta no permitida en el respaldo: " + relativa);
            }
            return raiz.Length == 0 ? limpia : raiz + "/" + limpia;
        }
    }
}
