using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace Gabriel.Cat.S.Extension
{
   public static class ExtensionIDictionary
    {
        public static Type DicOfWhat(this IDictionary dic)
        {
            return IDicOfWhat((dynamic)dic);
        }
        static Type IDicOfWhat<TKey,TValue>(this IDictionary<TKey,TValue> dic)
        {
            return typeof(KeyValuePair<TKey, TValue>);
        }
        public static TValue[] GetValues<TKey, TValue>(this IDictionary<TKey, TValue> dic)
        {
            TValue[] values = new TValue[dic.Count];
            int pos = 0;
            foreach (KeyValuePair<TKey, TValue> elemento in dic)
            {
                values[pos++] = elemento.Value;
            }
            return values;
        }
        public static TKey[] GetKeys<TKey, TValue>(this IDictionary<TKey, TValue> dic)
        {
            TKey[] values = new TKey[dic.Count];
            int pos = 0;
            foreach (KeyValuePair<TKey, TValue> elemento in dic)
            {
                values[pos++] = elemento.Key;
            }
            return values;
        }
    }
}
