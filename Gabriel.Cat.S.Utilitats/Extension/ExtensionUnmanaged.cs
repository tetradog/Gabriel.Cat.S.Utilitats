using Gabriel.Cat.S.Utilitats;
using System;
using System.Collections.Generic;
using System.Text;

namespace Gabriel.Cat.S.Extension
{
    public delegate void MetodoTratarUnmanagedTypeArray<T>(T[] byteArray) where T : unmanaged;
    public unsafe delegate void MetodoTratarUnmanagedTypePointer<T>(T* prtByteArray) where T : unmanaged;
    public static class ExtensionUnmanaged
    {
        public static T[] AddArray<T>(this T[] array, params T[][] arraisToAdd) where T : unmanaged
        {
            return AddArray<T>(array, (IList<T[]>)arraisToAdd);
        }
        public static T[] AddArray<T>(this T[] array, IList<T[]> arraysToAdd) where T : unmanaged
        {

            T[] arrayFinal;
            int lenght = array.Length;
            for (int i = 0; i < arraysToAdd.Count; i++)
                if (!Equals(arraysToAdd[i], default))
                    lenght += arraysToAdd[i].Length;
            arrayFinal = new T[lenght];
            unsafe
            {
                T* ptrBytes;
                T* ptrBytesFinal;

                fixed (T* ptBytesFinal = arrayFinal)
                {
                    ptrBytesFinal = ptBytesFinal;

                    fixed (T* ptBytes = array)
                    {
                        ptrBytes = ptBytes;
                        for (int i = 0; i < array.Length; i++)
                        {
                            *ptrBytesFinal = *ptrBytes;
                            ptrBytesFinal++;
                            ptrBytes++;
                        }
                    }
                    for (int j = 0; j < arraysToAdd.Count; j++)
                        if (!Equals(arraysToAdd[j], default))
                            fixed (T* ptBytes = arraysToAdd[j])
                            {
                                ptrBytes = ptBytes;
                                for (int i = 0; i < arraysToAdd[j].Length; i++)
                                {
                                    *ptrBytesFinal = *ptrBytes;
                                    ptrBytesFinal++;
                                    ptrBytes++;
                                }
                            }
                }

            }
            return arrayFinal;
        }
        public static T[] SubArray<T>(this T[] array, int lenght) where T : unmanaged
        {
            return SubArray(array, 0, lenght);
        }
        public static T[] SubArray<T>(this T[] array, int inicio, int lenght) where T : unmanaged
        {
            if (inicio < 0 || lenght < 0)
                throw new IndexOutOfRangeException();

            T[] bytes = new T[lenght];
            int aux=inicio+ lenght;
            int total=aux>array.Length? lenght - (aux-array.Length): lenght;
            unsafe
            {
                T* ptrArray;
                T* ptrBytes;
                fixed (T* ptArray = array, ptBytes = bytes)
                {
                    ptrArray = ptArray + inicio;
                    ptrBytes = ptBytes;
                    for (int i = 0; i < total; i++,ptrArray++,ptrBytes++)
                    {
                        *ptrBytes = *ptrArray;
                    }
                }
            }
            return bytes;
        }

