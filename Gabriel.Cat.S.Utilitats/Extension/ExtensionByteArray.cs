using System;
using System.Collections.Generic;
using System.Text;

namespace Gabriel.Cat.S.Extension
{
    public static class ExtensionByteArray
    {
        public static byte[] AddArray(this byte[] array, params byte[][] arraysToAdd)
        {

            byte[] arrayFinal;
            int lenght = array.Length;
            for (int i = 0; i < arraysToAdd.Length; i++)
                if (arraysToAdd[j] != null)
                    lenght += arraysToAdd[i].Length;
            arrayFinal = new byte[lenght];
            unsafe
            {
                byte* ptrBytes;
                byte* ptrBytesFinal;

                fixed (byte* ptBytesFinal = arrayFinal)
                {
                    ptrBytesFinal = ptBytesFinal;

                    fixed (byte* ptBytes = array)
                    {
                        ptrBytes = ptBytes;
                        for (int i = 0; i < array.Length; i++)
                        {
                            *ptrBytesFinal = *ptrBytes;
                            ptrBytesFinal++;
                            ptrBytes++;
                        }
                    }
                    for (int j = 0; j < arraysToAdd.Length; j++)
                        if (arraysToAdd[j] != null)
                            fixed (byte* ptBytes = arraysToAdd[j])
                            {
                                ptrBytes = ptBytes;
                                for (int i = 0; i < array.Length; i++)
                                {
                                    *ptrBytesFinal = *ptrBytes;
                                    ptrBytesFinal++;
                                    ptrBytes++;
                                }
                            }
                }

            }
            return arrayFinal;
        }
    }
}
