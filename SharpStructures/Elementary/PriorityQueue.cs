using System;

namespace SharpStructures.Elementary
{
    public class MinPq<T> : PriorityQueue<T> where T : IComparable<T>
    {
        protected override void Sink(int k)
        {
            while (k <= Size/2)
            {
                int j = 2*k;
                if (j < Size && Heap[j + 1].CompareTo(Heap[j]) < 0)
                    j++;
                if (Heap[k].CompareTo(Heap[j]) > 0)
                    Exch(k, j);
                else break;
                k = j;
            }
        }

        protected override void Swim(int k)
        {
            while (k > 1)
            {
                if (Heap[k].CompareTo(Heap[k/2]) < 0)
                    Exch(k, k/2);
                else break;
                k /= 2;
            }
        }
    }

    public class MaxPq<T> : PriorityQueue<T> where T : IComparable<T>
    {
        protected override void Sink(int k)
        {
            while (k <= Size/2)
            {
                int j = 2*k;
                if (j < Size && Heap[j + 1].CompareTo(Heap[j]) > 0)
                    j++;
                if (Heap[k].CompareTo(Heap[j]) < 0)
                    Exch(k, j);
                else break;
                k = j;
            }
        }

        protected override void Swim(int k)
        {
            while (k > 1)
            {
                if (Heap[k].CompareTo(Heap[k/2]) > 0)
                    Exch(k, k/2);
                else break;
                k /= 2;
            }
        }
    }

    public abstract class PriorityQueue<T> where T : IComparable<T>
    {
        protected int Capacity = 17;
        protected T[] Heap;
        protected int Size = 0;

        protected PriorityQueue(int capacity)
        {
            this.Capacity = capacity;
            Heap = new T[capacity];
        }

        protected PriorityQueue()
        {
            Heap = new T[Capacity];
        }

        public int Count
        {
            get { return Size; }
        }

        public void Enqueue(T item)
        {
            Heap[++Size] = item;
            Swim(Size);
            if (Size == Capacity - 1)
                Resize(2*Capacity);
        }

        public T Dequeue()
        {
            if (Size == 0) throw new InvalidOperationException("priority queue is empty.");
            T ret = Heap[1];
            Heap[1] = Heap[Size--];
            Sink(1);
            if (Size == Capacity/4)
                Resize(Capacity/2);
            return ret;
        }

        public T Peek()
        {
            if (Size == 0) throw new InvalidOperationException("priority queue is empty.");
            return Heap[1];
        }

        protected abstract void Swim(int k);
        protected abstract void Sink(int k);

        protected void Exch(int i, int j)
        {
            T t = Heap[i];
            Heap[i] = Heap[j];
            Heap[j] = t;
        }

        protected void Resize(int newCap)
        {
            var newHeap = new T[newCap];
            for (int i = 1; i <= Size; i++)
                newHeap[i] = Heap[i];
            Capacity = newCap;
            Heap = newHeap;
        }
    }
}