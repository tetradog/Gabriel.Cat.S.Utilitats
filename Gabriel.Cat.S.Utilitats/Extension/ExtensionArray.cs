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
            array.SetValue(objValue, GetIndex(dimensiones, indexTotal));
        }
        public static object GetValue(this Array array, int indexTotal)
        {
            return array.GetValue(array.GetDimensiones(), indexTotal);
        }
        public static object GetValue(this Array array, int[] dimensiones, int indexTotal)
        {
            return array.GetValue(GetIndex(dimensiones, indexTotal));
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
            return GetIndex( array.GetDimensiones(), indexTotal);
        }

        public static int[] GetIndex( int[] dimensiones, int indexTotal)
        {//por probar
            int[] index = new int[dimensiones.Length];
            int aux;
            //tengo que sacar el indice en dimensiones
            for (int i = 0; i < dimensiones.Length&&indexTotal>0; i++)
            {
                aux = indexTotal / dimensiones[i];
                index[i] = indexTotal % dimensiones[i];
                indexTotal = aux;

            }
            return index;
        }
    }
}
