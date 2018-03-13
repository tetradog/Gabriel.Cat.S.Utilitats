using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Gabriel.Cat.S.Extension;
namespace Gabriel.Cat.S.Utilitats
{
    /// <summary>
	/// Sirve para convertir a byte[] los objetos compatibles y de byte[] al objeto en cuestion
	/// </summary>
	public class Serializar
    {
        #region AssemblyNames
        //AssemblyName primero poner la string como constante
        public static readonly string StringAssemblyName = typeof(string).AssemblyQualifiedName;
        public static readonly string IntAssemblyName = typeof(int).AssemblyQualifiedName;
        public static readonly string ShortAssemblyName = typeof(short).AssemblyQualifiedName;
        public static readonly string LongAssemblyName = typeof(long).AssemblyQualifiedName;
        public static readonly string UShortAssemblyName = typeof(ushort).AssemblyQualifiedName;
        public static readonly string ULongAssemblyName = typeof(ulong).AssemblyQualifiedName;
        public static readonly string DoubleAssemblyName = typeof(double).AssemblyQualifiedName;
        public static readonly string CharAssemblyName = typeof(char).AssemblyQualifiedName;
        public static readonly string FloatAssemblyName = typeof(float).AssemblyQualifiedName;
        public static readonly string DateTimeAssemblyName = typeof(DateTime).AssemblyQualifiedName;
        public static readonly string ByteArrayAssemblyName = typeof(byte[]).AssemblyQualifiedName;
        public static readonly string ByteAssemblyName = typeof(byte).AssemblyQualifiedName;
        public static readonly string BooleanAssemblyName = typeof(bool).AssemblyQualifiedName;
        // public static readonly string BitmapAssemblyName = typeof(Bitmap).AssemblyQualifiedName;
        public static readonly string UIntAssemblyName = typeof(uint).AssemblyQualifiedName;
        //public static readonly string PointAssemblyName = typeof(Point).AssemblyQualifiedName;
        // public static readonly string ColorAssemblyName = typeof(Color).AssemblyQualifiedName;

        //por comprovar
        public static readonly string TimeSpanAssemblyName = typeof(TimeSpan).AssemblyQualifiedName;
        /// <summary>
        /// Necesitaba tener uno para que la lista de AsseblyQualifiedNameTiposMicrosoft coincida con la enumeracion
        /// </summary>
        public static readonly string NullAssemblyName = "NULL.AssemblyName";
        //mis clases
        // public static readonly string PointZAssemblyName = typeof(PointZ).AssemblyQualifiedName;
        //mientras no le cambie esta info sera valida...
        //se añade la constante en la lista :D
        /// <summary>
        /// Se puede usar la enumeracion para obtener su nombre :D
        /// </summary>
        public static readonly string[] AsseblyQualifiedName = new String[] {
            StringAssemblyName,
          //  BitmapAssemblyName,
            NullAssemblyName,
            ByteAssemblyName,
            BooleanAssemblyName,
            ShortAssemblyName,
            UShortAssemblyName,
            UIntAssemblyName,
            IntAssemblyName,
            LongAssemblyName,
            ULongAssemblyName,
            DoubleAssemblyName,
            FloatAssemblyName,
            CharAssemblyName,
            DateTimeAssemblyName,
           // PointAssemblyName,
           // PointZAssemblyName,
           // ColorAssemblyName,
            TimeSpanAssemblyName
        };
        #endregion
        //Si se añaden mas se tiene que dar de alta aqui :D y en el metodo ToTipoAceptado y GetBytes(TipoAceptado,Object)
        public enum TiposAceptados
        {
            //lo tipos anteriores a Null son variables en su longitud
            String,
            //  Bitmap,
            //los tipos que vayan apartir de aqui son tipos con longitud fija
            Null,
            Byte,
            Bool,
            Short,
            UShort,
            Int,
            UInt,
            Long,
            ULong,
            Double,
            Float,
            Char,
            DateTime,
            // Point,
            // PointZ,
            //Color,
            TimeSpan
        }

        static readonly TwoKeysList<string, string, string> dicTiposAceptadoAssembly;
        static Serializar()
        {
            TiposAceptados[] tipos = (TiposAceptados[])Enum.GetValues(typeof(TiposAceptados));
            dicTiposAceptadoAssembly = new TwoKeysList<string, string, string>();
            for (int i = 0; i < AsseblyQualifiedName.Length; i++)
            {
                dicTiposAceptadoAssembly.Add(tipos[i].ToString(), AsseblyQualifiedName[i], "");
            }
        }
        /// <summary>
        /// Obtiene el tipo si pertenece a la lista de tipos aceptados, en caso de ser null se devolvera TiposAceptados.Null si no esta en la lista lanza una excepción TypeNotSerializableException
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static TiposAceptados GetType(object obj)
        {
            return obj != null ? AssemblyToEnumTipoAceptado(obj.GetType().AssemblyQualifiedName) : TiposAceptados.Null;
        }
        public static TiposAceptados AssemblyToEnumTipoAceptado(string assemblyName)
        {
            if (!dicTiposAceptadoAssembly.ContainsKey2(assemblyName))
                throw new ArgumentException("assembltName");

            return (TiposAceptados)Enum.Parse(typeof(TiposAceptados), dicTiposAceptadoAssembly.GetTkey1WhithTkey2(assemblyName));
        }


