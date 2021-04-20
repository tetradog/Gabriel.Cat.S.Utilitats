using Gabriel.Cat.S.Extension;
using Gabriel.Cat.S.Utilitats.ClasesDeInternet;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;


namespace Gabriel.Cat.S.Extension
{


    public static class ExtensionFileInfo
    {
        public static FileStream GetStream(this FileInfo file)
        {
            return new FileStream(file.FullName, FileMode.Open);
        }
        public static string IdUnicoLento(this FileInfo file)
        {
            return (file.Miniatura().GetBytes().Hash()) + ";" + file.IdUnicoRapido();
        }

        public static string IdUnicoRapido(this FileInfo file)
        {
            return file.Extension + ";" + file.CreationTimeUtc.Ticks + ";" + file.LastWriteTimeUtc.Ticks + ";" + file.Length;
        }
        /// <summary>
        /// Icono del programa asociado al archivo
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        public static Bitmap Icono(this FileInfo file)
        {
            Bitmap bmp;
            if (!file.Exists)
                bmp = null;//si no existe
            else
            {
                bmp = Icon.ExtractAssociatedIcon(file.FullName).ToBitmap();

            }
            return bmp;
        }
        /// <summary>
        /// Miniatura (Thumbnail Handlers) o icono en caso de no tener el archivo con medidas 250x250
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        public static Bitmap Miniatura(this FileInfo file)
        {
            return file.Miniatura(250, 250);
        }
        /// <summary>
        /// Miniatura (Thumbnail Handlers)
        /// </summary>
        /// <param name="file"></param>
        /// <param name="amplitud"></param>
        /// <param name="altura"></param>
        /// <returns> si hay algun problema devuelve null</returns>
        public static Bitmap Miniatura(this FileInfo file, int amplitud, int altura)
        {
            Bitmap bmp = null;
            ShellThumbnail thub = new ShellThumbnail();
            try
            {
                bmp = thub.GetThumbnail(file.FullName, amplitud, altura).Clone() as Bitmap;
            }
            catch
            {
                try
                {
                    bmp = file.Icono();
                }
                catch
                {
                    bmp = null;
                }
            }
            return bmp;
        }
        public static string RutaRelativa(this FileInfo file, DirectoryInfo dir)
        {
            StringBuilder ruta = new StringBuilder(file.FullName);
            ruta.Replace(dir.FullName, "");
            return ruta.ToString();
        }
        public static byte[] GetBytes(this FileInfo file)
        {
            return File.ReadAllBytes(file.FullName);
        }
        public static Process Abrir(this FileInfo file)
        {
            //https://stackoverflow.com/questions/11365984/c-sharp-open-file-with-default-application-and-parameters
            return Process.Start(new ProcessStartInfo
            {
                FileName = "explorer",
                WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden,
                UseShellExecute = false,
                CreateNoWindow = true,
                Arguments = $"\"{file.FullName}\""
            });

        }
        /// <summary>
        /// Calcula el Hash del archivo
        /// </summary>
        /// <param name="file"></param>
        /// <returns>devuelve null si el archivo no existe</returns>
        public static string Hash(this FileInfo file)
        {
            string hash;
            if (file.Exists)
            {
                FileStream stream = file.GetStream();
                hash = stream.GetAllBytes().Hash();
                stream.Close();
            }
            else
                hash = null;
            return hash;
        }
        public static bool HashEquals(this FileInfo file, string hash)
        {
            return ComparaHash(file.Hash(), hash);
        }
        public static bool HashEquals(this FileInfo file1, FileInfo file2)
        {
            return ComparaHash(file1.Hash(), file2.Hash());
        }
        private static bool ComparaHash(string tmpHash, string tmpNewHash)
        {
            bool bEqual = false;
            int i;
            if (tmpNewHash.Length == tmpHash.Length)
            {
                i = 0;
                while ((i < tmpNewHash.Length) && (tmpNewHash[i] == tmpHash[i]))
                {
                    i += 1;
                }
                if (i == tmpNewHash.Length)
                {
                    bEqual = true;
                }
            }

            return bEqual;
        }
        public static void WriteAllBytes(this FileInfo file, Stream strData)
        {
            file.WriteAllBytes(strData.GetAllBytes());
        }
        public static void WriteAllBytes(this FileInfo file, byte[] data)
        {
            File.WriteAllBytes(file.FullName, data);
        }

    }
}
