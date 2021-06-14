using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading;

namespace Gabriel.Cat.S.Utilitats
{
    public class Llista<T> : IList, IList<T>
    {
        List<T> list;
        Semaphore semaphore;
        public event EventHandler<ListEventArgs<T>> Updated;
        public Llista(IEnumerable<T> initialElements = null)
        {
            list = new List<T>();
            semaphore = new Semaphore(1, 1);
            if (initialElements != null)
                list.AddRange(initialElements);
        }

        public T this[int index]
        {
            get
            {
                T value;
                try
                {
                    semaphore.WaitOne();
                    value = list[index];

                }
                catch { throw; }
                finally
                {
                    semaphore.Release();
                }
                return value;
            }
            set
            {

                try
                {
                    semaphore.WaitOne();
                    list[index] = value;
               

                }
                catch { throw; }
                finally
                {
                    semaphore.Release();
                    if (Updated != null)
                        Updated(this, new ListEventArgs<T>(this, value));
                }
            }
        }

        object IList.this[int index] { get => this[index]; set => this[index] = (T)value; }

        public int Count
        {
            get
            {
                int count;
                try
                {
                    semaphore.WaitOne();
                    count = list.Count;

                }
                catch { throw; }
                finally
                {
                    semaphore.Release();
                }
                return count;
            }
        }

        public bool IsReadOnly => false;

        bool IList.IsFixedSize => false;

        bool IList.IsReadOnly => IsReadOnly;

        int ICollection.Count => Count;

        bool ICollection.IsSynchronized
        {
            get
            {
                bool isSynchronized;
                try
                {
                    semaphore.WaitOne();
                    isSynchronized = ((IList)list).IsSynchronized;
                }
                catch { throw; }
                finally
                {
                    semaphore.Release();
                }
                return isSynchronized;
            }
        }

        object ICollection.SyncRoot
        {
            get
            {
                object syncRoot;
                try
                {
                    semaphore.WaitOne();
                    syncRoot = ((IList)list).SyncRoot;
                }
                catch { throw; }
                finally
                {
                    semaphore.Release();
                }
                return syncRoot;
            }
        }
        public void AddRange([NotNull] IList<T> items)
        {
            try
            {
                semaphore.WaitOne();
           
                for (int i = 0; i < items.Count; i++)
                    list.Add(items[i]);
    
            }
            catch { throw; }
            finally
            {
                semaphore.Release();
                if (Updated != null)
                    Updated(this, new ListEventArgs<T>(this, items));
            }
        }
        public void Add(T item)
        {
            try
            {
                semaphore.WaitOne();
                list.Add(item);
             
            }
            catch { throw; }
            finally
            {
                semaphore.Release();
                if (Updated != null)
                    Updated(this, new ListEventArgs<T>(this, item));
            }
        }

        public void Clear()
        {
            try
            {
                semaphore.WaitOne();
                list.Clear();
                
            }
            catch { throw; }
            finally
            {
                semaphore.Release();
                if (Updated != null)
                    Updated(this, new ListEventArgs<T>(this));
            }
        }

        public bool Contains(T item)
        {
            bool contains;
            try
            {
                semaphore.WaitOne();
                contains = list.Contains(item);

            }
            catch { throw; }
            finally
            {
                semaphore.Release();
            }
            return contains;
        }

        public void CopyTo([NotNull] T[] array, int arrayIndex)
        {
            try
            {
                semaphore.WaitOne();
                list.CopyTo(array, arrayIndex);

            }
            catch { throw; }
            finally
            {
                semaphore.Release();
            }
        }

        public IEnumerator<T> GetEnumerator()
        {
            IEnumerator<T> enumerator;
            try
            {
                semaphore.WaitOne();
                enumerator = list.GetEnumerator();

            }
            catch { throw; }
            finally
            {
                semaphore.Release();
            }
            return enumerator;
        }

        public int IndexOf(T item)
        {
            int indexOf;
            try
            {
                semaphore.WaitOne();
                indexOf = list.IndexOf(item);

            }
            catch { throw; }
            finally
            {
                semaphore.Release();
            }
            return indexOf;
        }

        public void Insert(int index, T item)
        {
       
            try
            {
                semaphore.WaitOne();
                list.Insert(index,item);

            }
            catch { throw; }
            finally
            {
                semaphore.Release();
            }
            
        }

        public bool Remove(T item)
        {
            bool removed;
            try
            {
                semaphore.WaitOne();
                removed = list.Remove(item);

            }
            catch { throw; }
            finally
            {
                semaphore.Release();
            }
            return removed;
        }

        public void RemoveAt(int index)
        {
            
            try
            {
                semaphore.WaitOne();
                list.RemoveAt(index);

            }
            catch { throw; }
            finally
            {
                semaphore.Release();
            }
           
        }

        int IList.Add(object value)
        {
            int pos;
            try
            {
                semaphore.WaitOne();
                pos = ((IList)list).Add(value);

            }
            catch { throw; }
            finally
            {
                semaphore.Release();
            }
            return pos;
        }

        void IList.Clear()
        {
            Clear();
        }

        bool IList.Contains(object value)
        {
            return Contains((T)value);
        }

        void ICollection.CopyTo([NotNull] Array array, int index)
        {
           
            try
            {
                semaphore.WaitOne();
                ((IList)list).CopyTo(array,index);

            }
            catch { throw; }
            finally
            {
                semaphore.Release();
            }
     
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        int IList.IndexOf(object value)
        {
            return IndexOf((T)value);
        }

        void IList.Insert(int index, object value)
        {
            Insert(index, (T)value);
        }

        void IList.Remove(object value)
        {
            Remove((T)value);
        }

        void IList.RemoveAt(int index)
        {
            RemoveAt(index);
        }

    }
    public class ListEventArgs<T> : EventArgs
    {
        public ListEventArgs(object list) : this(list, new T[0])
        { }
        public ListEventArgs(object list, T item) : this(list, new T[] { item })
        { }
        public ListEventArgs(object list, IEnumerable<T> items) : this(list, items.ToArray())
        { }
        public ListEventArgs(object list, IList<T> items)
        {
            this.List = list;
            this.Items = items;
        }
        public object List { get; private set; }
        public IList<T> Items { get; private set; }
    }


}