        //la clase es para convertir a byte[] los objetos
        #region GetBytes
        public static byte[] GetBytes(IEnumerable<Object> objsTipoAceptado, bool ignoreObjNotSerializable = false)
        {
            List<byte> bytesList = new List<byte>();
            foreach (Object obj in objsTipoAceptado)
            {
                try
                {
                    bytesList.AddRange(GetBytes(obj));
                }
                catch
                {
                    if (!ignoreObjNotSerializable)
                        throw;
                }
            }
            return bytesList.ToArray();
        }
        public static byte[] GetBytes(Object objTipoAceptado)
        {
            TiposAceptados tipo = GetType(objTipoAceptado);
            byte[] bytes = new byte[] { };
            try
            {
                switch (tipo)
                {
                    /*  case TiposAceptados.Point:
                          bytes = GetBytes((Point)objTipoAceptado);
                          break;
                      case TiposAceptados.PointZ:
                          bytes = GetBytes((PointZ)objTipoAceptado);
                          break;
                          */
                    case TiposAceptados.Bool:
                        bytes = GetBytes((bool)objTipoAceptado);
                        break;
                    case TiposAceptados.Short:
                        bytes = GetBytes((short)objTipoAceptado);
                        break;
                    case TiposAceptados.UShort:
                        bytes = GetBytes((ushort)objTipoAceptado);
                        break;
                    case TiposAceptados.Int:
                        bytes = GetBytes((int)objTipoAceptado);
                        break;
                    case TiposAceptados.UInt:
                        bytes = GetBytes((uint)objTipoAceptado);
                        break;
                    case TiposAceptados.Long:
                        bytes = GetBytes((long)objTipoAceptado);
                        break;
                    case TiposAceptados.ULong:
                        bytes = GetBytes((ulong)objTipoAceptado);
                        break;
                    case TiposAceptados.Double:
                        bytes = GetBytes((double)objTipoAceptado);
                        break;
                    case TiposAceptados.Float:
                        bytes = GetBytes((float)objTipoAceptado);
                        break;
                    case TiposAceptados.Char:
                        bytes = GetBytes((char)objTipoAceptado);
                        break;
                    case TiposAceptados.String:
                        bytes = GetBytes((string)objTipoAceptado);
                        break;
                    /* case TiposAceptados.Bitmap:
                         bytes = GetBytes((Bitmap)objTipoAceptado);
                         break;
                         */
                    case TiposAceptados.DateTime:
                        bytes = GetBytes((DateTime)objTipoAceptado);
                        break;
                    case TiposAceptados.Byte:
                        bytes = GetBytes((byte)objTipoAceptado);
                        break;
                    case TiposAceptados.Null:
                        bytes = new byte[] { 0x00 };
                        break;
                    /* case TiposAceptados.Color:
                         bytes = GetBytes((Color)objTipoAceptado);
                         break;
                         */
                    case TiposAceptados.TimeSpan:
                        bytes = GetBytes((TimeSpan)objTipoAceptado);
                        break;
                }
            }
            catch
            {
                throw new Exception("El objeto no es del tipo indicado como parametro");
            }
            return bytes;
        }

        //aqui empieza la serializacion de cada tipo
        /*    public static byte[] GetBytes(System.Drawing.Color color)
            {
                return new byte[] { color.A, color.R, color.G, color.B };
            }
            public static byte[] GetBytes(Point point)
            {
                return GetBytes(point.X).AddArray(GetBytes(point.Y));
            }
            public static byte[] GetBytes(PointZ point)
            {
                return GetBytes(point.X).AddArray(GetBytes(point.Y), GetBytes(point.Z));
            }
            //mirar de poder serializar null
            public static byte[] GetBytes(Bitmap img)
            {
                return GetBytes(img, img.RawFormat);
            }
            public static byte[] GetBytes(Bitmap img, System.Drawing.Imaging.ImageFormat formato)
            {
                return img.ToStream(formato).GetAllBytes();
            }
    */
        public static byte[] GetBytes(string str)
        {
            byte[] bytes = new byte[str.Length * sizeof(char)];
            unsafe
            {
                char* ptrStr, ptrBytes;
                fixed (char* ptStr = str)
                {
                    fixed (byte* ptBytes = bytes)
                    {
                        ptrStr = ptStr;
                        ptrBytes = (char*)ptBytes;
                        for (int i = 0, f = str.Length; i < f; i++)
                        {
                            *ptrBytes = *ptrStr;
                            ptrStr++;
                            ptrBytes++;
                        }


                    }
                }
            }

            return bytes;
        }


