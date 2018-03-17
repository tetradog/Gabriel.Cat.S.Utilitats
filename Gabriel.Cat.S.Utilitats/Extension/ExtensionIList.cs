using Gabriel.Cat.S.Utilitats;
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
        public static T GetElementActual<T>(this IList<T> llista, Ordre escogerKey, int contador)
        {

            int posicio = 0;
            if (contador < 0)
            {
                contador *= -1;
                contador = llista.Count - (contador % llista.Count);
            }
            switch (escogerKey)
            {
                case Ordre.Consecutiu:
                    posicio = contador % llista.Count;
                    break;
                case Ordre.ConsecutiuIAlInreves://repite el primero y el ultimo

                    posicio = contador / llista.Count;
                    if (posicio % 2 == 0)
                    {
                        //si esta bajando
                        posicio = contador % llista.Count;
                    }
                    else
                    {
                        //esta subiendo
                        posicio = llista.Count - (contador % llista.Count) - 1;
                    }

                    break;
            }
            return llista[posicio];
        }
   
        /// <summary>
        /// Ordena la array actual
        /// </summary>
        /// <param name="list"></param>
        /// <param name="orden"></param>
        /// <returns></returns>
        public static IList<T> Sort<T>(this IList<T> list, SortMethod orden) where T : IComparable
        {
            IList<T> listSorted = null;
            switch (orden)
            {
                case SortMethod.QuickSort:
                    listSorted = list.SortByQuickSort();
                    break;
                case SortMethod.Bubble:
                    listSorted = list.SortByBubble();
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
            return listSorted;
        }
       
        public static IList<T> SortByQuickSort<T>(this IList<T> elements) where T : IComparable
        {
            int left = 0, right = elements.Count - 1;
            return ISortByQuickSort(elements, left, right);
        }
        private static IList<T> ISortByQuickSort<T>(IList<T> elements, int left, int right) where T : IComparable
        {
            //algoritmo sacado se internet
            //todos los derechos son de http://snipd.net/quicksort-in-c
            int i = left, j = right;
            IComparable pivot;
            IComparable tmp;
            if (right >= 0)
            {
                pivot = elements[(left + right) / 2];

                while (i <= j)
                {
                    while (elements[i].CompareTo(pivot) < 0)
                    {
                        i++;
                    }

                    while (elements[j].CompareTo(pivot) > 0)
                    {
                        j--;
                    }

                    if (i <= j)
                    {
                        // Swap
                        tmp = elements[i];
                        elements[i] = elements[j];
                        elements[j] = (T)tmp;

                        i++;
                        j--;
                    }
                }

                // Recursive calls
                if (left < j)
                {
                    ISortByQuickSort(elements, left, j);
                }

                if (i < right)
                {
                    ISortByQuickSort(elements, i, right);
                }
            }

            return elements;
        }
  
        public static IList<T> SortByBubble<T>(this IList<T> listaParaOrdenar) where T : IComparable
        {
            //codigo de internet adaptado :)
            //Todos los derechos//http://www.c-sharpcorner.com/UploadFile/3d39b4/bubble-sort-in-C-Sharp/
            const int SUPERIOR = (int)Gabriel.Cat.S.Utilitats.CompareTo.Superior;
            bool flag = true;
            T temp;
            int numLength = listaParaOrdenar.Count;

            //sorting an array
            for (int i = 1; (i <= (numLength - 1)) && flag; i++)
            {
                flag = false;
                for (int j = 0; j < (numLength - 1); j++)
                {
                    if (listaParaOrdenar[j + 1].CompareTo(listaParaOrdenar[j]) == SUPERIOR)
                    {
                        temp = listaParaOrdenar[j];
                        listaParaOrdenar[j] = listaParaOrdenar[j + 1];
                        listaParaOrdenar[j + 1] = temp;
                        flag = true;
                    }
                }
            }
            return listaParaOrdenar;

        }
    }
}
