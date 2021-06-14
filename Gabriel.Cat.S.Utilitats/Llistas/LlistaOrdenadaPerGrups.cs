using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;

namespace Gabriel.Cat.S.Utilitats
{
    public class LlistaOrdenadaPerGrups<TKey, TValue> : ObjectAutoId, IEnumerable<KeyValuePair<TKey, TValue[]>>
         where TKey : IComparable
         where TValue : IComparable
    {
        LlistaOrdenada<TKey, LlistaOrdenada<IComparable, TValue>> diccionari;
        public LlistaOrdenadaPerGrups()
        {
            diccionari = new LlistaOrdenada<TKey, LlistaOrdenada<IComparable, TValue>>();
        }
        public TValue[] this[[NotNull] TKey key]
        {
            get
            {
                return (TValue[])diccionari[key].Values;
            }
        }
        public int Count()
        {
            int total = 0;

            for(int i=0;i<diccionari.Count;i++)
                total += diccionari.GetValueAt(i).Count;

            return total;
        }
        public int Count([NotNull] TKey key)
        {
            return diccionari[key].Count;
        }
        public void Add([NotNull] TKey key, [NotNull] IList<TValue> values)
        {

            for (int i = 0; i < values.Count; i++)
            {
                try
                {
                    Add(key, values[i]);
                }
                catch { }//si ya esta añadido no pasa nada :D
            }
        }
        public void Add([NotNull] TKey key, [NotNull] TValue value)
        {
            if (!diccionari.ContainsKey(key))
                diccionari.Add(key, new LlistaOrdenada<IComparable, TValue>());
            diccionari[key].Add(value, value);
        }
        public void Remove([NotNull] TKey key, [NotNull] IList<TValue> values)
        {
            for (int i = 0; i < values.Count; i++)
                if (values[i] != null)
                    Remove(key, values[i]);
        }
        public void Remove([NotNull] TKey key, [NotNull] TValue value)
        {
            if (diccionari.ContainsKey(key))
            {
                if (diccionari[key].ContainsKey(value))
                {
                    diccionari[key].Remove(value);
                    if (diccionari[key].Count == 0)
                        diccionari.Remove(key);
                }
            }
        }
        public void Remove([NotNull] IList<TKey> keys)
        {
            for (int i = 0; i < keys.Count; i++)
                if (keys[i] != null)
                    Remove(keys[i]);
        }
        public void Remove([NotNull] TKey key)
        {
            diccionari.Remove(key);
        }
        public void Clear()
        {
            diccionari.Clear();
        }
        public void ClearValues([NotNull] TKey key)
        {
            if (ContainsKey(key))
                diccionari[key].Clear();
        }
        public bool ContainsKey([NotNull] TKey key)
        {
            return diccionari.ContainsKey(key);
        }
        public bool ContainValues([NotNull] TKey key)
        {
            return !ContainsKey(key) ? false : diccionari[key].Count != 0;
        }
        public bool ContainsValue([NotNull] TKey key, [NotNull] TValue value)
        {
            return ContainsKey(key) ? diccionari[key].ContainsKey(value) : false;
        }
        public bool ContainsValue([NotNull] TValue value)
        {
            bool encontrado = false;
            for (int i = 0; i < diccionari.Count && !encontrado; i++)
                encontrado = diccionari.GetValueAt(i).ContainsKey(value);
            return encontrado;
        }
        public TValue GetValueAt([NotNull] TKey key, int index)
        {
            return diccionari[key].GetValueAt(index);
        }

        #region IEnumerable implementation

        public IEnumerator<KeyValuePair<TKey, TValue[]>> GetEnumerator()
        {
            for (int i = 0; i < diccionari.Count; i++)
            {
                yield return new KeyValuePair<TKey, TValue[]>(diccionari.GetKey(i), diccionari.GetValueAt(i).Values.ToArray());
            }
        }

        #endregion

        #region IEnumerable implementation

       

        IEnumerator<KeyValuePair<TKey, TValue[]>> IEnumerable<KeyValuePair<TKey, TValue[]>>.GetEnumerator()
        {
            return GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        #endregion
    }
}