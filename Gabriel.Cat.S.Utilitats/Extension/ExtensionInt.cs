﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Gabriel.Cat.S.Extension
{
   public static class ExtensionInt
    {
        public static bool EsPrimero(this int num)
        {
            bool esPrimero = true;
            for (int i = 2, f = Convert.ToInt32(Math.Sqrt(num)); i < f && esPrimero; i++)
                esPrimero = num % i != 0;
            return esPrimero;

        }
        public static int DamePrimeroCercano(this int num)
        {
            while (!num.EsPrimero())
                num++;
            return num;
        }
        public static int SumaHasta(this int[] array,int indexFin)
        {
            if (indexFin >= array.Length|| indexFin<0)
                throw new ArgumentOutOfRangeException();
            int total = 0;
            for (int i = 0; i <= indexFin; i++)
                total += array[i];
            return total;
        }
        public static int MultiplicaHasta(this int[] array, int indexFin)
        {
            if (indexFin >= array.Length || indexFin < 0)
                throw new ArgumentOutOfRangeException();
            int total = 1;
            for (int i = 0; i <= indexFin; i++)
                total *= array[i];
            return total;
        }
    }
}
