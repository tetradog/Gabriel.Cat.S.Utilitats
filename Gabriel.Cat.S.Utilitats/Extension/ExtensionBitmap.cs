
using Gabriel.Cat.S.Utilitats;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Text;

namespace Gabriel.Cat.S.Extension
{
    public  static unsafe class BitmapExtension
    {
        delegate byte[] MetodoColor(byte[] colorValue, byte[] colorKey);

        public static BitmapAnimated ToAnimatedBitmap(this IList<Bitmap> bmpsToAnimate, bool repetirSiempre = true)
        {
            return bmpsToAnimate.ToAnimatedBitmap(repetirSiempre, 500);
        }
        public static BitmapAnimated ToAnimatedBitmap(this IList<Bitmap> bmpsToAnimate, bool repetirSiempre = true, params int[] delay)
        {
            return new BitmapAnimated(bmpsToAnimate, delay) { AnimarCiclicamente = repetirSiempre };
        }
        #region Por acabar más adelante(cuando lo necesite)
        //public static Bitmap RandomPixels(this Bitmap imgRandom)
        //{
        //    const int MAXPRIMERO = 19;
        //    return imgRandom.RandomPixels(Convert.ToInt32(Math.Sqrt(imgRandom.Width) % MAXPRIMERO));
        //}
        //public static Bitmap RandomPixels(this Bitmap imgRandom, int cuadradosPorLinea)
        //{
        //    //hay un bug y no lo veo... no hace cuadrados...
        //    unsafe
        //    {
        //        imgRandom.TrataBytes((MetodoTratarBytePointer)((bytesImg) =>
        //        {
        //            const int PRIMERODEFAULT = 13;//al ser un numero Primo no hay problemas
        //            System.Drawing.Color[] cuadrados;
        //            System.Drawing.Color colorActual;
        //            int a = 3, r = 0, g = 1, b = 2;
        //            int lenght = imgRandom.LengthBytes();
        //            int pixel = imgRandom.IsArgb() ? 4 : 3;
        //            int pixelsLineasHechas;
        //            int sumaX;
        //            int numPixeles;
        //            int posicionCuadrado = 0;
        //            if (cuadradosPorLinea < 1)
        //                cuadradosPorLinea = PRIMERODEFAULT;
        //            else
        //                cuadradosPorLinea = cuadradosPorLinea.DamePrimeroCercano();
        //            numPixeles = imgRandom.Width / cuadradosPorLinea;
        //            numPixeles = numPixeles.DamePrimeroCercano();
        //            cuadrados = DamePixelesRandom(cuadradosPorLinea);
        //            colorActual = cuadrados[posicionCuadrado];
        //            for (int y = 0, xMax = imgRandom.Width * pixel; y < imgRandom.Height; y++)
        //            {
        //                pixelsLineasHechas = y * xMax;
        //                if (y % numPixeles == 0)
        //                {
        //                    cuadrados = DamePixelesRandom(cuadradosPorLinea);
        //                }
        //                for (int x = 0; x < xMax; x += pixel)
        //                {
        //                    if (x % numPixeles == 0)
        //                    {
        //                        colorActual = cuadrados[++posicionCuadrado % cuadrados.Length];
        //                    }
        //                    sumaX = pixelsLineasHechas + x;
        //                    if (pixel == 4)
        //                    {
        //                        bytesImg[sumaX + a] = byte.MaxValue;
        //                    }
        //                    bytesImg[sumaX + r] = colorActual.R;
        //                    bytesImg[sumaX + g] = colorActual.G;
        //                    bytesImg[sumaX + b] = colorActual.B;
        //                }
        //            }
        //        })
        //                            );
        //    }
        //    return imgRandom;
        //}

