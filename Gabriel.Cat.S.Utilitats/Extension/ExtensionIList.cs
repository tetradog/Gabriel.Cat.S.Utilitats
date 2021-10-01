using Gabriel.Cat.S.Utilitats;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace Gabriel.Cat.S.Extension
{
    public delegate bool ComprovaEventHandler<Tvalue>(Tvalue valorAComprovar);
    public delegate TOut MetodoConvertir<TIn, TOut>(TIn input);
    public static class ExtensionIList
    {
        #region PuestoAPrueba 


        public static IEnumerable<TCasting> Casting<TCasting>(this IList lst, bool elementosNoCompatiblesDefault = true)
        {
            TCasting value;
            for (int i = 0; i < lst.Count; i++)
            {
                try
                {
                    value=(TCasting)lst[i];
                }
                catch
                {
                    if (!elementosNoCompatiblesDefault)
                        throw;
                    else value=default;
                }
                yield return value;
            }

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
        /// <param name="orden">método para ordenar</param>
        /// <param name="ordenAscendente"></param>
        public static void Sort<T>(this IList<T> list, SortMethod orden = SortMethod.QuickSort, bool ordenAscendente = true) where T : IComparable
        {
         
            switch (orden)
            {
                case SortMethod.QuickSort:
                  list.SortByQuickSort(ordenAscendente);
                    break;
                case SortMethod.Bubble:
                    list.SortByBubble(ordenAscendente);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

        }
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="elements"></param>
        /// <param name="ordenAscendente"></param>
        /// <returns>devuelve la misma lista no es una copia!</returns>
        public static void SortByQuickSort<T>(this IList<T> elements, bool ordenAscendente = true) where T : IComparable
        {
            int left = 0, right = elements.Count - 1;
            ISortByQuickSort(elements, left, right, ordenAscendente);
        }
        private static void ISortByQuickSort<T>(IList<T> elements, int left, int right, bool ordenAscendente) where T : IComparable
        {
            //retocar para odenarlo al reves
            //algoritmo sacado se internet
            //todos los derechos son de http://snipd.net/quicksort-in-c

            const int IGUALES = 0;
            int i = left, j = right;
            IComparable pivot;
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
                SortByQuickSort(elements);
                elements.Invertir();


            }

      
        }
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="listaParaOrdenar"></param>
        /// <param name="ordenAscendente"></param>
        /// <returns>devuelve la misma lista no es una copia!</returns>
        public static void SortByBubble<T>(this IList<T> listaParaOrdenar, bool ordenAscendente = true) where T : IComparable
        {
            //codigo de internet adaptado :)
            //Todos los derechos//http://www.c-sharpcorner.com/UploadFile/3d39b4/bubble-sort-in-C-Sharp/
            int orden = ordenAscendente ? (int)Gabriel.Cat.S.Utilitats.CompareTo.Inferior : (int)Gabriel.Cat.S.Utilitats.CompareTo.Superior;
            bool flag = true;
            T temp;
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
         

        }

    
        public static void Swap<T>(this IList<T> lst, int posElementAToB, int posElementBToA)
        {
            T tmp = lst[posElementAToB];
            lst[posElementAToB] = lst[posElementBToA];
            lst[posElementBToA] = tmp;
        }

        #endregion

        public static T LastOrDefault<T>(this IList<T> lst)
        {
            return lst.Count > 0 ? lst[lst.Count - 1] : default(T);
        }
        public static object[] ToArray(this IList lst)
        {
            object[] objs = new object[lst.Count];
            for (int i = 0; i < objs.Length; i++)
                objs[i] = lst[i];
            return objs;
        }


        public static void Invertir<T>(this IList<T> lst)
        {
            for (int i = 0, f = lst.Count / 2, j = lst.Count - 1; i < f; i++, j--)
            {
                lst.Swap(i, j);
            }
        }





        //poder hacer que se pueda poner los valores en el orden contrario, de izquierda a derecha o  al rebes o por culumnas en vez de por filas...(y=0,x=0,y=1,x=0...)
        public static Tvalue[,] ToMatriu<Tvalue>(this IList<Tvalue> llista, int length, DimensionMatriz colocarTamañoGrandeEn = DimensionMatriz.Fila)
        {
            if (length < 1)
                throw new ArgumentOutOfRangeException("Como minimo 1 " + colocarTamañoGrandeEn.ToString());

            Tvalue[,] matriu;
            int numeroOtraDimension = (llista.Count / (length * 1.0)) > (llista.Count / length) ? (llista.Count / length) + 1 : (llista.Count / length);
            int contador = 0;
  

            if (colocarTamañoGrandeEn.Equals(DimensionMatriz.Fila))
                matriu = new Tvalue[numeroOtraDimension, length];
            else
                matriu = new Tvalue[length, numeroOtraDimension];

            for (int y = 0; y < matriu.GetLength(DimensionMatriz.Y) && contador < llista.Count; y++)
                for (int x = 0; x < matriu.GetLength(DimensionMatriz.X) && contador < llista.Count; x++)
                    matriu[x, y] = llista[contador++];

            return matriu;

        }

        public static int BinarySearch<T>(this IList<T> list, T value) where T : IComparable
        {//source https://stackoverflow.com/questions/8067643/binary-search-of-a-sorted-array
            const int IGUALES = 0;
            const int MINIMO = 2;
            int pos = -1;
            int compareTo;

            bool found = false;
            int first = 0, last = list.Count - 1, mid = list.Count / 2;

            list.SortByQuickSort(false);
            //for a sorted array with descending values
            if (list.Count > MINIMO)
            {
                while (!found && first <= last)
                {
                    mid = (first + last) / 2;
                    compareTo = ExtensionIComparable.CompareTo(list[mid], value);
                    if (IGUALES < compareTo)
                    {
                        first = mid + 1;
                    }

                    if (IGUALES > compareTo)
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
            }
            else
            {
                for (int i = 0; i < list.Count && !found; i++)
                {
                    found = list[i].CompareTo(value) == IGUALES;
                    if (found)
                    {
                        pos = i;
                    }
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
        public static bool Contains<T>(this IList<T> list,[NotNull] IList<T> listToContain) where T : IComparable
        {

            bool contains = false;
            for (int i = 0; i < listToContain.Count && !contains; i++)
            {
                contains = Contains(list, listToContain[i]);

            }
            return contains;
        }
        public static bool Contains<T>(this IList<T> list, T element) where T : IComparable
        {
            const int IGUALS = (int)Gabriel.Cat.S.Utilitats.CompareTo.Iguals;
            bool contains = false;
            for (int i = 0; i < list.Count && !contains; i++)
            {
                contains = ExtensionIComparable.CompareTo(list[i], element) == IGUALS;
            }
            return contains;
        }
        public static IEnumerable<T> SubList<T>(this IList<T> arrayB, int inicio)
        {
            return arrayB.SubList(inicio, arrayB.Count - inicio);
        }
        public static IEnumerable<T> SubList<T>(this IList<T> arrayB, int inicio, int longitud)
        {

            if (inicio < 0 || longitud <= 0)
                throw new IndexOutOfRangeException();
            if (longitud + inicio > arrayB.Count)
                throw new IndexOutOfRangeException();


            for (int i = inicio, fin = inicio + longitud,j=0; i < fin; i++,j++)
                yield return arrayB[i];


        }
        public static void SetIList<T>(this IList<T> listToSet,[NotNull] IList<T> source, int startIndexListToSet = 0, int startIndexSource = 0, int endIndexSource = -1)
        {
            if (startIndexSource < 0 || source.Count < startIndexSource || endIndexSource > 0 && (source.Count < endIndexSource || listToSet.Count < startIndexListToSet + (endIndexSource - startIndexSource)))
                throw new ArgumentOutOfRangeException();
            for (int i = startIndexListToSet, j = startIndexSource; i < source.Count && (endIndexSource == -1 || j < endIndexSource); i++, j++)
                listToSet[i] = source[j];
        }
        public static bool AreEqual(this IList lstLeft, [AllowNull] IList lstRight)
        {
            bool equals = !Equals(lstRight,default(IList)) && lstLeft.Count == lstRight.Count;
            for (int i = 0; i < lstLeft.Count && equals; i++)
                equals = Equals(lstLeft[i], lstRight[i]);
            return equals;
        }
        public static bool AreEquals<T>(this IList<T> lstLeft, [AllowNull] IList<T> lstRight)
        {

            bool equals = !Equals(lstRight, default(IList)) && lstLeft.Count == lstRight.Count;
            for (int i = 0; i < lstLeft.Count && equals; i++)
                equals = Equals(lstLeft[i], lstRight[i]);
            return equals;
        }
        public static bool AreEquals<T>(this IList<T> lstLeft,[AllowNull] IList<T?> lstRight) where T : struct
        {

            bool equals = !Equals(lstRight, default(IList)) && lstLeft.Count == lstRight.Count;
            object right;
            for (int i = 0; i < lstLeft.Count && equals; i++)
            {
                if (lstRight[i].HasValue)
                    right = lstRight[i].Value;
                else right = null;

                equals = Equals(lstLeft[i], right);
            }
            return equals;
        }
        public static void AddRange<T>(this IList<T> lst, IList<T> toAdd)
        {
           for(int i=0;i<toAdd.Count;i++)
                lst.Add(toAdd[i]);
        }
        public static void AddRange<T>(this IList<T> lst,IEnumerable<T> toAdd)
        {
            foreach (T item in toAdd)
                lst.Add(item);
        }
        public static void Desordena<T>(this IList<T> lst)
        {
            T[] lstDesordenada = lst.GetRandom().ToArray();
            lst.Clear();
            lst.AddRange(lstDesordenada);

        }
        public static IEnumerable<T> GetRandom<T>(this IList<T> lst)
        {
            foreach (int randomPos in lst.Count.GetRandomPositionList())
                yield return lst[randomPos];
        }


        public static IEnumerable<TOut> Convert<TIn,TOut>(this IList<TIn> ins,[NotNull]MetodoConvertir<TIn,TOut> method)
        {

            for (int i = 0; i < ins.Count; i++)
                yield return method(ins[i]);
         
        }
    }
}
