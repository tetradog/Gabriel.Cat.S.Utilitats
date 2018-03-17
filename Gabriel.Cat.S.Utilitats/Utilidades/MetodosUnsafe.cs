namespace Gabriel.Cat.S.Extension
{
    public static class MetodosUnsafe
    {
        #region Pointers
        public unsafe static byte[] ReadBytes(byte* ptrBytes, int lenght)
        {
            byte[] array = new byte[lenght];
            byte* ptrArray;
            fixed (byte* ptArray = array)
            {
                ptrArray = ptArray;
                for (int i = 0; i < lenght; i++)
                {
                    *ptrArray = *ptrBytes;
                    ptrArray++;
                    ptrBytes++;
                }
            }
            return array;
        }
        public unsafe static void WriteBytes(byte[] bytesLeft, byte* ptrBytesRight)
        {
            fixed (byte* ptrBytesLeft = bytesLeft)
            {
                WriteBytes(ptrBytesLeft, ptrBytesRight, bytesLeft.Length);
            }
        }
        public unsafe static void WriteBytes(byte* ptrBytesLeft, byte[] bytesRight)
        {
            fixed (byte* ptrBytesRight = bytesRight)
            {
                WriteBytes(ptrBytesLeft, ptrBytesRight, bytesRight.Length);
            }
        }
        public unsafe static void WriteBytes(byte* ptrBytesLeft, byte* ptrBytesRight, int lenght)
        {

            for (int i = 0; i < lenght; i++)
            {
                *ptrBytesLeft = *ptrBytesRight;
                ptrBytesLeft++;
                ptrBytesRight++;
            }

        }
        #endregion
    }

}

