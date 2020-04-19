using Gabriel.Cat.S.Utilitats;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;

namespace Gabriel.Cat.S.Extension
{
   public static class ExtensionDirectoryInfo
    {
        public static List<KeyValuePair<DirectoryInfo, FileInfo[]>> GetFiles(this DirectoryInfo dir, bool recursive = false)
        {//windows si da error no se puede omit por lo tanto te quedas sin los archivos que puedes coger...es por eso que hago mi metodo...
            List<KeyValuePair<DirectoryInfo, FileInfo[]>> carpetasConSusArchivos = new List<KeyValuePair<DirectoryInfo, FileInfo[]>>();
            List<DirectoryInfo> subDirs;
            bool canRead = dir.CanRead();
            if (canRead)
            {
                carpetasConSusArchivos.Add(new KeyValuePair<DirectoryInfo, FileInfo[]>(dir, dir.GetFiles()));

                if (recursive)
                {
                    subDirs = dir.SubDirectoris();
                    for (int i = 0; i < subDirs.Count; i++)
                        if (subDirs[i].CanRead())
                            carpetasConSusArchivos.Add(new KeyValuePair<DirectoryInfo, FileInfo[]>(subDirs[i], subDirs[i].GetFiles()));


                }
            }
            return carpetasConSusArchivos;
        }
        #region BuscaConHash

        public static FileInfo BuscaConHash(this DirectoryInfo dir, string fileHash, bool recursivo = false)
        {

            List<FileInfo> files = BuscaConHash(dir, new string[] { fileHash }, recursivo);
            FileInfo file;
            if (files.Count > 0)
                file = files[0];
            else
                file = null;
            return file;
        }
      
        public static List<FileInfo> BuscaConHash(this DirectoryInfo dir, IList<string> filesHash, bool recursivo = false)
        {
            return dir.IBuscoConHash(filesHash, recursivo);
        }
        static List<FileInfo> IBuscoConHash(this DirectoryInfo dir, IList<string> hashes, bool recursivo)
        {
            //por probar :)
            List<DirectoryInfo> dirs = new List<DirectoryInfo>();
            SortedList<string, string> llista = hashes.ToSortedList();
            List<FileInfo> filesEncontrados = new List<FileInfo>();
            FileInfo[] files = null;
            string hashArchivo = null;

            dirs.Add(dir);
            if (recursivo)
                dirs.AddRange(dir.SubDirectoris());

            for (int i = 0; i < dirs.Count && llista.Count != 0; i++)
            {
                files = dirs[i].GetFiles();
                for (int j = 0; j < files.Length && llista.Count != 0; j++)
                {

                   
                        hashArchivo = files[j].Hash();

                    if (llista.ContainsKey(hashArchivo))
                    {
                        filesEncontrados.Add(files[j]);
                        llista.Remove(hashArchivo);

                    }
                }
            }
            return filesEncontrados;
        }
        #endregion
        public static List<FileInfo> GetAllFiles(this DirectoryInfo dir)
        {
            List<DirectoryInfo> dirs = new List<DirectoryInfo>();
            List<FileInfo> files = new List<FileInfo>();
            dirs.Add(dir);
            dirs.AddRange(dir.SubDirectoris());
            for (int i = 0; i < dirs.Count; i++)
                if (dirs[i].CanRead())
                    files.AddRange(dirs[i].GetFiles());
            return files;
        }
        public static List<FileInfo> GetFiles(this DirectoryInfo dir, params string[] formatsAdmessos)
        {
            List<FileInfo> files = new List<FileInfo>();
            FileInfo[] filesDir;
            SortedList<string, string> dicFormats;
            if (dir.CanRead())
            {
                filesDir = dir.GetFiles();
                for (int i = 0; i < formatsAdmessos.Length; i++)
                    formatsAdmessos[i] = DameFormatoCorrectamente(formatsAdmessos[i]); //els arreglo
                dicFormats = formatsAdmessos.ToSortedList();
                for (int i = 0; i < filesDir.Length; i++)
                {
                    if (dicFormats.ContainsKey(filesDir[i].Extension))
                        files.Add(filesDir[i]);
                }
            }
            return files;
        }

