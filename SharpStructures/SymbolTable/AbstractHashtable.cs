using System.Collections;
using System.Collections.Generic;
using SharpStructures.Elementary;

namespace SharpStructures.SymbolTable
{
    public abstract class AbstractHashtable<TKey, TValue> : IHashtable<TKey, TValue>
    {
        protected abstract int Size { get; }
        protected abstract int Capacity { get; }

        #region IHashtable<TKey,TValue> Members

        public ICollection<TKey> Keys
        {
            get
            {
                IList<KeyValuePair<TKey, TValue>> allItems = GetAllItems();
                ICollection<TKey> keys = new ArrayList<TKey>();
                foreach (var item in allItems)
                    keys.Add(item.Key);
                return keys;
            }
        }

        public ICollection<TValue> Values
        {
            get
            {
                IList<KeyValuePair<TKey, TValue>> allItems = GetAllItems();
                ICollection<TValue> values = new ArrayList<TValue>();
                foreach (var item in allItems)
                    values.Add(item.Value);
                return values;
            }
        }

        public TValue this[TKey key]
        {
            get
            {
                TValue v;
                TryGetValue(key, out v);
                return v;
            }
            set { Add(key, value); }
        }

        public int Count
        {
            get { return Size; }
        }

        public bool IsReadOnly
        {
            get { return false; }
        }

        public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator()
        {
            return GetAllItems().GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public void Add(KeyValuePair<TKey, TValue> item)
        {
            Add(item.Key, item.Value);
        }

        public bool Contains(KeyValuePair<TKey, TValue> item)
        {
            TValue it = this[item.Key];
            return it != null && it.Equals(item.Value);
        }

        public void CopyTo(KeyValuePair<TKey, TValue>[] array, int arrayIndex)
        {
            IList<KeyValuePair<TKey, TValue>> allItems = GetAllItems();
            for (int i = arrayIndex; i < Size; i++)
                array[i] = allItems[i];
            return;
        }

        public bool Remove(KeyValuePair<TKey, TValue> item)
        {
            return Remove(item.Key);
        }

        public abstract bool ContainsKey(TKey key);
        public abstract void Add(TKey key, TValue value);
        public abstract bool Remove(TKey key);
        public abstract void Clear();
        public abstract bool TryGetValue(TKey key, out TValue value);

        #endregion

        protected int ToHashBucket(TKey key)
        {
            return (key.GetHashCode() & 0x7FFFFFFF)%Capacity;
        }

        protected abstract IList<KeyValuePair<TKey, TValue>> GetAllItems();
    }
}