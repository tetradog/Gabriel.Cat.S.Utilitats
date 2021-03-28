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
        {//falta hacer un test con todas las posibilidades

            int totalDim;
            int[] index = new int[dimensiones.Length];
            int aux=indexTotal;

            //tengo que sacar el indice en dimensiones
            for (int i = dimensiones.Length-1; i>=1&&indexTotal>0; i--)
            {
                totalDim= dimensiones.MultiplicaHasta(i-1);
                aux = indexTotal %totalDim;
                index[i] = indexTotal / totalDim;
                indexTotal = aux;

            }
            index[0] = aux;
            return index;
        }

        public static T[] GetFila<T>(this T[,] matriz, int fila)
        {
            T[] tFila = new T[matriz.GetDimensiones()[0]];
            for (int i = 0; i < tFila.Length; i++)
                tFila[i] = matriz[i, fila];
            return tFila;
        }
        public static void SetFila<T>(this T[,] matriz, int fila, T[] tFila)
        {
            for (int i = 0; i < tFila.Length; i++)
                matriz[i, fila] = tFila[i];
        }
    }
}
