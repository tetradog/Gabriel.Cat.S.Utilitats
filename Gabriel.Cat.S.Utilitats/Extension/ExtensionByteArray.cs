using Gabriel.Cat.S.Utilitats;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Gabriel.Cat.S.Extension
{


    public static class ExtensionByteArray
    {


        public static string Hash(this byte[] array)
        {
            return System.Security.Cryptography.MD5Core.GetHashString(array);
        }

        public static void CopyTo(this byte[] source, IntPtr ptrDestino, int startIndex = 0)
        {
            System.Runtime.InteropServices.Marshal.Copy(source, startIndex, ptrDestino, source.Length);
        }

     

        public static bool[] ToBits(this byte[] byteToBits)
        {
            const int BITSBYTE = 8;

            bool[] bitsAuxByte;
            bool[] bits = new bool[byteToBits.Length * BITSBYTE];
            
            //opero
            unsafe
            {
                bool* ptrBits, ptrBitsAuxByte;
                byte* ptrBytesToBits;
                fixed (bool* ptBits = bits)
                {
                    fixed (byte* ptBytesToBits = byteToBits)
                    {
                        ptrBytesToBits = ptBytesToBits;
                        ptrBits = ptBits;
                        for (int i = 0, f = byteToBits.Length; i < f; i++)
                        {
                            bitsAuxByte = (*ptrBytesToBits).ToBits();
                            ptrBytesToBits++;
                            fixed (bool* ptBitsAuxByte = bitsAuxByte)
                            {
                                ptrBitsAuxByte = ptBitsAuxByte;
                                for (int j = 0; j < BITSBYTE; j++)
                                {
                                    *ptrBits = *ptrBitsAuxByte;
                                    ptrBits++;
                                    ptrBitsAuxByte++;
                                }
                            }
                        }
                    }
                }
            }

            return bits;
        }

        public static CompareTo CompareTo(this byte[] arrayLeft, byte[] arrayRight)
        {
            const int IGUALES = (int)Gabriel.Cat.S.Utilitats.CompareTo.Iguals;
            const int INFERIOR= (int)Gabriel.Cat.S.Utilitats.CompareTo.Inferior;

            int pos;
            int compareTo =arrayRight!=null? arrayLeft.Length.CompareTo(arrayRight.Length):INFERIOR;
            
            if (compareTo == IGUALES)
            {

                pos = 0;
                do
                {
                    compareTo = arrayLeft[pos].CompareTo(arrayRight[pos]);
                    pos++;
                } while (compareTo == IGUALES && pos < arrayLeft.Length);



            }
            return (Gabriel.Cat.S.Utilitats.CompareTo)compareTo;
        }

   


    }

}

