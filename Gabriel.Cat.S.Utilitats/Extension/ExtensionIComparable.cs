using System;
using System.Collections.Generic;
using System.Text;

namespace Gabriel.Cat.S.Extension
{
   public static class ExtensionIComparable
    {
        public static int CompareTo<T>(this T left, T right) where T : IComparable
        {
            const int IGUALES = 0;
            const int INFERIOR = -1;
            return left == null && right == null ? IGUALES : left != null ? left.CompareTo(right) : INFERIOR;
        }
    }
}
