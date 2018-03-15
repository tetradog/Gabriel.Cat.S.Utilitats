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
      
        public static void Dispose(this IntPtr point)
        {
            System.Runtime.InteropServices.Marshal.FreeHGlobal(point);

        }
    }
}
