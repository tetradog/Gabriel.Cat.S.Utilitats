using Gabriel.Cat.S.Utilitats;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace Gabriel.Cat.S.Extension
{
    public delegate bool ComprovaEventHandler<Tvalue>(Tvalue valorAComprovar);
    public static class ExtensionIList
    {
         static Type IListOfWhat<T>(this IList<T> list)
        {
            return typeof(T);
        }
        public static Type ListOfWhat(this IList list)
        {
            return IListOfWhat((dynamic)list);
        }
        public static TCasting[] Casting<TCasting>(this IList lst, bool elementosNoCompatiblesDefault = true)
        {

            TCasting[] castings = new TCasting[lst.Count];
            for (int i = 0; i < lst.Count; i++)
            {
                try
                {
                    castings[i] = (TCasting)lst[i];
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
        public static T GetElementActual<T>(this IList<T> llista, Ordre orden, int contador)
        {

            int posicio = 0;
            if (contador < 0)
            {
                contador *= -1;
                contador = llista.Count - (contador % llista.Count);
            }
            switch (orden)
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

        //poder hacer que se pueda poner los valores en el orden contrario, de izquierda a derecha o  al rebes o por culumnas en vez de por filas...(y=0,x=0,y=1,x=0...)
        public static Tvalue[,] ToMatriu<Tvalue>(this IList<Tvalue> llista, int length, DimensionMatriz dimensionTamañoMax = DimensionMatriz.Fila)
        {
            if (length < 1)
                throw new Exception("Como minimo 1 " + dimensionTamañoMax.ToString());

            int numeroOtraDimension = (llista.Count / (length * 1.0)) > (llista.Count / length) ? (llista.Count / length) + 1 : (llista.Count / length);
            int contador = 0;
            Tvalue[,] matriu;

            if (dimensionTamañoMax.Equals(DimensionMatriz.Fila))
                matriu = new Tvalue[numeroOtraDimension, length];
            else
                matriu = new Tvalue[length, numeroOtraDimension];

            for (int y = 0; y < matriu.GetLength(DimensionMatriz.Y) && contador < llista.Count; y++)
                for (int x = 0; x < matriu.GetLength(DimensionMatriz.X) && contador < llista.Count; x++)
                    matriu[x, y] = llista[contador++];

            return matriu;

        }

        //para los tipos genericos :) el tipo generico se define en el NombreMetodo<Tipo> y se usa en todo el metodoConParametros ;)
        public static List<Tvalue> Filtra<Tvalue>(this IList<Tvalue> valors, ComprovaEventHandler<Tvalue> comprovador)
        {
            if (comprovador == null)
                throw new ArgumentNullException("El metodo para realizar la comparacion no puede ser null");

            List<Tvalue> valorsOk = new List<Tvalue>();
            for (int i = 0; i < valors.Count; i++)
                if (comprovador(valors[i]))
                    valorsOk.Add(valors[i]);
            return valorsOk;

        }
        public static int BinarySearch<T>(this IList<T> list, T value) where T : IComparable
        {//source https://stackoverflow.com/questions/8067643/binary-search-of-a-sorted-array
            int pos = -1;
            int compareTo;

            bool found = false;
            int first = 0, last = list.Count - 1, mid = list.Count / 2;

            list.SortByQuickSort();
            //for a sorted array with descending values
            while (!found && first <= last)
            {
                mid = (first + last) / 2;
                compareTo = list[mid].CompareTo(value);
                if (0 < compareTo)
                {
                    first = mid + 1;
                }

                if (0 > compareTo)
                {
                    last = mid - 1;
                }

                else
                {
                    // You need to stop here once found or it's an infinite loop once it finds it.
                    found = true;
                    pos = mid;
                }
            }

            return pos;
        }
        /// <summary>
        /// Mira en la lista IEnumerable si contiene exactamente todos los elementos de la otra lista, no tiene en cuenta el orden
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        /// <param name="listToContain">lista a tener dentro de la otra</param>
        /// <returns></returns>
        public static bool Contains<T>(this IList<T> list, IList<T> listToContain) where T : IComparable
        {
            if (listToContain == null)
                throw new ArgumentNullException("La lista ha contener no puede ser null!!");
            bool contains = false;
            for (int i = 0; i < list.Count && !contains; i++)
            {
                contains = list.Contains(list[i])||Contains(list,list[i]);

            }
            return contains;
        }
        public static bool Contains<T>(this IList<T> list, T element) where T : IComparable
        {
            const int IGUALS= (int)Gabriel.Cat.S.Utilitats.CompareTo.Iguals; 
            bool contains = false;
            for (int i = 0; i < list.Count && !contains; i++)
            {
                contains = list[i].CompareTo(element) == IGUALS;
            }
            return contains;
        }
        public static List<T> SubList<T>(this IList<T> arrayB, int inicio)
        {
            return arrayB.SubList(inicio, arrayB.Count - inicio);
        }
        public static List<T> SubList<T>(this IList<T> arrayB, int inicio, int longitud)
        {

            List<T> subArray;

            if (inicio < 0 || longitud <= 0)
                throw new IndexOutOfRangeException();
            if (longitud + inicio > arrayB.Count)
                throw new IndexOutOfRangeException();
            subArray = new List<T>();

            for (int i = inicio, fin = inicio + longitud; i < fin; i++)
                subArray.Add(arrayB[i]);

            return subArray;

        }
        public static void SetIList<T>(this IList<T> listToSet, IList<T> source, int startIndexListToSet = 0, int startIndexSource = 0, int endIndexSource = -1)
        {
            if (source == null)
                throw new ArgumentNullException();
            if (startIndexSource < 0 || source.Count < startIndexSource || endIndexSource > 0 && (source.Count < endIndexSource || listToSet.Count < startIndexListToSet + (endIndexSource - startIndexSource)))
                throw new ArgumentOutOfRangeException();
            for (int i = startIndexListToSet, j = startIndexSource; i < source.Count && (endIndexSource == -1 || j < endIndexSource); i++, j++)
                listToSet[i] = source[j];
        }
    }
}
