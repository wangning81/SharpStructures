using System;
using System.Collections;
using System.Collections.Generic;

namespace SharpStructures.Elementary
{
    public class ArrayList<T> : IList<T>
    {
        private int _capacity = 16;
        private T[] _list;
        private int _size;

        public ArrayList()
        {
            Clear();
        }

        #region IList<T> Members

        public int IndexOf(T item)
        {
            for (int i = 0; i < _size; i++)
                if (item.Equals(_list[i]))
                    return i;
            return -1;
        }

        public void Insert(int index, T item)
        {
            if (index < 0 || index > _size)
                throw new ArgumentOutOfRangeException("index");
            for (int i = _size - 1; i >= index; i--)
                _list[i + 1] = _list[i];
            _list[index] = item;
            _size++;
            if (_size == _capacity)
                Resize(2*_capacity);
        }

        public void RemoveAt(int index)
        {
            if (index < 0 || index >= _size)
                throw new ArgumentOutOfRangeException("index");

            for (int i = index + 1; i < _size; i++)
                _list[i - 1] = _list[i];
            _size--;
            if (_size <= _capacity/4)
                Resize(_capacity/2);
        }

        public T this[int index]
        {
            get { return _list[index]; }
            set { _list[index] = value; }
        }


        public void Add(T item)
        {
            _list[_size++] = item;
            if (_size == _capacity)
                Resize(2*_capacity);
        }

        public void Clear()
        {
            _capacity = 16;
            _list = new T[_capacity];
            _size = 0;
        }

        public bool Contains(T item)
        {
            foreach (T i in _list)
                if (i.Equals(item))
                    return true;
            return false;
        }

        public void CopyTo(T[] array, int arrayIndex)
        {
            for (int i = 0; i < _size; i++)
                array[arrayIndex++] = _list[i];
        }

        public int Count
        {
            get { return _size; }
        }

        public bool IsReadOnly
        {
            get { return false; }
        }

        public bool Remove(T item)
        {
            int i = IndexOf(item);
            if (i == -1) return false;
            RemoveAt(i);
            return true;
        }

        public IEnumerator<T> GetEnumerator()
        {
            return new ArrayListEnumerator(_list, _size);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        #endregion

        protected internal void Resize(int newCap)
        {
            var newList = new T[newCap];
            for (int i = 0; i < _size; i++)
                newList[i] = _list[i];
            _list = newList;
            _capacity = newCap;
        }

        public void AddRange(IEnumerable<T> items)
        {
            foreach (T item in items)
                Add(item);
        }

        #region Nested type: ArrayListEnumerator

        protected internal class ArrayListEnumerator : IEnumerator<T>
        {
            private readonly T[] _list;
            private readonly int _size;
            private int _index = -1;

            public ArrayListEnumerator(T[] list, int size)
            {
                this._list = list;
                this._size = size;
            }

            #region IEnumerator<T> Members

            public T Current
            {
                get { return _list[_index]; }
            }

            public void Dispose()
            {
            }

            object IEnumerator.Current
            {
                get { return _list[_index]; }
            }

            public bool MoveNext()
            {
                _index++;
                return _index < _size;
            }

            public void Reset()
            {
                _index = -1;
            }

            #endregion
        }

        #endregion
    }
}