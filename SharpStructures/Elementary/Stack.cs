using System;

namespace SharpStructures.Elementary
{
    public class Stack<T>
    {
        protected int Capacity = 16;
        protected T[] ArrayStack;
        protected int Top = -1;

        public Stack()
        {
            ArrayStack = new T[Capacity];
        }

        public Stack(int capacity)
        {
            this.Capacity = capacity;
            ArrayStack = new T[capacity];
        }

        public bool IsEmpty
        {
            get { return Count == 0; }
        }

        public virtual int Count
        {
            get { return Top + 1; }
        }

        public virtual void Push(T item)
        {
            ArrayStack[++Top] = item;
            if (Count == Capacity)
                Resize(2*Capacity);
        }

        public virtual T Pop()
        {
            if (IsEmpty) throw new InvalidOperationException("Stack is empty.");
            T ret = ArrayStack[Top--];
            if (Count == Capacity/4)
                Resize(Capacity/2);
            return ret;
        }

        public virtual T Peek()
        {
            if (IsEmpty) throw new InvalidOperationException("Stack is empty.");
            return ArrayStack[Top];
        }

        protected void Resize(int newCap)
        {
            var newStack = new T[newCap];
            for (int i = 0; i <= Top; i++)
                newStack[i] = ArrayStack[i];
            ArrayStack = newStack;
            Capacity = newCap;
        }
    }
}