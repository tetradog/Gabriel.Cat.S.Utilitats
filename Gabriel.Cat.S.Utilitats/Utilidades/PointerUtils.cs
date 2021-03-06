using System.Diagnostics.CodeAnalysis;

namespace Gabriel.Cat.S.Utilitats
{
    public static class PointerUtils
    {

        public static unsafe byte[] ReadLine([NotNull] byte*[] ptrs, bool ptrNext = true)
        {
            byte[] bytesLine = new byte[ptrs.Length];
            byte* ptrBytes;
            fixed (byte* ptBytes = bytesLine)
            {
                ptrBytes = ptBytes;
                for (int i = 0; i < ptrs.Length; i++)
                {
                    *ptrBytes = *ptrs[i];
                    ptrBytes++;
                    if (ptrNext)
                        ptrs[i]++;


                }

            }
            return bytesLine;
        }
        public static unsafe void WriteLine([NotNull] byte*[] ptrs, [NotNull] byte[] data, bool ptrNext = true)
        {
            fixed (byte* ptData = data)
                WriteLine(ptrs, ptData, ptrNext);
        }
        public static unsafe void WriteLine([NotNull] byte*[] ptrs, byte* ptrData, bool ptrNext = true)
        {
            for (int i = 0; i < ptrs.Length; i++)
            {
                *ptrs[i] = *ptrData;
                ptrData++;
                if (ptrNext)
                    ptrs[i]++;
            }
        }

        public static unsafe byte*[] ToArray(byte* ptrData, int lengthPart, int parts)
        {
            byte*[] partes = new byte*[parts];
            for (int i = 0; i < parts; i++)
            {
                partes[i] = ptrData;
                ptrData += lengthPart;
            }
            return partes;
        }

        public static unsafe void Seek([NotNull] byte*[] ptrs, int toAdd = 1)
        {
            for (int i = 0; i < ptrs.Length; i++)
                ptrs[i] += toAdd;
        }

    }








}