        #endregion
        private static IEnumerable<System.Drawing.Color> GetPixelesRandom(this int numPixeles)
        {
            for (int i = 0; i < numPixeles; i++)
                yield return System.Drawing.Color.FromArgb(MiRandom.Next());
      
        }
        public static byte[] GetBytes(this Bitmap bmp)
        {
            BitmapData bmpData = bmp.LockBits();
            // Get the address of the first line.
            IntPtr ptr = bmpData.Scan0;

            // Declare an array to hold the bytes of the bitmap.
            int bytes = Math.Abs(bmpData.Stride) * bmp.Height;

            byte[] rgbValues = new byte[bytes];

            // Copy the RGB values into the array.
            ptr.CopyTo(rgbValues);
            // Unlock the bits.
            bmp.UnlockBits(bmpData);
            return rgbValues;
        }
        public static void SetBytes(this Bitmap bmp, byte[] rgbValues)
        {
            BitmapData bmpData = bmp.LockBits();
            // Get the address of the first line.
            IntPtr ptr = bmpData.Scan0;

            // Declare an array to hold the bytes of the bitmap.
            int bytes = Math.Abs(bmpData.Stride) * bmp.Height;
            if (bytes != rgbValues.Length)
                throw new Exception("La array de bytes no se corresponde a la imagen");

            // Copy the RGB values back to the bitmap
            rgbValues.CopyTo(ptr);

            // Unlock the bits.
            bmp.UnlockBits(bmpData);

        }
        public static BitmapData LockBits(this Bitmap bmp)
        {
            return bmp.LockBits(new Rectangle(0, 0, bmp.Width, bmp.Height), System.Drawing.Imaging.ImageLockMode.ReadWrite, bmp.PixelFormat);
        }
        public static MemoryStream ToStream(this Bitmap bmp, bool useRawFormat = false)
        {
            MemoryStream memory;
            ImageFormat format = useRawFormat ? bmp.RawFormat : bmp.IsArgb() ? ImageFormat.Png : ImageFormat.Jpeg;//no se porque aun pero no funciona...mejor pasarla a png
            memory = ToStream(bmp, format);
            return memory;

        }
        public static MemoryStream ToStream(this Bitmap bmp, ImageFormat format)
        {
            MemoryStream stream = new MemoryStream();
            string path;
            FileStream fs;
            try
            {
                new Bitmap(bmp).Save(stream, format);
            }
            catch
            {
                path = System.IO.Path.GetRandomFileName();
                format = bmp.IsArgb() ? ImageFormat.Png : ImageFormat.Jpeg;
                new Bitmap(bmp).Save(path, format);
                fs = File.OpenRead(path);
                new Bitmap(fs).Save(stream, format);
                fs.Close();
                File.Delete(path);
            }
            return new MemoryStream(stream.GetAllBytes());
        }

        public static void TrataBytes(this Bitmap bmp, MetodoTratarUnmanagedTypeArray<byte> metodo)
        {
            BitmapData bmpData = bmp.LockBits();
            // Get the address of the first line.
            IntPtr ptr = bmpData.Scan0;

            // Declare an array to hold the bytes of the bitmap.
            int bytes = System.Math.Abs(bmpData.Stride) * bmp.Height;

            byte[] rgbValues = new byte[bytes];

            // Copy the RGB values into the array.
            ptr.CopyTo(rgbValues);
            if (metodo != null)
            {
                metodo(rgbValues);//se modifican los bytes :D
                                  // Copy the RGB values back to the bitmap
                rgbValues.CopyTo(ptr);
            }
            // Unlock the bits.
            bmp.UnlockBits(bmpData);

        }
        public static unsafe void TrataBytes(this Bitmap bmp, MetodoTratarUnmanagedTypePointer<byte> metodo)
        {

            BitmapData bmpData = bmp.LockBits();
            // Get the address of the first line.

            IntPtr ptr = bmpData.Scan0;
            if (!Equals(metodo,default))
            {
                metodo((byte*)ptr.ToPointer());//se modifican los bytes :D
            }
            // Unlock the bits.
            bmp.UnlockBits(bmpData);

        }
        public static int LengthBytes(this Bitmap bmp)
        {
            int multiplicadorPixel = bmp.IsArgb() ? 4 : 3;
            return bmp.Height * bmp.Width * multiplicadorPixel;
        }
        public static bool IsArgb(this Bitmap bmp)
        {
            return bmp.PixelFormat.IsArgb();
        }

       
        #region BitmapImportado
        /// <summary>
        /// Recorta una imagen en formato Bitmap
        /// </summary>
        /// <param name="localizacion">localizacion de la esquina izquierda de arriba</param>
        /// <param name="tamaño">tamaño del rectangulo</param>
        /// <param name="bitmapARecortar">bitmap para recortar</param>
        /// <returns>bitmap resultado del recorte</returns>
        public static Bitmap Recortar(this Bitmap bitmapARecortar, Point localizacion, Size tamaño)
        {

            Rectangle rect = new Rectangle(localizacion.X, localizacion.Y, tamaño.Width, tamaño.Height);
            Bitmap cropped = bitmapARecortar.Clone(rect, bitmapARecortar.PixelFormat);
            return cropped;

        }
        public static Bitmap Escala(this Bitmap imgAEscalar, float escala)
        {
            return Resize(imgAEscalar, new Size(Convert.ToInt32(imgAEscalar.Size.Width * escala), Convert.ToInt32(imgAEscalar.Size.Height * escala)));
        }
        public static Bitmap Resize(this Bitmap imgToResize, Size size)
        {
            Bitmap bmpResized;
            try
            {
                bmpResized = new Bitmap(size.Width, size.Height);
                using (Graphics g = Graphics.FromImage((System.Drawing.Image)bmpResized))
                {
                    g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                    g.DrawImage(imgToResize, 0, 0, size.Width, size.Height);
                }

            }
            catch
            {
                bmpResized = imgToResize;
            }

            return bmpResized;
        }




