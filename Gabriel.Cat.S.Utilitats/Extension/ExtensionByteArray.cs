using System;
using System.Collections.Generic;
using System.Text;

namespace Gabriel.Cat.S.Extension
{
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
    }
}
