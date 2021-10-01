using Gabriel.Cat.S.Utilitats;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;

namespace Gabriel.Cat.S.Extension
{
    public delegate bool MetodoWhileEach<Tvalue>(Tvalue valor);

    public static class ExtensionIEnumerable
    {
        static Type IListOfWhat<T>(this IEnumerable<T> list)
        {
            return typeof(T);
        }

        public static Type ListOfWhat(this IEnumerable list)
        {
            return IListOfWhat((dynamic)list);
        }

        public static IEnumerator<T> ObtieneEnumerador<T>(this IEnumerable<T> enumerator)
        {
            return enumerator.GetEnumerator();
        }
        public static void WhileEach<T>(this IEnumerable<T> enumeracion,[NotNull] MetodoWhileEach<T> metodo)
        {
            IEnumerator<T> enumerator = enumeracion.GetEnumerator();
            while (enumerator.MoveNext() && metodo(enumerator.Current)) ;
            
        }
        public static T[] SortByQuickSort<T>(this IEnumerable<T> list) where T : IComparable
        {
            return list.Sort(SortMethod.QuickSort);
        }
        public static T[] SortByBubble<T>(this IEnumerable<T> list) where T : IComparable
        {
            return list.Sort(SortMethod.Bubble);

        }
        /// <summary>
        /// Ordena por elemetodo de orden indicado sin modificar la coleccion que se va a ordenar
        /// </summary>
        /// <param name="list"></param>
        /// <param name="orden"></param>
        /// <returns>devuelve una array ordenada</returns>
        public static T[] Sort<T>(this IEnumerable<T> list, SortMethod orden) where T : IComparable
        {
            T[] sortedArray = (T[])list.ToArray();
            sortedArray.Sort(orden);
            return sortedArray;
        }
        public static Tvalue[,] ToMatriu<Tvalue>(this IEnumerable<Tvalue> llista, int numeroDimension, DimensionMatriz dimensionTamañoMax = DimensionMatriz.Fila)
        { return llista.ToArray().ToMatriu(numeroDimension, dimensionTamañoMax); }

        public static IEnumerable<Tvalue> Join<Tvalue>(this IEnumerable<Tvalue> valors, Tvalue valorNou,bool joinValueAtTop=false)
        {
            if (joinValueAtTop)
                yield return valorNou;

            foreach (Tvalue value in valors)
                yield return value;

            if(!joinValueAtTop)
                yield return valorNou;
        }
        public static List<Tvalue> Join<Tvalue>(this IEnumerable<Tvalue> valors, IEnumerable<Tvalue> valorsNous, bool noPosarValorsJaExistents = false) where Tvalue : IComparable
        {
            List<Tvalue> llista = new List<Tvalue>(valors);
            bool valorEnLista = true;
            if (!Equals(valorsNous, default(IEnumerable<Tvalue>)))
            {

            
                    foreach (Tvalue valor in valorsNous)
                    {
                        if (noPosarValorsJaExistents)
                            valorEnLista =Extension.ExtensionIList.Contains(llista, valor);
                        
                         if (!noPosarValorsJaExistents)
                        {
                            llista.Add(valor);
                        }else if (!valorEnLista)
                            llista.Add(valor);
                    }

            }
            return llista;

        }
        public static IEnumerable<Tvalue> Join<Tvalue>([NotNull]this IEnumerable<Tvalue> valors, IEnumerable<Tvalue> valorsNous)
        {
            foreach (Tvalue value in valors)
                yield return value;
            if (!ReferenceEquals(valorsNous, default))
                foreach (Tvalue value in valorsNous)
                    yield return value;

        }


        
    }
}