        public static Color[,] GetColorMatriu(this Bitmap bmp)
        {
            Color[,] matriz = new Color[bmp.Width, bmp.Height];
            unsafe
            {
                bmp.TrataBytes(((MetodoTratarUnmanagedTypePointer<byte>)((ptrBytesBmp) =>
                {

                    Gabriel.Cat.S.Utilitats.V2.Color* ptrColoresBmp = (Gabriel.Cat.S.Utilitats.V2.Color*)ptrBytesBmp;
                    for (int y = 0, yFinal = bmp.Width, xFinal = bmp.Height; y < yFinal; y++)
                        for (int x = 0; x < xFinal; x++)
                        {
                          
                            matriz[x, y] = *ptrColoresBmp;
                            ptrColoresBmp++;
                        }

                })));
            }
            return matriz;
        }
        public static Bitmap GetBitmap(this Color[,] array)
        {
            Bitmap bmp = new Bitmap(array.GetLength(DimensionMatriz.X), array.GetLength(DimensionMatriz.Y));
            unsafe
            {
                bmp.TrataBytes(((MetodoTratarUnmanagedTypePointer<byte>)((ptrBytesBmp) =>
                {
              
                    Gabriel.Cat.S.Utilitats.V2.Color* ptrColoresBmp = (Gabriel.Cat.S.Utilitats.V2.Color*)ptrBytesBmp;
                    for (int y = 0, yFinal = array.GetLength((int)DimensionMatriz.Y), xFinal = array.GetLength((int)DimensionMatriz.X); y < yFinal; y++)
                        for (int x = 0; x < xFinal; x++)
                        {
                            *ptrColoresBmp = array[x, y];
                            ptrColoresBmp++;
                        }

                })));
            }
            return bmp;
        }
        public static byte[,] GetMatriuBytes(this Bitmap bmp)
        {
            byte[] bytesArray = bmp.GetBytes();
            return bytesArray.ToMatriu(bmp.Height, DimensionMatriz.Y);
        }
        public static void SetMatriuBytes(this Bitmap bmp, byte[,] matriuBytes)
        {
            if (bmp.Height * bmp.Width * (bmp.IsArgb() ? 4 : 3) != matriuBytes.GetLength(DimensionMatriz.Y) * matriuBytes.GetLength(DimensionMatriz.X))
                throw new Exception("La matriz no tiene las medidas de la imagen");
            unsafe
            {
                bmp.TrataBytes(((MetodoTratarUnmanagedTypePointer<byte>)((ptrBytesBmp) =>
                {
                    byte* ptBytesBmp = ptrBytesBmp;
                    for (long y = 0, yFinal = matriuBytes.GetLongLength((int)DimensionMatriz.Y), xFinal = matriuBytes.GetLongLength((int)DimensionMatriz.X); y < yFinal; y++)
                        for (long x = 0; x < xFinal; x++)
                        {
                            *ptBytesBmp = matriuBytes[x, y];
                            ptBytesBmp++;
                        }


                })));
            }
        }


        public static Bitmap Clon(this Bitmap bmp, PixelFormat format=PixelFormat.Format32bppArgb)
        {
            return bmp.Clone(bmp.GetRectangle(), format);
        }
        public static Rectangle GetRectangle(this Bitmap bmp,Point location=default)
        {
            return new Rectangle(location, bmp.Size);
        }
        #endregion

