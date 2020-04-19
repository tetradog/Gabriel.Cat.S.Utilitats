using Gabriel.Cat.S.Utilitats;
using System;
using System.Collections.Generic;

namespace Gabriel.Cat.S.Extension
{
    public static class ExtensionIListNullable
    {

        public static bool AreEquals<T>(this IList<T?> lstLeft, IList<T> lstRight) where T : struct
        {
            if (lstRight == default)
                throw new ArgumentNullException("lstRight");

            return lstRight.AreEquals(lstLeft);
        }

        public static IList<T?> Sort<T>(this IList<T?> lst, SortMethod orden = SortMethod.QuickSort, bool ordenAscendente = true) where T : struct, IComparable
        {
            IList<T?> listSorted = default;
            switch (orden)
            {
                case SortMethod.QuickSort:
                    listSorted = lst.SortByQuickSort(ordenAscendente);
                    break;
                case SortMethod.Bubble:
                    listSorted = lst.SortByBubble(ordenAscendente);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
            return listSorted;
        }
        public static IList<T?> SortByQuickSort<T>(this IList<T?> elements, bool ordenAscendente = true) where T : struct, IComparable
        {
            int left = 0, right = elements.Count - 1;
            return ISortByQuickSort(elements, left, right, ordenAscendente);
        }
        private static IList<T?> ISortByQuickSort<T>(IList<T?> elements, int left, int right, bool ordenAscendente) where T : struct, IComparable
        {
         
            //algoritmo sacado se internet
            //todos los derechos son de http://snipd.net/quicksort-in-c

            const int IGUALES = 0;
            int i = left, j = right;
            T? pivot;
            if (ordenAscendente)
            {

                if (right >= 0)
                {
                    pivot = elements[(left + right) / 2];

                    while (i <= j)
                    {

                        while (ExtensionIComparable.CompareTo(elements[i], pivot) < IGUALES)
                        {
                            i++;
                        }

                        while (ExtensionIComparable.CompareTo(elements[j], pivot) > IGUALES)
                        {
                            j--;
                        }
                        if (i <= j)
                        {
                            // Swap
                            elements.Swap(i, j);

                            i++;
                            j--;
                        }
                    }



                    // Recursive calls
                    if (left < j)
                    {
                        ISortByQuickSort(elements, left, j, ordenAscendente);
                    }

                    if (i < right)
                    {
                        ISortByQuickSort(elements, i, right, ordenAscendente);
                    }
                }
            }
            else
            {
                //se que no es optimo pero asi hay menos código :3
                SortByQuickSort(elements).Invertir();


            }

            return elements;
        }

      
        public static IList<T?> SortByBubble<T>(this IList<T?> listaParaOrdenar, bool ordenAscendente = true) where T : struct, IComparable
        {
            //codigo de internet adaptado :)
            //Todos los derechos//http://www.c-sharpcorner.com/UploadFile/3d39b4/bubble-sort-in-C-Sharp/
            int orden = ordenAscendente ? (int)Gabriel.Cat.S.Utilitats.CompareTo.Inferior : (int)Gabriel.Cat.S.Utilitats.CompareTo.Superior;
            bool flag = true;
            T? temp;
            int numLength = listaParaOrdenar.Count;

            //sorting an array
            for (int i = 1; (i <= (numLength - 1)) && flag; i++)
            {
                flag = false;
                for (int j = 0,k; j < (numLength - 1); j++)
                {
                    k = j + 1;
                    if (ExtensionIComparable.CompareTo(listaParaOrdenar[k], listaParaOrdenar[j]) == orden)
                    {
                        temp = listaParaOrdenar[j];
                        listaParaOrdenar[j] = listaParaOrdenar[k];
                        listaParaOrdenar[k] = temp;
                        flag = true;
                    }
                }
            }
            return listaParaOrdenar;

        }
    }
}
