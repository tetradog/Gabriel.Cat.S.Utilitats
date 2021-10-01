using System;
using System.Collections.Generic;
using System.Text;

namespace Gabriel.Cat.S.Extension
{
   public static class ExtensionICollection
    {
        public static void RemoveRange<T>(this ICollection<T> list, IEnumerable<T> elementsToRemove)
        {
            if (!ReferenceEquals(elementsToRemove, default))
                foreach (T element in elementsToRemove)
                    list.Remove(element);
        }
        public static void RemoveRange<T>(this ICollection<T> list, IList<T> elementsToRemove)
        {
            if (!ReferenceEquals(elementsToRemove, default))
                for (int i=0;i<elementsToRemove.Count;i++)
                    list.Remove(elementsToRemove[i]);
        }
    }
}