        public static void CambiarPixel(this Bitmap bmp, Color aEnontrar, Color aDefinir)
        {
            bmp.CambiarPixel(new KeyValuePair<Color, Color>[] { new KeyValuePair<Color, Color>(aEnontrar, aDefinir) });
        }
        public static void CambiarPixel(this Bitmap bmp, IList<KeyValuePair<Color, Color>> colorsKeyValue)
        {
            MetodoColor metodo = (colorValue, colorKey) =>
            {
                return colorValue;
            };
            ICambiaPixel(bmp, colorsKeyValue, metodo);
        }
        public static void EfectoPixel(this Bitmap bmp, Color aMezclarConTodos, bool saltarsePixelsTransparentes = true)
        {
            const byte TRANSPARENTE = 0x00;
            const int TOTALARGBBYTES = 4;
            int incremento = bmp.IsArgb() ? 4 : 3;
            int aux;
            bool mezclar = true;
          
            unsafe
            {
                bmp.TrataBytes(((MetodoTratarUnmanagedTypePointer<byte>)((ptrbyteArray) =>
                {
                    byte* ptByteArray = ptrbyteArray;
                    for (int i = 0, iFinal = bmp.LengthBytes(); i < iFinal; i += incremento)
                    {
                        if (incremento == TOTALARGBBYTES)
                        {
                            if (saltarsePixelsTransparentes)
                                mezclar = *ptByteArray != TRANSPARENTE;
                            if (mezclar)
                            {
                                //MEZCLO LA A
                                aux = *ptByteArray + aMezclarConTodos.A;
                                if (aux > 255) aux = 255;
                                *ptByteArray = (byte)aux;
                                ptByteArray++;
                            }
                        }
                        if (mezclar)
                        {
                            //MEZCLO LA R
                            aux = *ptByteArray + aMezclarConTodos.R;
                            if (aux > 255) aux = 255;
                            *ptByteArray = (byte)aux;
                            ptByteArray++;
                            //MEZCLO LA G
                            aux = *ptByteArray + aMezclarConTodos.G;
                            if (aux > 255) aux = 255;
                            *ptByteArray = (byte)aux;
                            ptByteArray++;
                            //MEZCLO LA B
                            aux = *ptByteArray + aMezclarConTodos.B;
                            if (aux > 255) aux = 255;
                            *ptByteArray = (byte)aux;
                            ptByteArray++;
                        }
                    }
                })));
            }
        }

        static void ICambiaPixel(Bitmap bmp, IList<KeyValuePair<Color, Color>> colorsKeyValue, MetodoColor metodo)
        {
            const byte AOPACA = 0xFF;
            const int TOTALBYTESCOLOR = 4;
            DiccionarioColor2 diccionario = new DiccionarioColor2(colorsKeyValue);
            byte[] colorLeido;
            byte[] colorObtenido;
           
            int incremento = bmp.IsArgb() ? 4 : 3;
            unsafe
            {
                bmp.TrataBytes(((MetodoTratarUnmanagedTypePointer<byte>)((ptrBytesBmp) =>
                {
                    byte* ptColorLeido, ptColorObtenido;
                    byte* ptBytesBmp = ptrBytesBmp;
                    for (int i = 0, iFin = bmp.LengthBytes(); i < iFin; i += incremento)
                    {
                        colorLeido = new byte[4];
                        fixed (byte* ptrColorLeido = colorLeido)
                        {
                            ptColorLeido = ptrColorLeido;
                            if (incremento == TOTALBYTESCOLOR)
                            {
                                *ptColorLeido = *ptBytesBmp;
                                ptBytesBmp++;
                            }
                            else
                            {
                                *ptColorLeido = AOPACA;

                            }
                            ptColorLeido++;
                            for (int j = 1; j < incremento; j++)
                            {
                                *ptColorLeido = *ptBytesBmp;
                                ptBytesBmp++;
                                ptColorLeido++;
                            }
                            ptBytesBmp -= incremento;//vuelvo a poner el puntero al principio del color para sobreescribirlo con el nuevo
                        }

                        colorObtenido = metodo(diccionario.ObtenerPrimero(colorLeido), colorLeido);
                        if (colorObtenido != null)
                        {
                            fixed (byte* ptrColorObtenido = colorObtenido)
                            {
                                ptColorObtenido = ptrColorObtenido;
                                for (int j = 0; j < incremento; j++)
                                {
                                    *ptBytesBmp = *ptColorObtenido;
                                    ptBytesBmp++;
                                    ptColorObtenido++;
                                }


                            }
                        }

                    }
                })));
            }
        }

       

    }
}