        private static string DameFormatoCorrectamente(string formato)
        {
            string[] camposFormato;
            if (formato.Contains("."))
            {
                camposFormato = formato.Split('.');
                formato = camposFormato[camposFormato.Length - 1];
            }
            formato = "." + formato;
            return formato;
        }

        public static List<FileInfo> GetFiles(this DirectoryInfo dir, bool recursivo, params string[] formatsAdmessos)
        {
            List<FileInfo> files = new List<FileInfo>();
            List<DirectoryInfo> subDirs;
            if (dir.CanRead())
            {
                files.AddRange(dir.GetFiles(formatsAdmessos));
                if (recursivo)
                {
                    subDirs = dir.SubDirectoris();
                    for (int i = 0; i < subDirs.Count; i++)
                        if (subDirs[i].CanRead())
                            files.AddRange(subDirs[i].GetFiles(formatsAdmessos));
                }
            }
            return files;

        }
        public static List<KeyValuePair<DirectoryInfo, List<FileInfo>>> GetFilesWithDirectory(this DirectoryInfo dir)
        {
            return dir.GetFilesWithDirectory("*");
        }
        public static List<KeyValuePair<DirectoryInfo, List<FileInfo>>> GetFilesWithDirectory(this DirectoryInfo dir, params string[] formatsAdmessos)
        {
            return dir.GetFilesWithDirectory(false, formatsAdmessos);
        }
        public static List<KeyValuePair<DirectoryInfo, List<FileInfo>>> GetFilesWithDirectory(this DirectoryInfo dir, bool recursive, params string[] formatsAdmessos)
        {
            List<KeyValuePair<DirectoryInfo, List<FileInfo>>> llistaArxiusPerCarpeta = new List<KeyValuePair<DirectoryInfo, List<FileInfo>>>();
            List<DirectoryInfo> subDirs;
            if (dir.CanRead())
            {
                llistaArxiusPerCarpeta.Add(new KeyValuePair<DirectoryInfo, List<FileInfo>>(dir, dir.GetFiles(formatsAdmessos)));
                if (recursive)
                {
                    subDirs = dir.SubDirectoris();
                    for (int i = 0; i < subDirs.Count; i++)
                        if (subDirs[i].CanRead())
                            llistaArxiusPerCarpeta.Add(new KeyValuePair<DirectoryInfo,List<FileInfo>>(subDirs[i], subDirs[i].GetFiles(formatsAdmessos)));
                }
            }
            return llistaArxiusPerCarpeta;
        }
        public static List<DirectoryInfo> SubDirectoris(this DirectoryInfo dirPare)
        {
            return dirPare.ISubDirectoris();
        }
        static List<DirectoryInfo> ISubDirectoris(this DirectoryInfo dirPare)
        {
            List<DirectoryInfo> subDirectoris = new List<DirectoryInfo>();
            DirectoryInfo[] subDirs;
            if (dirPare.CanRead())
            {
                subDirs = dirPare.GetDirectories();
                subDirectoris.AddRange(subDirs);

                for (int i = 0; i < subDirs.Length; i++)
                    subDirectoris.AddRange(ISubDirectoris(subDirs[i]));

            }
            return subDirectoris;

        }
        /*Por mirar, revisar que sea optimo y necesario y este bien escrito ;) */
        /// <summary>
        /// Copia el archivo si no esta en la carpeta (mira el nombre)
        /// </summary>
        /// <param name="dir"></param>
        /// <param name="pathArchivo">direccion del archivo en memoria</param>
        /// <returns>devuelve la ruta final del archivo en caso de no existir el archivo devuelve null</returns>
        public static string HazSitio(this DirectoryInfo dir, string pathArchivoHaCopiar)
        {
            string direccionArchivoFinal = null;
            if (File.Exists(pathArchivoHaCopiar))
            {

                direccionArchivoFinal = dir.DamePathSinUsar(pathArchivoHaCopiar);
                File.Copy(pathArchivoHaCopiar, direccionArchivoFinal);

            }

            return direccionArchivoFinal;
        }
        /// <summary>
        /// Genera los tipicos NombreArchivo(NumeroNoOcupadoYa).extensión
        /// </summary>
        /// <param name="dir"></param>
        /// <param name="pathArchivoHaCopiar"></param>
        /// <returns></returns>
        public static string DamePathSinUsar(this DirectoryInfo dir, string pathArchivoHaCopiar)
        {
            int contadorIguales = 1;
            string nombreArchivo = Path.GetFileNameWithoutExtension(pathArchivoHaCopiar);
            string extension = Path.GetExtension(pathArchivoHaCopiar);
            string direccionArchivoFinal = dir.FullName + Path.DirectorySeparatorChar + nombreArchivo + extension;
            while (File.Exists(direccionArchivoFinal))
            {
                direccionArchivoFinal = dir.FullName + Path.DirectorySeparatorChar + nombreArchivo + "(" + contadorIguales + ")" + extension;
                contadorIguales++;
            }
            return direccionArchivoFinal;
        }
        /// <summary>
        /// Copia el archivo si no esta en el directorio compara el hash de cada archivo de la carpeta.
        /// </summary>
        /// <param name="dir"></param>
        /// <param name="pathArchivo">direccion completa del archivo</param>
        /// <returns>devuelve la ruta final del archivo, si encuentra el archivo no lo copia i devuelve su ruta,devuelve null si no existe el archivo</returns>
        public static string HazSitioSiNoEsta(this DirectoryInfo dir, string pathArchivo)
        {
            string direccionArchivoFinal = null;
            bool encontrado = false;
            string nombreArchivo;
            string extension;
            int contadorIguales = 1;
            if (File.Exists(pathArchivo))
            {
                string hashArchivo = new FileInfo(pathArchivo).Hash();
                FileInfo[] archivos = dir.GetFiles();
                for (int i = 0; i < archivos.Length && !encontrado; i++)
                {
                    encontrado = archivos[i].HashEquals(hashArchivo);
                    if (encontrado)
                        direccionArchivoFinal = archivos[i].FullName;
                }
                if (!encontrado)
                {
                    nombreArchivo = Path.GetFileNameWithoutExtension(pathArchivo);
                    extension = Path.GetExtension(pathArchivo);
                    direccionArchivoFinal = dir.FullName + Path.DirectorySeparatorChar + nombreArchivo + extension;
                    while (File.Exists(direccionArchivoFinal))
                    {
                        direccionArchivoFinal = dir.FullName + Path.DirectorySeparatorChar + nombreArchivo + "(" + contadorIguales + ")" + extension;
                        contadorIguales++;

                    }
                    File.Copy(pathArchivo, direccionArchivoFinal);
                }


            }

            return direccionArchivoFinal;
        }
        /// <summary>
        /// Copia si es necesario los arxivos existentes en la lista.
        /// </summary>
        /// <param name="dir"></param>
        /// <param name="pathsArxius">lista de path de archivos</param>
        /// <returns>key=path final archivo, value= archivo a copiar(solo devuelve los archivos existentes los demas no estan en la lista)</returns>
        public static List<KeyValuePair<string, FileInfo>> HazSitioSiNoEsta(this DirectoryInfo dir, IList<string> pathsArxius)
        {//por provar con idRapido :) no es un hash que mira el archivo pero puede ser fiable...
            SortedList<string, FileInfo> idArxiusPerCopiar = new SortedList<string, FileInfo>();
            FileInfo fitxer;
            IEnumerator<FileInfo> fitxersCarpeta = dir.GetFiles().ObtieneEnumerador();
            TwoKeysList<string, string, FileInfo> pathsFinals = new TwoKeysList<string, string, FileInfo>();
            List<KeyValuePair<string, FileInfo>> llistaFinal = new List<KeyValuePair<string, FileInfo>>();

            List<FileInfo> archivosDuplicados = new List<FileInfo>();

            string direccioFinalArxiu;
            int contador;
            if (pathsArxius != null)
                for(int i=0;i<pathsArxius.Count;i++)
                    if (File.Exists(pathsArxius[i]))
                    {
                        fitxer = new FileInfo(pathsArxius[i]);
                        if (!idArxiusPerCopiar.ContainsKey(fitxer.IdUnicoRapido()))
                            idArxiusPerCopiar.Add(fitxer.IdUnicoRapido(), fitxer);
                        else
                            archivosDuplicados.Add(fitxer);
                    }
            foreach (var archiuACopiar in idArxiusPerCopiar)
            {//miro archivo a copiar uno a uno  para ver si se tiene que copiar o no :)

                while (fitxersCarpeta.MoveNext() && !idArxiusPerCopiar.ContainsKey(fitxersCarpeta.Current.IdUnicoRapido()))
                    ;//mira archivo por archivo de la carpeta si su hash esta en la lista de archivos a copiar
                if (idArxiusPerCopiar.ContainsKey(fitxersCarpeta.Current.IdUnicoRapido()))
                {
                    pathsFinals.Add(fitxersCarpeta.Current.FullName, fitxersCarpeta.Current.IdUnicoRapido(), fitxersCarpeta.Current);//si el arxivo esta en la carpeta pongo la ruta
                }
                else
                {
                    contador = 1;
                    direccioFinalArxiu = dir.FullName + Path.DirectorySeparatorChar + Path.GetFileName(archiuACopiar.Value.FullName);
                    while (File.Exists(direccioFinalArxiu))//mira que no coincida en nombre con ninguno
                        direccioFinalArxiu = dir.FullName + Path.DirectorySeparatorChar + Path.GetFileNameWithoutExtension(archiuACopiar.Value.FullName) + "(" + (contador++) + ")" + Path.GetExtension(archiuACopiar.Value.FullName);
                    File.Copy(archiuACopiar.Value.FullName, direccioFinalArxiu);//copia el archivo con su nuevo nombre
                    pathsFinals.Add(direccioFinalArxiu, fitxersCarpeta.Current.IdUnicoRapido(), archiuACopiar.Value);
                }
            }
            llistaFinal.AddRange(pathsFinals.Key1ValuePair());
            for (int i = 0; i < archivosDuplicados.Count; i++)
            {
                llistaFinal.Add(new KeyValuePair<string, FileInfo>(pathsFinals.GetValueWithKey2(archivosDuplicados[i].IdUnicoRapido()).FullName, archivosDuplicados[i]));
            }
            return llistaFinal;

        }
        /*Lo demas ya esta revisado :)  */
        public static Process Abrir(this DirectoryInfo dir)
        {//source https://github.com/dotnet/wpf/issues/2566
            return Process.Start(new ProcessStartInfo
            {
                FileName = "cmd",
                WindowStyle = ProcessWindowStyle.Hidden,
                UseShellExecute = false,
                CreateNoWindow = true,
                Arguments = $"/c start {dir.FullName + Path.DirectorySeparatorChar}"
            });
            //sino pongo la separacion me puede abrir un archivo con el nombre de la carpeta...
        }
        public static bool CanWrite(this DirectoryInfo dir)
        {
            bool canWrite = false;
            StreamWriter sw = null;
            string path = dir.FullName + Path.DirectorySeparatorChar + MiRandom.Next() + "Archivo.exTmp.SeTeniaDeHaberBorrado.SePuedeBorrar";
            try
            {
                sw = new StreamWriter(path, false);
                sw.WriteLine("prueba");
                canWrite = true;

            }
            catch
            {
            }
            finally
            {
                if (sw != null)
                {
                    sw.Close();
                    if (File.Exists(path))
                        File.Delete(path);
                }
            }
            return canWrite;
        }

        public static bool CanRead(this DirectoryInfo dir)
        {
            bool puedeLeer = false;
            try
            {
                dir.GetDirectories();
                puedeLeer = true;
            }
            catch
            {
            }
            return puedeLeer;
        }

    }
}
