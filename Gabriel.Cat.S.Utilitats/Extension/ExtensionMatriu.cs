using Gabriel.Cat.S.Utilitats;
using System;
using System.Collections.Generic;
using System.Text;

namespace Gabriel.Cat.S.Extension
{
    public static class ExtensionMatriu
    {
        public static int GetLength<T>(this T[,] matriu,DimensionMatriz dimension)
        {
            return matriu.GetLength((int)dimension);
        }
        public static int GetLength<T>(this T[,,] matriu, DimensionMatriz dimension)
        {
            return matriu.GetLength((int)dimension);
        }
    }
}
