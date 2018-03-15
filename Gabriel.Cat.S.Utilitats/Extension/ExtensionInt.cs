using System;
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
    }
}
