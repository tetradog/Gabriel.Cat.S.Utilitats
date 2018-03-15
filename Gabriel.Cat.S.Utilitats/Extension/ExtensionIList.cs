using Gabriel.Cat.S.Utilitats;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace Gabriel.Cat.S.Extension
{
   public static class ExtensionIList
    {
        public static Type ListOfWhat<T>(this IList<T> list)
        {
            return typeof(T);
        }
        public static Type ListOfWhat(this IList list)
        {
            return ListOfWhat((dynamic)list);
        }
        public static TCasting[] Casting<T,TCasting>(this IList<T> lst,bool elementosNoCompatiblesDefault=true)
        {
        
            TCasting[] castings = new TCasting[lst.Count];
            for (int i = 0; i < lst.Count; i++)
            {
                try
                {
                    castings[i] = (TCasting)(object)lst[i];
                }
                catch
                {
                    if (!elementosNoCompatiblesDefault)
                        throw;
                    else castings[i] = default(TCasting);
                }
            }
            return castings;
        }
        public static T GetElementActual<T>(this IList<T> llista, Ordre escogerKey, int contador)
        {

            int posicio = 0;
            if (contador < 0)
            {
                contador *= -1;
                contador = llista.Count - (contador % llista.Count);
            }
            switch (escogerKey)
            {
                case Ordre.Consecutiu:
                    posicio = contador % llista.Count;
                    break;
                case Ordre.ConsecutiuIAlInreves://repite el primero y el ultimo

                    posicio = contador / llista.Count;
                    if (posicio % 2 == 0)
                    {
                        //si esta bajando
                        posicio = contador % llista.Count;
                    }
                    else
                    {
                        //esta subiendo
                        posicio = llista.Count - (contador % llista.Count) - 1;
                    }

                    break;
            }
            return llista[posicio];
        }
    }
}