        public static int SearchArray<T>(this T[] datos, T[] arrayAEncontrar) where T : unmanaged
        {
            return SearchArray(datos, 0, arrayAEncontrar);
        }
        public static int SearchArray<T>(this T[] datos, int offsetInicio, T[] arrayAEncontrar) where T : unmanaged
        {
            return SearchArray(datos, offsetInicio, -1, arrayAEncontrar);
        }
        public static int SearchArray<T>(this T[] datos, int offsetInicio, int offsetFin, T[] arrayAEncontrar) where T : unmanaged
        {

            if (Equals(arrayAEncontrar, default))
                throw new ArgumentNullException("arrayAEncontrar");
            if (offsetInicio < 0 || offsetInicio + arrayAEncontrar.Length > datos.Length)
                throw new ArgumentOutOfRangeException();
            if (arrayAEncontrar.Length == 0)
                throw new ArgumentException("Empty array");

            const int DIRECCIONNOENCONTRADO = -1;
            int direccionBytes = DIRECCIONNOENCONTRADO;
            int posibleDireccion = DIRECCIONNOENCONTRADO;
            int numBytesEncontrados = 0;
            bool hastaElFinal = offsetFin < offsetInicio;
            //busco la primera aparicion de esos bytes a partir del offset dado como parametro
            unsafe
            {
                fixed (T* ptBytesDatos = datos, ptBytesAEcontrar = arrayAEncontrar)
                {
                    T* ptrBytesDatos = ptBytesDatos + offsetInicio;//posiciono al principio de la busqueda
                    T* ptrBytesAEcontrar = ptBytesAEcontrar;
                    for (int i = offsetInicio, finDatos = hastaElFinal ? datos.Length : offsetFin, totalBytesArrayAEncontrar = arrayAEncontrar.Length; direccionBytes == DIRECCIONNOENCONTRADO && i < finDatos && i + (totalBytesArrayAEncontrar - 1 - numBytesEncontrados) < finDatos/*si los bytes que quedan por ver se pueden llegar a ver continuo sino paro*/; i++)
                    {
                        if (Equals(*ptrBytesDatos, *ptrBytesAEcontrar))
                        {
                            numBytesEncontrados++;
                            //si no es el siguiente va al otro pero si es el primero se lo salta como si fuese malo...
                            if (posibleDireccion == DIRECCIONNOENCONTRADO)//si es la primera vez que entra
                            {
                                posibleDireccion = i;//le pongo el inicio

                            }
                            if (numBytesEncontrados == totalBytesArrayAEncontrar)//si es la última vez
                                direccionBytes = posibleDireccion;//le pongo el resultado para poder salir del bucle

                            ptrBytesAEcontrar++;



                        }
                        else if (numBytesEncontrados > 0)
                        {
                            //si no es reinicio la búsqueda
                            ptrBytesAEcontrar = ptBytesAEcontrar;
                            numBytesEncontrados = 0;
                            posibleDireccion = DIRECCIONNOENCONTRADO;

                        }

                        ptrBytesDatos++;

                    }
                }
            }

            return direccionBytes;
        }

        public static void UnsafeMethod<T>(this T[] array, MetodoUnsafeArray<T> mathod) where T : unmanaged
        {
            if (Equals(mathod,default))
                throw new ArgumentNullException("metodo");
            UnsafeArray<T>.Usar(array, mathod);
        }

        public static List<T[]> Split<T>(this T[] array, T byteSplit) where T : unmanaged
        {
            return Split(array, new T[] { byteSplit });
        }
        public static List<T[]> Split<T>(this T[] array, T[] bytesSplit) where T : unmanaged
        {
            if (Equals(bytesSplit,default))
             throw new ArgumentNullException();

            List<T[]> bytesSplited = new List<T[]>();
            int posicionArray;
            int posicionArrayEncontrada;
            if (bytesSplit.Length != 0)
            {

                posicionArray = (int)array.SearchArray(0, -1, bytesSplit);

                //opero
                if (posicionArray > -1)
                {
                    bytesSplited.Add(array.SubArray(0, posicionArray));
                    posicionArray += bytesSplit.Length;
                    do
                    {
                        posicionArrayEncontrada = (int)array.SearchArray(posicionArray, -1, bytesSplit);
                        if (posicionArrayEncontrada > -1)
                        {
                            bytesSplited.Add(array.SubArray(posicionArray, posicionArrayEncontrada));
                            posicionArray = posicionArrayEncontrada + bytesSplit.Length;

                        }
                    }
                    while (posicionArrayEncontrada > -1);
                    if (posicionArray < array.Length)
                        bytesSplited.Add(array.SubArray(posicionArray, array.Length));

                }
                else
                {
                    bytesSplited.Add(array);//no la ha encontrado pues la pongo toda
                }
            }
            else bytesSplited.Add(array);//no hay bytesPara hacer split pues pongo toda
            return bytesSplited;
        }
        public static bool ArrayEqual<T>(this T[] arrayLeft, T[] arrayRight, int inicioArrayLeft = 0, int inicioArrayRight = 0, int length = -1) where T : unmanaged
        {
            bool equals = !Equals(arrayRight,default);


            if (equals)
            {
                if (inicioArrayLeft < 0 || inicioArrayRight < 0 || length > -1 && inicioArrayLeft + length >= arrayLeft.Length && inicioArrayRight + length >= arrayRight.Length)
                    throw new ArgumentOutOfRangeException();

                unsafe
                {
                    T* ptrArrayLeft, ptrArrayRight;
                    fixed (T* ptArrayLeft = arrayLeft, ptArrayRight = arrayRight)
                    {

                        ptrArrayLeft = ptArrayLeft + inicioArrayLeft;
                        ptrArrayRight = ptArrayRight + inicioArrayRight;

                        for (int i = 0, f = arrayLeft.Length - inicioArrayLeft > arrayRight.Length - inicioArrayRight ? arrayRight.Length - inicioArrayRight : arrayLeft.Length - inicioArrayLeft; equals && (i < f && length == -1 || i < length); i++)
                        {
                            equals = Equals(*ptrArrayLeft, *ptrArrayRight);
                            if (!equals)
                            {
                                ptrArrayLeft++;
                                ptrArrayRight++;
                            }
                        }

                    }

                }
            }
            return equals;

        }

