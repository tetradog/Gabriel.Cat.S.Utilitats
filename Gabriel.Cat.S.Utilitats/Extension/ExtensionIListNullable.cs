using Gabriel.Cat.S.Utilitats;
using System;
using System.Collections.Generic;

namespace Gabriel.Cat.S.Extension
{
    public static class ExtensionIListNullable
    {
        static Type IListOfWhat<T>(this IList<T?> list) where T : struct
        {
            return typeof(T);
        }
        public static bool AreEquals<T>(this IList<T?> lstLeft, IList<T?> lstRight) where T : struct
        {
            bool equals = lstRight != null && lstLeft.Count == lstRight.Count;
            for (int i = 0; i < lstLeft.Count && equals; i++)
                equals = Equals(lstLeft[i], lstRight[i]);
            return equals;
        }
        public static IList<Nullable<T>> SortByQuickSort<T>(this IList<Nullable<T>> elements, bool ordenAscendente = true) where T : struct, IComparable
        {
            int left = 0, right = elements.Count - 1;
            return ISortByQuickSort(elements, left, right, ordenAscendente);
        }
        private static IList<Nullable<T>> ISortByQuickSort<T>(IList<Nullable<T>> elements, int left, int right, bool ordenAscendente) where T : struct, IComparable
        {
            //retocar para odenarlo al reves
            //algoritmo sacado se internet
            //todos los derechos son de http://snipd.net/quicksort-in-c

            const int IGUALES = 0;
            int i = left, j = right;
            Nullable<T> pivot;
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
        public static void Invertir<T>(this IList<T?> lst) where T : struct
        {
            for (int i = 0, f = lst.Count / 2, j = lst.Count - 1; i < f; i++, j--)
            {
                lst.Swap(i, j);
            }
        }
        public static void Swap<T>(this IList<Nullable<T>> lst, int posLeft, int posRight) where T : struct
        {
            Nullable<T> tmp = lst[posLeft];
            lst[posLeft] = lst[posRight];
            lst[posRight] = tmp;
        }
        public static IList<Nullable<T>> Sort<T>(this IList<Nullable<T>> lst, SortMethod orden = SortMethod.QuickSort, bool ordenAscendente = true) where T : struct, IComparable
        {
            IList<Nullable<T>> listSorted = null;
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
                for (int j = 0; j < (numLength - 1); j++)
                {
                    if (ExtensionIComparable.CompareTo(listaParaOrdenar[j + 1], listaParaOrdenar[j]) == orden)
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