        public static byte[] GetBytes(char caracter)
        {
            return GetBytes(caracter + "");
        }
        /// <summary>
        /// Serializa los Ticks del Datetime y ocupa los 8 bytes de un long
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static byte[] GetBytes(DateTime data)
        {
            return GetBytes(data.Ticks);
        }
        public static byte[] GetBytes(int numero)
        {
            return BitConverter.GetBytes(numero);
        }
        public static byte[] GetBytes(ulong numero)
        {
            return BitConverter.GetBytes(numero);
        }
        public static byte[] GetBytes(long numero)
        {
            return BitConverter.GetBytes(numero);
        }
        public static byte[] GetBytes(uint numero)
        {
            return BitConverter.GetBytes(numero);
        }
        public static byte[] GetBytes(short numero)
        {
            return BitConverter.GetBytes(numero);
        }
        public static byte[] GetBytes(double numero)
        {
            return BitConverter.GetBytes(numero);
        }
        public static byte[] GetBytes(float numero)
        {
            return BitConverter.GetBytes(numero);
        }
        public static byte[] GetBytes(ushort numero)
        {
            return BitConverter.GetBytes(numero);
        }
        public static byte[] GetBytes(bool bolean)
        {
            return BitConverter.GetBytes(bolean);
        }
        public static byte[] GetBytes(byte byteToArray)
        {
            return new byte[] { byteToArray };
        }
        public static byte[] GetBytes(TimeSpan time)
        {
            return GetBytes(time.Ticks);
        }

        #endregion
        #region To
        /// <summary>
        /// Convierte a obj los bytes al tipo especificado
        /// </summary>
        /// <param name="tipo"></param>
        /// <param name="bytesTipo"></param>
        /// <returns>si no se puede devuelve null</returns>
        public static Object ToTipoAceptado(TiposAceptados tipo, byte[] bytesTipo)
        {
            Object obj = null;
            Stream str = new MemoryStream(bytesTipo);
            obj = ToObjetoAceptado(tipo, str);
            str.Dispose();
            return obj;
        }
        public static object ToObjetoAceptado(TiposAceptados objHaLeer, Stream ms)
        {
            object obj = null;
            switch (objHaLeer)
            {
                case TiposAceptados.Null:
                    break;
                case TiposAceptados.Byte:
                    obj = ms.ReadByte();
                    break;
                case TiposAceptados.Bool:
                    obj = ToBoolean(ms.Read(sizeof(bool)));
                    break;
                case TiposAceptados.Short:
                    obj = ToShort(ms.Read(sizeof(short)));
                    break;
                case TiposAceptados.UShort:
                    obj = ToUShort(ms.Read(sizeof(ushort)));
                    break;
                case TiposAceptados.Int:
                    obj = ToInt(ms.Read(sizeof(int)));
                    break;
                case TiposAceptados.UInt:
                    obj = ToUInt(ms.Read(sizeof(uint)));
                    break;
                case TiposAceptados.Long:
                    obj = ToLong(ms.Read(sizeof(long)));
                    break;
                case TiposAceptados.ULong:
                    obj = ToULong(ms.Read(sizeof(ulong)));
                    break;
                case TiposAceptados.Double:
                    obj = ToDouble(ms.Read(sizeof(double)));
                    break;
                case TiposAceptados.Float:
                    obj = ToFloat(ms.Read(sizeof(float)));
                    break;
                case TiposAceptados.Char:
                    obj = ToChar(ms.Read(sizeof(char)));
                    break;
                case TiposAceptados.DateTime:
                    obj = ToDateTime(ms.Read(sizeof(long)));
                    break;
                /*   case TiposAceptados.Point:
                       obj = ToPoint(ms.Read(sizeof(int) * 2));
                       break;
                   case TiposAceptados.PointZ:
                       obj = ToPointZ(ms.Read(sizeof(int) * 3));
                       break;
                   case TiposAceptados.String:
                       obj = ToString(ms.Read(ToLong(ms.Read(sizeof(long)))));
                       break;
                   case TiposAceptados.Bitmap:
                       obj = ToBitmap(ms.Read(ToLong(ms.Read(sizeof(long)))));
                       break;
                   case TiposAceptados.Color:
                       obj = ToColor(ms.Read(sizeof(int)));
                       break;
                       */
                case TiposAceptados.TimeSpan:
                    obj = ToTimeSpan(ms.Read(sizeof(long)));
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
            return obj;
        }
        public static T ToTipoAceptadoTipado<T>(TiposAceptados tipo, byte[] bytesTipo)
        {
            return (T)ToTipoAceptado(tipo, bytesTipo);
        }
        public static T ToObjetoAceptadoTipado<T>(TiposAceptados objHaLeer, Stream ms)
        {
            return (T)ToObjetoAceptado(objHaLeer, ms);
        }

