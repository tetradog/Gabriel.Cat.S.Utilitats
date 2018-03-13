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
    }
}
