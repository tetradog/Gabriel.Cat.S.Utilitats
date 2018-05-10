using System;
using System.Collections.Generic;
using System.Text;

namespace Gabriel.Cat.S.Extension
{
   public static class ExtensionString
    {
        public static string[] Split(this string txt,string caracteresSplitSeguidos)
        {//falta testing
            IList<char[]> filasChar = txt.ToCharArray().Split(caracteresSplitSeguidos.ToCharArray());
            string[] filas = new string[filasChar.Count];
            for (int i = 0; i < filas.Length; i++)
                filas[i] = new string(filasChar[i]);
            return filas;
        }
    }
}
