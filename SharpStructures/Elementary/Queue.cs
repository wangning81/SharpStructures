using System;

namespace SharpStructures.Elementary
{
    public class Queue<T>
    {
        private int _capacity = 16;
        private int _head;
        private T[] _queue;
        private int _tail;

        public Queue()
        {
            _queue = new T[_capacity];
        }

        public Queue(int capacity)
        {
            this._capacity = capacity;
            _queue = new T[capacity];
        }

        public int Count
        {
            get
            {
                if (_head <= _tail)
                    return _tail - _head;
                else
                    return _tail + _capacity - _head;
            }
        }

        public bool IsEmpty
        {
            get { return _tail == _head; }
        }

        public void Enqueue(T item)
        {
            _queue[_tail] = item;
            _tail = (_tail + 1)%_capacity;
            if (Count == _capacity)
                Resize(2*_capacity);
        }

        public T Dequeque()
        {
            if (IsEmpty) throw new InvalidOperationException("Queue is empty.");
            T ret = _queue[_head];
            _head = (_head + 1)%_capacity;
            if (Count == _capacity/4)
                Resize(_capacity/2);
            return ret;
        }

        private void Resize(int newCap)
        {
            var newQ = new T[newCap];
            int size = Count;
            if (_head <= _tail)
            {
                for (int i = _head; i < _tail; i++)
                    newQ[i - _head] = _queue[i];
            }
            else
            {
                for (int i = _head; i < _capacity; i++)
                    newQ[i - _head] = _queue[i];
                for (int i = 0; i < _tail; i++)
                    newQ[_capacity - _head + 1] = _queue[i];
            }
            _queue = newQ;
            _head = 0;
            _tail = size;
            _capacity = newCap;
        }
    }
}