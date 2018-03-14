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
        public static TCasting[] Casting<T,TCasting>(this IList<T> lst,bool elementosNoCompatiblesDefault=true)
        {
        
            TCasting[] castings = new TCasting[lst.Count];
            for (int i = 0; i < lst.Count; i++)
            {
                try
                {
                    castings[i] = (TCasting)(object)lst[i];
                }
                catch
                {
                    if (!elementosNoCompatiblesDefault)
                        throw;
                    else castings[i] = default(TCasting);
                }
            }
            return castings;
        }
    }
}
