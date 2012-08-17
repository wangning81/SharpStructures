using System;
using System.Collections;
using System.Collections.Generic;

namespace SharpStructures.Elementary
{
    public class LinkedList<T> : IList<T>
    {
        private Node _first;
        private int _size;

        #region IList<T> Members

        public T this[int index]
        {
            get { return GetAt(index); }
            set { SetAt(index, value); }
        }

        public void Add(T item)
        {
            var n = new Node(item, _first);
            _first = n;
            _size++;
        }

        public void Insert(int index, T item)
        {
            if (index < 0 || index > _size)
                throw new ArgumentOutOfRangeException("index");
            if (index == 0)
            {
                Add(item);
            }
            else
            {
                Node p = _first;
                for (int i = 0; i < index - 1; i++)
                    p = p.Next;
                p.Next = new Node(item, p.Next);
                _size++;
            }
        }

        public bool Contains(T item)
        {
            Node p = _first;
            while (p != null && !p.Data.Equals(item))
                p = p.Next;
            return p != null;
        }

        public void RemoveAt(int index)
        {
            if (index < 0 || index >= _size)
                throw new ArgumentOutOfRangeException("index");
            if (index == 0)
                _first = _first.Next;
            else
            {
                Node p = _first;
                for (int i = 0; i < index - 1; i++)
                    p = p.Next;
                p.Next = p.Next.Next;
            }
            _size--;
        }


        public int IndexOf(T item)
        {
            int index = 0;
            Node p = _first;
            while (p != null && !p.Data.Equals(item))
            {
                p = p.Next;
                index++;
            }
            return index < _size ? index : -1;
        }

        public void Clear()
        {
            _first = null;
            _size = 0;
        }

        public void CopyTo(T[] array, int arrayIndex)
        {
            Node p = _first;
            while (p != null)
                array[arrayIndex++] = p.Data;
        }

        public int Count
        {
            get { return _size; }
        }

        public bool IsReadOnly
        {
            get { return false; }
        }

        bool ICollection<T>.Remove(T item)
        {
            return Remove(item);
        }


        public IEnumerator<T> GetEnumerator()
        {
            return new LinkedListEnumerator(_first);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        #endregion

        public void AddRange(IEnumerable<T> items)
        {
            foreach (T item in items)
                Add(item);
        }

        public void Append(T item)
        {
            if (_first == null) Add(item);
            else
            {
                Node p = _first;
                while (p.Next != null)
                    p = p.Next;
                p.Next = new Node(item);
            }
        }

        public bool Remove(T item)
        {
            if (_first.Data.Equals(item))
            {
                _first = _first.Next;
                _size--;
                return true;
            }

            Node p = _first;
            while (p.Next != null && !p.Next.Data.Equals(item))
                p = p.Next;
            if (p.Next != null)
            {
                p.Next = p.Next.Next;
                _size--;
                return true;
            }
            return false;
        }

        public T GetAt(int index)
        {
            return GetNodeAt(index).Data;
        }

        public void SetAt(int index, T item)
        {
            GetNodeAt(index).Data = item;
        }

        internal void Append(Node n)
        {
            if (_first == null) _first = n;
            else
            {
                Node p = _first;
                while (p.Next != null)
                    p = p.Next;
            }
        }

        internal Node GetNode(T item)
        {
            Node p = _first;
            while (p != null && !p.Data.Equals(item))
                p = p.Next;
            return p;
        }

        internal Node GetNodeAt(int index)
        {
            if (index < 0 || index >= _size)
                throw new ArgumentOutOfRangeException("index");
            Node p = _first;
            for (int i = 0; i < index; i++)
                p = p.Next;
            return p;
        }

        #region Nested type: LinkedListEnumerator

        protected internal class LinkedListEnumerator : IEnumerator<T>
        {
            private readonly Node _first;
            private Node _current;

            public LinkedListEnumerator(Node first)
            {
                this._first = first;
            }

            #region IEnumerator<T> Members

            public T Current
            {
                get { return _current.Data; }
            }

            public void Dispose()
            {
            }

            object IEnumerator.Current
            {
                get { return _current.Data; }
            }

            public bool MoveNext()
            {
                if (_current == null) _current = _first;
                else _current = _current.Next;
                return _current != null;
            }

            public void Reset()
            {
                _current = null;
            }

            #endregion
        }

        #endregion

        #region Nested type: Node

        protected internal class Node
        {
            public T Data;
            public Node Next;

            public Node(T item, Node next)
            {
                Data = item;
                Next = next;
            }

            public Node(T item)
                : this(item, null)
            {
            }
        }

        #endregion
    }
}