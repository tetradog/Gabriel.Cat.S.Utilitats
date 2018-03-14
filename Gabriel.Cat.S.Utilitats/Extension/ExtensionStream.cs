using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Gabriel.Cat.S.Extension
{
   public static class ExtensionStream
    {
        public static byte[] Read(this Stream str,int lenght)
        {
            byte[] bytesToRead = new byte[lenght];
            str.Read(bytesToRead, (int)str.Position, lenght);
            return bytesToRead;
        }
        public static bool EndOfStream(this Stream str)
        {
            return str.Position == str.Length;
        }
        public static byte[] GetAllBytes(this Stream str)
        {
            byte[] bytes = new byte[str.Length];
            long position = str.Position;
            str.Position = 0;
            str.Read(bytes, 0, bytes.Length);
            str.Position = position;
            return bytes;
        }
    }
}