        public static int IndexOfT<T>(this T[] array, T byteAEcontrar) where T : unmanaged
        {
            return IndexOfT(array, 0, byteAEcontrar);
        }
        public static int IndexOfT<T>(this T[] array, int offsetInicio, T byteAEcontrar) where T : unmanaged
        {
            const int NOENCONTRADO = -1;
            int indexOf = NOENCONTRADO;
            int inicio = offsetInicio;//por si peta que no sea con el fixed
            unsafe
            {
                array.UnsafeMethod((unsArray) =>
                {

                    for (int i = inicio; i < unsArray.Length && indexOf == NOENCONTRADO; i++)
                        if (Equals(unsArray[i], byteAEcontrar))
                            indexOf = i;
                });
            }
            return indexOf;
        }

        public static int SearchBlock<T>(this T[] array, int offsetInicial, int lengthBlock, T byteBlock = default) where T : unmanaged
        {
            if (offsetInicial < 0 || lengthBlock < 0)
                throw new ArgumentOutOfRangeException();
            const int NOENCONTRADO = -1;
            int posicionFinal = NOENCONTRADO;
            int cantiadaBytesActual = 0;
            unsafe
            {
                T* ptrArray;
                array.UnsafeMethod((ptArray) =>
                {
                    ptrArray = ptArray.PtrArray + offsetInicial;
                    for (int i = offsetInicial; posicionFinal == NOENCONTRADO && i + (lengthBlock - cantiadaBytesActual) < array.Length; i++)
                    {
                        if (Equals(*ptrArray, byteBlock))
                        {
                            cantiadaBytesActual++;
                            if (cantiadaBytesActual > lengthBlock)
                                posicionFinal = i - lengthBlock;

                        }
                        else
                        {
                            cantiadaBytesActual = 0;
                        }
                        ptrArray++;
                    }

                });

            }
            return posicionFinal;
        }

        public static void Remove<T>(this T[] datos, int offsetInicio, int longitud, T byteEnBlanco = default) where T : unmanaged
        {
            if (offsetInicio < 0 || longitud < 0 || datos.Length < offsetInicio + longitud)
                throw new ArgumentException();
            unsafe
            {
                fixed (T* ptrbytesRom = datos)
                {
                    T* ptbytesRom = ptrbytesRom;
                    ptbytesRom += offsetInicio;
                    for (int i = 0, f = longitud; i < f; i++)
                    {
                        *ptbytesRom = byteEnBlanco;
                        ptbytesRom++;
                    }
                }
            }
        }

        public static void SetArray<T>(this T[] datos, int offsetIncioArrayDatos, T[] arrayAPoner) where T : unmanaged
        {
            if (arrayAPoner.Length + offsetIncioArrayDatos > datos.Length)
                throw new ArgumentOutOfRangeException();
            unsafe
            {
                T* ptrDatos, ptrArrayAPoner;
                datos.UnsafeMethod((ptDatos) =>
                {
                    arrayAPoner.UnsafeMethod((ptArrayAPoner) =>
                    {
                        ptrDatos = ptDatos.PtrArray + offsetIncioArrayDatos;
                        ptrArrayAPoner = ptArrayAPoner.PtrArray;
                        for (int i = 0; i < arrayAPoner.Length; i++)
                        {
                            *ptrDatos = *ptrArrayAPoner;
                            ptrDatos++;
                            ptrArrayAPoner++;
                        }


                    });



                });

            }

        }

        public static void Invertir<T>(this T[] array) where T : unmanaged
        {
            T aux;
            unsafe
            {
                T* ptrArray;
                fixed (T* ptArray = array)
                {
                    ptrArray = ptArray;
                    for (int i = 0, f = array.Length / 2, j = array.Length - 1; i < f; i++, j--)
                    {
                        aux = ptrArray[i];
                        ptrArray[i] = ptrArray[j];
                        ptrArray[j] = aux;
                    }
                }
            }
        }
        /// <summary>
        /// Devuelve un clon invertido de la array.
        /// </summary>
        /// <param name="byteArrayToReverse"></param>
        /// <returns></returns>
        public static T[] InvertirClone<T>(this T[] byteArrayToReverse) where T : unmanaged
        {
            T[] array = ((T[])byteArrayToReverse.Clone());
            array.Invertir();
            return array;
        }
    }
}
