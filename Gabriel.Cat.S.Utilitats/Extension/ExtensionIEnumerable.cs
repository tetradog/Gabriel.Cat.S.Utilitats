using Gabriel.Cat.S.Utilitats;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gabriel.Cat.S.Extension
{
    public delegate bool MetodoWhileEach<Tvalue>(Tvalue valor);

    public static class ExtensionIEnumerable
    {
        public static IEnumerator<T> ObtieneEnumerador<T>(this IEnumerable<T> enumerator)
        {
            return enumerator.GetEnumerator();
        }
        public static void WhileEach<T>(this IEnumerable<T> enumeracion,MetodoWhileEach<T> metodo)
        {
            if (metodo == null)
                throw new ArgumentNullException("metodo");

            IEnumerator<T> enumerator = enumeracion.GetEnumerator();
            while (enumerator.MoveNext() && metodo(enumerator.Current)) ;
            
        }
        public static T[] SortByQuickSort<T>(this IEnumerable<T> list) where T : IComparable
        {
            return (T[])SortByQuickSort(list.ToArray());
        }
        public static T[] SortByBubble<T>(this IEnumerable<T> list) where T : IComparable
        {
            return (T[])SortByBubble(list.ToArray());

        }
        /// <summary>
        /// Ordena por elemetodo de orden indicado sin modificar la coleccion que se va a ordenar
        /// </summary>
        /// <param name="list"></param>
        /// <param name="orden"></param>
        /// <returns>devuelve una array ordenada</returns>
        public static T[] Sort<T>(this IEnumerable<T> list, SortMethod orden) where T : IComparable
        {
            return (T[])list.ToArray().Sort(orden);
        }
    }
}
