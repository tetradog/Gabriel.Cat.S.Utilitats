using System;
using System.Collections.Generic;
using System.Text;

namespace Gabriel.Cat.S.Extension
{
   public static class ExtensionCharArray
    {
        #region COPY&PASTE
        /*
         IMPORTANTE ESTA PARTE ES COPY&PASTE de la parte ExtensionBYTEARRAY si hay bug mirar allí y luego corregir aqui
         me gustaria no tener que repetir código pero es obligatorio por el unsafe no me deja hacerlo generico...
         */
        public static int SearchArray(this char[] datos, char[] arrayAEncontrar)
        {
            return SearchArray(datos, 0, arrayAEncontrar);
        }
        public static int SearchArray(this char[] datos, int offsetInicio, char[] arrayAEncontrar)
        {
            return SearchArray(datos, offsetInicio, -1, arrayAEncontrar);
        }
        public static int SearchArray(this char[] datos, int offsetInicio, int offsetFin, char[] arrayAEncontrar)
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
                fixed (char* ptBytesDatos = datos, ptBytesAEcontrar = arrayAEncontrar)
                {
                    char* ptrBytesDatos = ptBytesDatos + offsetInicio;//posiciono al principio de la busqueda
                    char* ptrBytesAEcontrar = ptBytesAEcontrar;
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
        public static char[] SubArray(this char[] array, int lenght)
        {
            return SubArray(array, 0, lenght);
        }
        public static char[] SubArray(this char[] array, int inicio, int lenght)
        {
            if (inicio < 0 || lenght < 0)
                throw new IndexOutOfRangeException();

            char[] caracteres = new char[lenght];
            unsafe
            {
                char* ptrArray;
                char* ptrCaracteres;
                fixed (char* ptArray = array)
                fixed (char* ptCaracteres = caracteres)
                {
                    ptrArray = ptArray + inicio;
                    ptrCaracteres = ptCaracteres;
                    for (int i = 0; i < lenght; i++)
                    {
                        *ptrCaracteres = *ptrArray;
                        ptrArray++;
                        ptrCaracteres++;
                    }
                }
            }
            return caracteres;
        }
        #endregion
        public static List<char[]> Split(this char[] caracteres,string caracteresSplit)
        {
            return Split(caracteres, caracteresSplit.ToCharArray());
        }
        public static List<char[]> Split(this char[] caracteres,params char[] caracteresSplit)
        {//falta testing
            const int FIN = -1;
            List<char[]> lstPartes = new List<char[]>();
            int posicionCaracteresSplit=0;
            int posicionAnt;
            do
            {
                posicionAnt = posicionCaracteresSplit;
                posicionCaracteresSplit = -1;
                for(int i=0;i<caracteresSplit.Length&&posicionCaracteresSplit==-1;i++)
                   posicionCaracteresSplit = caracteres.SearchArray(posicionAnt, new char[] { caracteresSplit[i] });
                if(posicionCaracteresSplit!=FIN&& posicionCaracteresSplit + caracteresSplit.Length < caracteres.Length)
                {
                    lstPartes.Add(caracteres.SubArray(posicionAnt,posicionCaracteresSplit-posicionAnt));
                    posicionCaracteresSplit++;
                }
               
            } while (posicionCaracteresSplit != FIN);
            if (posicionAnt + caracteresSplit.Length < caracteres.Length)
                lstPartes.Add(caracteres.SubArray(posicionAnt,caracteres.Length- posicionAnt));
            return lstPartes;
        }
    }
}
