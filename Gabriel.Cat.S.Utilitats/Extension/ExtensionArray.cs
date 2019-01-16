using System;
using System.Collections.Generic;
using System.Text;

namespace Gabriel.Cat.S.Extension
{
    public static class ExtensionArray
    {
        public static void SetValue(this Array array, int indexTotal, object objValue)
        {

            array.SetValue(array.GetDimensiones(), indexTotal, objValue);
        }
        public static void SetValue(this Array array, int[] dimensiones, int indexTotal, object objValue)
        {

            array.SetValue(objValue, array.GetIndex(dimensiones, indexTotal));
        }
        public static object GetValue(this Array array, int indexTotal)
        {

            return array.GetValue(array.GetDimensiones(), indexTotal);
        }
        public static object GetValue(this Array array, int[] dimensiones, int indexTotal)
        {

            return array.GetValue(array.GetIndex(dimensiones, indexTotal));
        }

        public static int[] GetDimensiones(this Array array)
        {
            int[] dimensiones = new int[array.Rank];
            for (int i = 0; i < array.Rank; i++)
                dimensiones[i] = array.GetLength(i);
            return dimensiones;
        }
        public static int[] GetIndex(this Array array, int indexTotal)
        {
            return GetIndex(array, array.GetDimensiones(), indexTotal);
        }

        public static int[] GetIndex(this Array array, int[] dimensiones, int indexTotal)
        {//por probar
            int[] index = new int[dimensiones.Length];
            int aux;
            for (int i = 0, j = index.Length - 1; i < dimensiones.Length; i++, j--)
            {
                aux = dimensiones.SumaAsta(j);
                if (indexTotal > aux)
                {
                    index[j] = indexTotal / aux;
                    indexTotal -= index[j] * aux;
                }

            }
            return index;
        }
    }
}
