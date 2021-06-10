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
            return typeof(T).IsNullableType()?( left == null && right == null ? IGUALES : left != null ? left.CompareTo(right) : INFERIOR):left.CompareTo(right);
        }
        public static int CompareTo<T>(this Nullable<T> left,Nullable<T> right) where T :struct, IComparable
        {
            const int IGUALES = 0;
            const int INFERIOR = -1;
            return !left.HasValue&& !right.HasValue? IGUALES : left.HasValue ? ExtensionIComparable.CompareTo(left.Value,right.Value) : INFERIOR;
        }
        public static SortedList<T,T> ToSortedList<T>(this IList<T> lst) where T : IComparable
        {
            SortedList<T, T> sortedList = new SortedList<T, T>();
            for (int i = 0; i < lst.Count; i++)
                sortedList.Add(lst[i], lst[i]);
            return sortedList;
        }
    }
}
