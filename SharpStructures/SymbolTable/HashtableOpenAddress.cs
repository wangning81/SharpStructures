using System;
using System.Collections.Generic;
using SharpStructures.Elementary;

namespace SharpStructures.SymbolTable
{
    public class HashtableOpenAddress<TKey, TValue> : AbstractHashtable<TKey, TValue>
    {
        private static readonly int[] PrimeDelta
            =
            {
                1, 3, 1, 5, 3, 3, 9, 3, 1,
                3, 19, 15, 1, 5, 1, 3, 9,
                3, 15, 3, 39, 5, 39, 57, 3, 35, 1
            };

        private int M;

        private TKey[] _keys;
        private int _lgM = 5;
        private int _size;
        private TValue[] _values;

        public HashtableOpenAddress()
        {
            Clear();
        }

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
            while (_keys[h] != null)
                h = (h + 1)%M;
            _keys[h] = key;
            _values[h] = value;
            _size++;
            if (_size == M/2)
                IncreaseSize();
        }

        public override bool ContainsKey(TKey key)
        {
            int h = GetPossiblePosition(key);
            return _keys[h] != null;
        }

        public override bool Remove(TKey key)
        {
            int h = GetPossiblePosition(key);
            if (_keys[h] == null) return false;

            _keys[h] = default(TKey);
            _values[h] = default(TValue);

            for (int i = h + 1; _keys[i] != null; i = (i + 1)%M)
            {
                TKey k = _keys[i];
                TValue v = _values[i];

                _keys[i] = default(TKey);
                _values[i] = default(TValue);

                Add(k, v);
            }
            _size--;
            if (_size <= M/4)
                DecreaesSize();
            return true;
        }

        public override bool TryGetValue(TKey key, out TValue value)
        {
            int h = GetPossiblePosition(key);
            value = _values[h];
            return value != null;
        }

        protected int GetPossiblePosition(TKey key)
        {
            int h = ToHashBucket(key);
            while (_keys[h] != null && !_keys[h].Equals(key))
                h = (h + 1)%M;
            return h;
        }

        public override void Clear()
        {
            _lgM = 5;
            M = GetM(_lgM);
            _keys = new TKey[M];
            _values = new TValue[M];
            _size = 0;
        }

        protected override IList<KeyValuePair<TKey, TValue>> GetAllItems()
        {
            var ret = new ArrayList<KeyValuePair<TKey, TValue>>();
            int count = 0;
            for (int i = 0; i < M && count < _size; i++)
            {
                if (_keys[i] != null)
                {
                    ret.Add(new KeyValuePair<TKey, TValue>(_keys[i], _values[i]));
                    count++;
                }
            }
            return ret;
        }

        protected static int GetM(int k)
        {
            return 1 << k - PrimeDelta[k];
        }

        protected void IncreaseSize()
        {
            Resize(true);
        }

        protected void DecreaesSize()
        {
            Resize(false);
        }

        protected void Resize(bool increase)
        {
            if (increase && _lgM == 31) throw new InvalidOperationException();
            if (!increase && _lgM == 5) return;

            var newTable = new HashtableOpenAddress<TKey, TValue>();
            newTable._lgM += increase ? 1 : -1;
            newTable.M = GetM(newTable._lgM);

            for (int i = 0; i < M; i++)
                if (_keys[i] != null)
                    newTable.Add(_keys[i], _values[i]);

            _keys = newTable._keys;
            _values = newTable._values;
            _lgM = newTable._lgM;
            M = newTable.M;
        }
    }
}