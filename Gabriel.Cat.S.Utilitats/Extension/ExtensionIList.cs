using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace Gabriel.Cat.S.Extension
{
   public static class ExtensionIList
    {
        public static Type ListOfWhat<T>(this IList<T> list)
        {
            return typeof(T);
        }
        public static Type ListOfWhat(this IList list)
        {
            return ListOfWhat((dynamic)list);
        }
    }
}
