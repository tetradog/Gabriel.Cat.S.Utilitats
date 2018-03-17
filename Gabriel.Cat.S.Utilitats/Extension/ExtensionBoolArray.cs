using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gabriel.Cat.S.Extension
{
   public static class ExtensionBoolArray
    {
        public static byte[] ToByteArray(this bool[] bits)
        {
            const int BITSBYTE = 8;

            if (bits.Length % BITSBYTE != 0)
                throw new ArgumentException();

            int numBytes = bits.Length / BITSBYTE;
            byte[] bytes = new byte[numBytes];
            int index = 0;

            unsafe
            {
                byte* ptrBytes;
                fixed (byte* ptBytes = bytes)
                {
                    ptrBytes = ptBytes;
                    for (int i = 0; i < numBytes; i++)
                    {
                        *ptrBytes = bits.SubArray(index, BITSBYTE).ToByte();
                        index += BITSBYTE;
                        ptrBytes++;
                    }
                }

            }

            return bytes;

        }
        public static bool[] SubArray(this bool[] array,  int length) {
            return SubArray(array, 0, length);
        }
        public static bool[] SubArray(this bool[] array, int startIndex, int length)
        {
            if (startIndex < 0 || startIndex + length > array.Length)
                throw new ArgumentOutOfRangeException();
            bool[] subArray = new bool[length];
            unsafe
            {
                bool* ptrArray, ptrSubArray;
                fixed (bool* ptArray = array)
                {
                    fixed (bool* ptSubArray = subArray)
                    {
                        ptrArray = ptArray;
                        ptrSubArray = ptSubArray;
                        ptrArray += startIndex;//asigno el inicio aqui :D
                        for (int j = 0, f = length; j < length; j++)
                        {
                            *ptrSubArray = *ptrArray;
                            ptrArray++;
                            ptrSubArray++;
                        }
                    }
                }
            }
            return subArray;
        }
        public static byte ToByte(this bool[] bits)
        {
            byte byteBuild = new byte();
            bits = bits.Reverse().ToArray();
            unsafe
            {
                bool* ptrBits;
                fixed (bool* ptBits = bits)
                {
                    ptrBits = ptBits;
                    for (int i = 0; i < bits.Length; i++)
                    {
                        if (*ptrBits)
                            byteBuild |= (byte)(1 << (7 - i));
                        ptrBits++;

                    }
                }
            }
            return byteBuild;
        }
    }
}
