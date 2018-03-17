using Gabriel.Cat.S.Utilitats;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Gabriel.Cat.S.Extension
{
    public delegate void MetodoTratarByteArray(byte[] byteArray);
    public unsafe delegate void MetodoTratarBytePointer(byte* prtByteArray);

    public static class ExtensionByteArray
    {
        public static byte[] AddArray(this byte[] array, params byte[][] arraysToAdd)
        {

            byte[] arrayFinal;
            int lenght = array.Length;
            for (int i = 0; i < arraysToAdd.Length; i++)
                if (arraysToAdd[i] != null)
                    lenght += arraysToAdd[i].Length;
            arrayFinal = new byte[lenght];
            unsafe
            {
                byte* ptrBytes;
                byte* ptrBytesFinal;

                fixed (byte* ptBytesFinal = arrayFinal)
                {
                    ptrBytesFinal = ptBytesFinal;

                    fixed (byte* ptBytes = array)
                    {
                        ptrBytes = ptBytes;
                        for (int i = 0; i < array.Length; i++)
                        {
                            *ptrBytesFinal = *ptrBytes;
                            ptrBytesFinal++;
                            ptrBytes++;
                        }
                    }
                    for (int j = 0; j < arraysToAdd.Length; j++)
                        if (arraysToAdd[j] != null)
                            fixed (byte* ptBytes = arraysToAdd[j])
                            {
                                ptrBytes = ptBytes;
                                for (int i = 0; i < array.Length; i++)
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
        public static byte[] SubArray(this byte[] array, int lenght)
        {
            return SubArray(array, 0, lenght);
        }
        public static byte[] SubArray(this byte[] array, int inicio, int lenght)
        {
            byte[] bytes = new byte[lenght];
            array.CopyTo(bytes, inicio);
            return bytes;
        }
        public static int SearchArray(this byte[] datos, byte[] arrayAEncontrar)
        {
            return SearchArray(datos, 0, arrayAEncontrar);
        }
        public static int SearchArray(this byte[] datos, int offsetInicio, byte[] arrayAEncontrar)
        {
            return SearchArray(datos, offsetInicio, -1, arrayAEncontrar);
        }
        public static int SearchArray(this byte[] datos, int offsetInicio, int offsetFin, byte[] arrayAEncontrar)
        {

            if (arrayAEncontrar == null)
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
                fixed (byte* ptBytesDatos = datos, ptBytesAEcontrar = arrayAEncontrar)
                {
                    byte* ptrBytesDatos = ptBytesDatos + offsetInicio;//posiciono al principio de la busqueda
                    byte* ptrBytesAEcontrar = ptBytesAEcontrar;
                    for (int i = offsetInicio, finDatos = hastaElFinal ? datos.Length : offsetFin, totalBytesArrayAEncontrar = arrayAEncontrar.Length; direccionBytes == DIRECCIONNOENCONTRADO && i < finDatos && i + (totalBytesArrayAEncontrar - 1 - numBytesEncontrados) < finDatos/*si los bytes que quedan por ver se pueden llegar a ver continuo sino paro*/; i++)
                    {
                        if (*ptrBytesDatos == *ptrBytesAEcontrar)
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

        public static string Hash(this byte[] array)
        {
            return  System.Security.Cryptography.MD5Core.GetHashString(array);
        }
        public static void CopyTo(this byte[] source, IntPtr ptrDestino, int startIndex = 0)
        {
            System.Runtime.InteropServices.Marshal.Copy(source, startIndex, ptrDestino, source.Length);
        }

        public static void UnsafeMethod(this byte[] array, MetodoUnsafeArray metodo)
        {
            UnsafeArray.Usar(array, metodo);
        }

        public static List<byte[]> Split(this byte[] array, byte byteSplit)
        {
            return Split(array, new byte[] { byteSplit });
        }
        public static List<byte[]> Split(this byte[] array, byte[] bytesSplit)
        {
            if (bytesSplit == null) throw new ArgumentNullException();
            List<byte[]> bytesSplited = new List<byte[]>();
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
        public static bool[] ToBits(this byte[] byteToBits)
        {
            const int BITSBYTE = 8;
            bool[] bits = new bool[byteToBits.Length * BITSBYTE];
            bool[] bitsAuxByte;
            //opero
            unsafe
            {
                bool* ptrBits, ptrBitsAuxByte;
                byte* ptrBytesToBits;
                fixed (bool* ptBits = bits)
                {
                    fixed (byte* ptBytesToBits = byteToBits)
                    {
                        ptrBytesToBits = ptBytesToBits;
                        ptrBits = ptBits;
                        for (int i = 0, f = byteToBits.Length; i < f; i++)
                        {
                            bitsAuxByte = (*ptrBytesToBits).ToBits();
                            ptrBytesToBits++;
                            fixed (bool* ptBitsAuxByte = bitsAuxByte)
                            {
                                ptrBitsAuxByte = ptBitsAuxByte;
                                for (int j = 0; j < BITSBYTE; j++)
                                {
                                    *ptrBits = *ptrBitsAuxByte;
                                    ptrBits++;
                                    ptrBitsAuxByte++;
                                }
                            }
                        }
                    }
                }
            }

            return bits;
        }
        public static CompareTo CompareTo(this byte[] arrayLeft, byte[] arrayRight)
        {
            const int IGUALES = (int)Gabriel.Cat.S.Utilitats.CompareTo.Iguals;
            int compareTo = arrayLeft.Length.CompareTo(arrayRight.Length);
            int pos;
            if (compareTo == IGUALES)
            {

                pos = 0;
                do
                {
                    compareTo = arrayLeft[pos].CompareTo(arrayRight[pos]);
                    pos++;
                } while (compareTo == IGUALES && pos < arrayLeft.Length);



            }
            return (Gabriel.Cat.S.Utilitats.CompareTo)compareTo;
        }
        public static bool ArrayEqual(this byte[] arrayLeft, byte[] arrayRight, int inicioArrayLeft = 0, int inicioArrayRight = 0, int length = -1)
        {
            if (inicioArrayLeft < 0 || inicioArrayRight < 0 || length > -1 && inicioArrayLeft + length >= arrayLeft.Length && inicioArrayRight + length >= arrayRight.Length)
                throw new ArgumentOutOfRangeException();
            bool equals = arrayRight != null;
            if (equals)
            {
                unsafe
                {
                    byte* ptrArrayLeft, ptrArrayRight;
                    fixed (byte* ptArrayLeft = arrayLeft, ptArrayRight = arrayRight)
                    {

                        ptrArrayLeft = ptArrayLeft + inicioArrayLeft;
                        ptrArrayRight = ptArrayRight + inicioArrayRight;

                        for (int i = 0, f = arrayLeft.Length - inicioArrayLeft > arrayRight.Length - inicioArrayRight ? arrayRight.Length - inicioArrayRight : arrayLeft.Length - inicioArrayLeft; equals && (i < f && length == -1 || i < length); i++)
                        {
                            equals = *ptrArrayLeft == *ptrArrayRight;
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

        public static int IndexByte(this byte[] array, byte byteAEcontrar)
        {
            return IndexByte(array, 0, byteAEcontrar);
        }
        public static int IndexByte(this byte[] array, int offsetInicio, byte byteAEcontrar)
        {
            const int NOENCONTRADO = -1;
            int indexOf = NOENCONTRADO;
            int inicio = offsetInicio;//por si peta que no sea con el fixed
            unsafe
            {
                array.UnsafeMethod((unsArray) =>
                {

                    for (int i = inicio; i < unsArray.Length && indexOf == NOENCONTRADO; i++)
                        if (unsArray[i] == byteAEcontrar)
                            indexOf = i;
                });
            }
            return indexOf;
        }
        public static int SearchBlock(this byte[] array, int offsetInicial, int lengthBlock, byte byteBlock = 0x0)
        {
            if (offsetInicial < 0 || lengthBlock < 0)
                throw new ArgumentOutOfRangeException();
            const int NOENCONTRADO = -1;
            int posicionFinal = NOENCONTRADO;
            int cantiadaBytesActual = 0;
            unsafe
            {
                byte* ptrArray;
                array.UnsafeMethod((ptArray) =>
                {
                    ptrArray = ptArray.PtrArray + offsetInicial;
                    for (int i = offsetInicial; posicionFinal == NOENCONTRADO && i + (lengthBlock - cantiadaBytesActual) < array.Length; i++)
                    {
                        if (*ptrArray == byteBlock)
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
        public static void Remove(this byte[] datos, int offsetInicio, int longitud, byte byteEnBlanco = 0x0)
        {
            if (offsetInicio < 0 || longitud < 0 || datos.Length < offsetInicio + longitud)
                throw new ArgumentException();
            unsafe
            {
                fixed (byte* ptrbytesRom = datos)
                {
                    byte* ptbytesRom = ptrbytesRom;
                    ptbytesRom += offsetInicio;
                    for (int i = 0, f = longitud; i < f; i++)
                    {
                        *ptbytesRom = byteEnBlanco;
                        ptbytesRom++;
                    }
                }
            }
        }
        public static void SetArray(this byte[] datos, int offsetIncioArrayDatos, byte[] arrayAPoner)
        {
            if (arrayAPoner.Length + offsetIncioArrayDatos > datos.Length)
                throw new ArgumentOutOfRangeException();
            unsafe
            {
                byte* ptrDatos, ptrArrayAPoner;
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
        public static void Invertir(this byte[] array)
        {
            //por testear!!
            byte aux;
            unsafe
            {
                fixed (byte* ptrArray = array)
                    for (int i = 0, f = array.Length / 2, j = array.Length - 1; i < f; i++, j--)
                    {
                        aux = ptrArray[i];
                        ptrArray[i] = ptrArray[j];
                        ptrArray[j] = aux;
                    }
            }
        }
        public static byte[] ReverseArray(this byte[] byteArrayToReverse)
        {
            byte[] byteArrayReversed = new byte[byteArrayToReverse.Length];
            unsafe
            {

                byte* ptrInverseBytesOut, ptrInverseBytesIn;
                byteArrayReversed.UnsafeMethod((ptrBytesOut) =>
                {
                    byteArrayToReverse.UnsafeMethod((ptrBytesIn) =>
                    {
                        ptrInverseBytesIn = ptrBytesIn.PtrArrayFin;
                        ptrInverseBytesOut = ptrBytesOut.PtrArrayFin;

                        for (int i = 0, f = (int)ptrBytesIn.Length / 2; i < f; i++)
                        {
                            *ptrBytesOut.PtrArray = *ptrInverseBytesIn;
                            *ptrInverseBytesOut = *ptrBytesIn.PtrArray;
                            ptrBytesIn.PtrArray++;
                            ptrBytesOut.PtrArray++;
                            ptrInverseBytesIn--;
                            ptrInverseBytesOut--;
                        }

                    });
                });
            }
            return byteArrayReversed;
        }
    
          
        }

}