        public static Object ToTipoAceptado(string assemblyQualifiedName, byte[] bytesTipo)
        {
            return ToTipoAceptado(assemblyQualifiedName, new MemoryStream(bytesTipo));
        }
        public static Object ToTipoAceptado(string assemblyQualifiedName, Stream bytesTipo)
        {
            return ToObjetoAceptado(AssemblyToEnumTipoAceptado(assemblyQualifiedName), bytesTipo);
        }



        #region Desserializar Medida Fija
        /*    public static Color ToColor(byte[] bytesObj)
            {
                if (bytesObj.Length != 4)
                    throw new ArgumentException("Un color consta de 4 bytes ARGB");
                return System.Drawing.Color.FromArgb(bytesObj[0], bytesObj[1], bytesObj[2], bytesObj[3]);
            }
            public static PointZ ToPointZ(byte[] bytesObj)
            {
                return new PointZ(ToPoint(bytesObj), ToInt(bytesObj.SubArray(8)));
            }

            public static Point ToPoint(byte[] bytesObj)
            {
                return new Point(ToInt(bytesObj), ToInt(bytesObj.SubArray(4)));
            }
            */
        public static bool ToBoolean(byte[] boolean)
        {
            return BitConverter.ToBoolean(boolean, 0);
        }
        public static int ToInt(byte[] numero)
        {
            return BitConverter.ToInt32(numero, 0);
        }
        public static ulong ToULong(byte[] numero)
        {
            return BitConverter.ToUInt64(numero, 0);
        }
        public static long ToLong(byte[] numero)
        {
            return BitConverter.ToInt64(numero, 0);
        }
        public static uint ToUInt(byte[] numero)
        {
            return BitConverter.ToUInt32(numero, 0);
        }
        public static short ToShort(byte[] numero)
        {
            return BitConverter.ToInt16(numero, 0);
        }
        public static double ToDouble(byte[] numero)
        {
            return BitConverter.ToDouble(numero, 0);
        }
        public static float ToFloat(byte[] numero)
        {
            return BitConverter.ToSingle(numero, 0);
        }
        public static ushort ToUShort(byte[] numero)
        {
            return BitConverter.ToUInt16(numero, 0);
        }
        public static DateTime ToDateTime(byte[] data)
        {
            return new DateTime(ToLong(data));
        }
        public static char ToChar(byte[] caracter)
        {
            return ToString(caracter)[0];
        }
        public static TimeSpan ToTimeSpan(byte[] time)
        {
            return new TimeSpan(ToLong(time));
        }
        #endregion
        #region Deserializar medias dynamicas
        /*   /// <summary>
           /// bytes img
           /// </summary>
           /// <param name="bytesImgSerializada"></param>
           /// <returns></returns>
           public static Bitmap ToBitmap(byte[] bytesImgSerializada)
           {
               Bitmap imgDeserializada = null;
               try
               {
                   imgDeserializada = new Bitmap(new MemoryStream(bytesImgSerializada));
               }
               catch
               {
               }
               return imgDeserializada;

           }
   */

        public static string ToString(byte[] bytes)
        {
            if (bytes.Length % 2 != 0)
                throw new ArgumentException("los caracteres ocupan 2 bytes y tiene que ser por lo tanto par la longitud y es '" + bytes.Length + "'");
            char[] chars = new char[bytes.Length / sizeof(char)];
            unsafe
            {
                char* ptrStr, ptrBytes;
                fixed (char* ptStr = chars)
                {
                    fixed (byte* ptBytes = bytes)
                    {
                        ptrStr = ptStr;
                        ptrBytes = (char*)ptBytes;
                        for (long i = 0, f = chars.Length; i < f; i++)
                        {
                            *ptrStr = *ptrBytes;
                            ptrStr++;
                            ptrBytes++;
                        }


                    }
                }
            }
            return new string(chars);
        }
        #endregion

        #endregion

        public class TypeNotSerializableException : Exception
        {
            public TypeNotSerializableException(object obj) : this(obj.GetType().AssemblyQualifiedName) { }
            public TypeNotSerializableException(string assemblyQualifiedName) : base(String.Format("the object type of \"{ 0}\" can not be serialitzed by Serializar class because miss serialitzacion method ", assemblyQualifiedName)) { }
        }
    }
}