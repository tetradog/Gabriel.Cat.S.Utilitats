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
            unsafe
            {
                byte* ptrBytes;
                fixed(byte* ptBytes=bytes)
                {
                    ptrBytes = ptBytes;
                    for (int i = 0; i < source.Length; i++)
                    {
                        try
                        {

                            *ptrBytes = (byte)source[i];
                        }
                        catch
                        {
                        }
                        finally {
                            ptrBytes++;
                        }
                    }
                }
              
            }
            return bytes;
        }
    }
}
