using System;

namespace SharpStructures.SymbolTable
{
    public class BinaryNode<T>
    {
        public T Key;
        public BinaryNode<T> Left;
        public BinaryNode<T> Parent;
        public BinaryNode<T> Right;

        public BinaryNode()
        {
        }

        public BinaryNode(T key)
        {
            Key = key;
        }
    }

    public class BinarySearchTree<TKey, TValue> where TKey : IComparable<TKey>
    {
        protected Node root;

        public Node Root
        {
            get { return root; }
        }

        public int Count
        {
            get { return SizeOf(root); }
        }

        public virtual TValue GetValue(TKey key)
        {
            Node n = GetNode(key);
            return n != null ? n.Value : default(TValue);
        }

        public virtual void Add(TKey key, TValue val)
        {
            root = Add(root, key, val);
        }

        public bool ContainsKey(TKey key)
        {
            Node n = GetNode(key);
            return n != null;
        }

        public virtual bool Remove(TKey key)
        {
            bool ret = ContainsKey(key);
            root = Remove(root, key);
            return ret;
        }

        protected virtual Node Remove(Node n, TKey key)
        {
            if (n == null) return null;
            int cmp = n.Key.CompareTo(key);
            if (cmp > 0)
                n.Right = Remove(n.Right, key);
            else if (cmp < 0)
                n.Left = Remove(n.Left, key);
            else
            {
                if (n.Left == null || n.Right == null)
                {
                    if (n.Left != null)
                        return n.Left;
                    else return n.Right;
                }
                Node succ = Min(n.Right);
                n.Key = succ.Key;
                n.Value = succ.Value;
                n.Right = RemoveMin(n.Right);
            }
            n.Size = SizeOf(n.Left) + SizeOf(n.Right) + 1;
            return n;
        }

        protected Node Min(Node node)
        {
            while (node.Left != null)
                node = node.Left;
            return node;
        }

        protected virtual Node Add(Node n, TKey key, TValue val)
        {
            if (n == null) return new Node(key, val);
            int cmp = key.CompareTo(n.Key);
            if (cmp > 0)
                n.Right = Add(n.Right, key, val);
            else if (cmp < 0)
                n.Left = Add(n.Left, key, val);
            else
                n.Value = val;

            n.Size = SizeOf(n.Left) + SizeOf(n.Right) + 1;
            return n;
        }

        public virtual void RemoveMin()
        {
            if (root == null) return;
            root = RemoveMin(root);
        }

        protected virtual Node RemoveMin(Node n)
        {
            if (n.Left == null) return null;
            n.Left = RemoveMin(n.Left);
            n.Size = SizeOf(n.Left) + SizeOf(n.Right) + 1;
            return n;
        }

        public virtual void RemoveMax()
        {
            if (root == null) return;
            root = RemoveMax(root);
        }

        protected virtual Node RemoveMax(Node n)
        {
            if (n.Right == null) return null;
            n.Right = RemoveMax(n.Right);
            n.Size = SizeOf(n.Left) + SizeOf(n.Right) + 1;
            return n;
        }

        protected int SizeOf(Node n)
        {
            return n == null ? 0 : n.Size;
        }

        protected virtual Node GetNode(TKey key)
        {
            Node n = root;
            while (n != null)
            {
                int cmp = key.CompareTo(n.Key);
                if (cmp > 0)
                    n = n.Right;
                else if (cmp < 0)
                    n = n.Left;
                else
                    return n;
            }
            return null;
        }

        #region Nested type: Node

        public class Node
        {
            public TKey Key;
            public Node Left;
            public Node Right;
            public int Size;
            public TValue Value;

            public Node(TKey key, TValue val)
            {
                Key = key;
                Value = val;
            }
        }

        #endregion
    }
}