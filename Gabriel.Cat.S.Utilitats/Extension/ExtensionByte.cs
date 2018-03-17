using System;
using System.Collections.Generic;
using System.Text;

namespace Gabriel.Cat.S.Extension
{
   public static class ExtensionByte
    {

        public static bool[] ToBits(this byte byteToBits)
        {
            const int BITSBYTE = 8;
            bool[] bits = new bool[BITSBYTE];
            unsafe
            {
                bool* ptrBits;
                fixed (bool* ptBits = bits)
                {
                    ptrBits = ptBits;
                    for (int i = BITSBYTE - 1; i >= 0; i--)
                    {
                        *ptrBits = (byteToBits & (1 << (i % BITSBYTE))) != 0;
                        ptrBits++;
                    }


                }
            }
            return bits;
        }
        public static byte GetHalfByte(this byte bToGet, bool getLeft = true)
        {
            byte bToReturn;
            if (getLeft)
                bToReturn = (byte)(0xF & bToGet >> 4);
            else bToReturn = (byte)(bToGet & 0xF);


            return bToReturn;

        }
        public static byte SetHalfByte(this byte bToSet, byte halfByte, bool setLeft = true)
        {
            byte byteToReturn;
            if (setLeft)
                byteToReturn = (byte)((halfByte << 4) + bToSet.GetHalfByte(false));
            else byteToReturn = (byte)((bToSet.GetHalfByte(true) << 4) + halfByte);

            return byteToReturn;
        }
    }
}
