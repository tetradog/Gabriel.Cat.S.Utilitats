using System;
using System.Collections.Generic;
using System.Text;

namespace Gabriel.Cat.S.Extension
{
    public delegate bool MetodoWhileEach<Tvalue>(Tvalue valor);

    public static class ExtensionIEnumerable
    {
        public static IEnumerator<T> ObtieneEnumerador<T>(this IEnumerable<T> enumerator)
        {
            return enumerator.GetEnumerator();
        }
        public static void WhileEach<T>(this IEnumerable<T> enumeracion,MetodoWhileEach<T> metodo)
        {
            if (metodo == null)
                throw new ArgumentNullException("metodo");

            IEnumerator<T> enumerator = enumeracion.GetEnumerator();
            while (enumerator.MoveNext() && metodo(enumerator.Current)) ;
            
        }
    }
}
