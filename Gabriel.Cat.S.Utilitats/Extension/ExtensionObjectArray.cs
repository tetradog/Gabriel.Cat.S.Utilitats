using System;
using System.Collections.Generic;
using System.Text;

namespace Gabriel.Cat.S.Extension
{
    public static class ExtensionObjectArray
    {
        public static byte[] CastingToByte(this object[] source)
        {
            byte[] bytes = new byte[source.Length];
            for (int i = 0; i < source.Length; i++)
            {
                try
                {
                    bytes[i] = (Convert.ToByte(source[i]));
                }
                catch
                {
                }
            }
            return bytes;
        }
    }
}
