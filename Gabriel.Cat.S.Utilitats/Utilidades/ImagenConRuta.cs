
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Gabriel.Cat.S.Extension;


namespace Gabriel.Cat.S.Utilitats
{
    /// <summary>
    /// Sirve para no cargar mas de una vez la misma imagen
    /// </summary>
    public class ImagenConRuta :  IEquatable<ImagenConRuta>, IComparable<ImagenConRuta>
    {
        string ruta;
        string hash;
        static TwoKeysList<string, string, Bitmap> imagenes = new TwoKeysList<string, string, Bitmap>();
        public ImagenConRuta(string ruta)=> Ruta = ruta;

        public string Ruta
        {
            get
            {
                return ruta;
            }
            set
            {
                byte[] buffer;
                Stream str=default;
                ruta = value;
          
                if (File.Exists(Ruta))
                {
                    try
                    {
                        str = new FileInfo(Ruta).OpenRead();
                        buffer = ExtensionStream.Read(str,(int) str.Length);
                        Hash = buffer.Hash();
                    }
                    catch { throw; }
                    finally {
                        if(!Equals(str,default))
                           str.Close();
                    }
                  
                }
             
            }
        }

        public Bitmap Imagen
        {
            get
            {

                if (!imagenes.ContainsKey2(hash))
                {
                    try
                    {
                        imagenes.Add(Ruta, Hash, new Bitmap(Ruta));
                    }
                    catch
                    {
                        imagenes.Add(Ruta, Hash, new Bitmap(1, 1));
                    }
                }

                return imagenes.GetValueWithKey2(hash);

            }
            set
            {
                Ruta = String.Empty;
                Hash = value.GetBytes().Hash();
                if (!imagenes.ContainsKey2(hash))
                {

                    if (imagenes.ContainsKey1(Ruta))
                        imagenes.Remove1(Ruta);
                    imagenes.Add(Ruta, hash, value);
                }
                else
                {
                    imagenes.Remove1(Ruta);
                    Imagen = value;
                }

            }
        }

        public string Hash
        {
            get
            {
                return hash;
            }

            private set
            {

                if (!string.IsNullOrEmpty(value))
                    hash = value;
                else if (!string.IsNullOrEmpty(Ruta))
                    hash = Serializar.GetBytes(Ruta).Hash();
                else
                    hash = string.Empty;
            }
        }

        /// <summary>
        /// Libera la imagen pero si se necesita aun tiene la ruta
        /// </summary>
        public void Dispose()
        {
            imagenes.Remove1(Ruta);
        }
        #region IEquatable implementation


        public bool Equals( ImagenConRuta other)
        {
            return !Equals(other,default(ImagenConRuta)) && Equals(other.Hash, Hash);
        }
        public override string ToString() => $"{(Equals(Ruta,default)?"Sin ruta":Ruta)}";
       
        #region Equals and GetHashCode implementation
        public override bool Equals(object obj)=> Equals(obj as ImagenConRuta);


        bool IEquatable<ImagenConRuta>.Equals(ImagenConRuta other)
        {
            return Hash == other.Hash;
        }

        public override int GetHashCode()
        {
            int hashCode = 0;
            unchecked
            {
                if (ruta != null)
                    hashCode += 1000000007 * ruta.GetHashCode();

            }
            return hashCode;
        }

        #region IComparable implementation
        public int CompareTo(ImagenConRuta other)
        {
            return string.Compare(ruta, other.Ruta, StringComparison.Ordinal);
        }
        #endregion
        public static bool operator ==(ImagenConRuta lhs, ImagenConRuta rhs)
        {
            return lhs.Equals(rhs);
        }

        public static bool operator !=(ImagenConRuta lhs, ImagenConRuta rhs)
        {
            return !(lhs == rhs);
        }

        #endregion

        /// <summary>
        /// 
        /// </summary>
        /// <returns>devuelve la imagen o Resource1.sinImagen en caso de no encontrar ninguna valida</returns>
        public static ImagenConRuta ImagenRandom([NotNull]string[] rutasImg, int intentosMaximos)
        {
            int posImgRandom;
            string ruta = String.Empty;
            if (rutasImg.Length > 0)
            {
                do
                    posImgRandom = MiRandom.Next(0, rutasImg.Length);
                while (!System.IO.File.Exists(rutasImg[posImgRandom]) && intentosMaximos-- > 0);

                if (intentosMaximos > 0)
                {
                    ruta = rutasImg[posImgRandom];
                }
            }
            return new ImagenConRuta(ruta);
        }
        /// <summary>
        /// Libera todas las imagenes en memoria
        /// </summary>
        public static void DisposeAll()
        {
            imagenes.Clear();
        }
        #endregion
    }
}