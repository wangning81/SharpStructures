using System.Collections.Generic;
using SharpStructures.Elementary;

namespace SharpStructures.SymbolTable
{
    public class HashtableChaining<TKey, TValue> : AbstractHashtable<TKey, TValue>
    {
        private const int M = 997;

        private readonly SharpStructures.Elementary.LinkedList<KeyValuePair<TKey, TValue>>[] _buckets =
            new SharpStructures.Elementary.LinkedList<KeyValuePair<TKey, TValue>>[M];

        private int _size;

        protected override int Capacity
        {
            get { return M; }
        }

        protected override int Size
        {
            get { return _size; }
        }

        public override void Add(TKey key, TValue value)
        {
            int h = ToHashBucket(key);
            if (_buckets[h] == null) _buckets[h] = new SharpStructures.Elementary.LinkedList<KeyValuePair<TKey, TValue>>();
            var pair = new KeyValuePair<TKey, TValue>(key, value);
            int index = _buckets[h].IndexOf(pair);
            if (index == -1)
            {
                _buckets[h].Add(pair);
                _size++;
            }
            else _buckets[h].GetAt(index).Value = value;
        }

        public override bool ContainsKey(TKey key)
        {
            int h = ToHashBucket(key);
            if (_buckets[h] == null
                || !_buckets[h].Contains(new KeyValuePair<TKey, TValue>(key, default(TValue))))
                return false;
            return true;
        }

        protected override IList<KeyValuePair<TKey, TValue>> GetAllItems()
        {
            var ret = new ArrayList<KeyValuePair<TKey, TValue>>();
            for (int i = 0; i < M; i++)
                if (_buckets[i] != null)
                    ret.AddRange(_buckets[i]);
            return ret;
        }

        public override bool Remove(TKey key)
        {
            int h = ToHashBucket(key);
            if (_buckets[h] == null) return false;
            bool ret = _buckets[h].Remove(new KeyValuePair<TKey, TValue>(key, default(TValue)));
            if (ret) _size--;
            return ret;
        }

        public override bool TryGetValue(TKey key, out TValue value)
        {
            int h = ToHashBucket(key);
            if (_buckets[h] == null)
            {
                value = default(TValue);
                return false;
            }
            SharpStructures.Elementary.LinkedList<KeyValuePair<TKey, TValue>>.Node node =
                _buckets[h].GetNode(new KeyValuePair<TKey, TValue>(key, default(TValue)));
            if (node != null)
            {
                value = node.Data.Value;
                return true;
            }
            value = default(TValue);
            return false;
        }

        public override void Clear()
        {
            for (int i = 0; i < M; i++)
                if (_buckets[i] != null)
                    _buckets[i].Clear();
            _size = 0;
        }
    }
}