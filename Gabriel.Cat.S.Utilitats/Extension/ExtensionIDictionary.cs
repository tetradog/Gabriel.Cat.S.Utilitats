using Gabriel.Cat.S.Utilitats;
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
        public static void SetValues<TKey,TValue>(this IDictionary<TKey, TValue> dic, IDictionary<TKey,TValue> values)
        {
            TKey[] ids= dic.GetKeys();
            for (int i = 0; i < ids.Length; i++)
                if (values.ContainsKey(ids[i]))
                    dic[ids[i]] = values[ids[i]];
        }
        public static IDictionary<TKey, TValue> Clon<TKey, TValue>(this IDictionary<TKey, TValue> dic)
        {
            IDictionary<TKey, TValue> clon =(IDictionary<TKey, TValue>) dic.GetType().GetObj(); 
            foreach (var item in dic)
                clon.Add(item.Key, item.Value);
            return clon;
        }

    }
}
