using System;
using System.Collections.Generic;
using System.Text;

namespace Gabriel.Cat.S.Extension
{
   public static class ExtensionIntPtr
    {
        public static void CopyTo(this IntPtr ptr, byte[] destino, int startIndex = 0)
        {
            System.Runtime.InteropServices.Marshal.Copy(ptr, destino, startIndex, destino.Length);
        }
        public static byte[] CopyTo(this IntPtr ptr, int lenght, int startIndex = 0)
        {
            byte[] destino = new byte[lenght];
            CopyTo(ptr, lenght, startIndex);
            return destino;
        }
        public static void Dispose(this IntPtr point)
        {
            System.Runtime.InteropServices.Marshal.FreeHGlobal(point);

        }
    }
}
