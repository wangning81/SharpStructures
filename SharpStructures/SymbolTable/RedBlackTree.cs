using System;

namespace SharpStructures.SymbolTable
{
    public class RedBlackTree<TKey, TValue> : BinarySearchTree<TKey, TValue> where TKey : IComparable<TKey>
    {
        public override void Add(TKey key, TValue val)
        {
            root = Add(root, key, val);
            ((RbNode) root).IsRed = false;
        }

        public override bool Remove(TKey key)
        {
            bool found = ContainsKey(key);
            return found;
        }


        public override void RemoveMin()
        {
            if (root == null) return;
            var rbr = root as RbNode;
            if (!IsRed(root.Left) && !IsRed(root.Right))
                rbr.IsRed = true;

            root = RemoveMin(root);

            if (root != null)
                ((RbNode) root).IsRed = false;
        }

        protected override Node RemoveMin(Node n)
        {
            if (n.Left == null) return null;
            if (!IsRed(n.Left) && !IsRed(n.Left.Left))
            {
                n = MoveRedToLeft(n as RbNode);
            }
            n.Left = RemoveMin(n.Left);
            return Balance(n as RbNode);
        }

        private RbNode MoveRedToLeft(RbNode n)
        {
            FlipColors(n);
            if (IsRed(n.Right.Left))
            {
                n.Right = RotateToRight((RbNode) n.Right);
                n = RotateToLeft(n);
            }
            return n;
        }

        public override void RemoveMax()
        {
        }

        protected override Node Add(Node n, TKey key, TValue val)
        {
            if (n == null) return new RbNode(key, val);
            int cmp = key.CompareTo(n.Key);
            if (cmp > 0)
                n.Right = Add(n.Right, key, val);
            else if (cmp < 0)
                n.Left = Add(n.Left, key, val);
            else
                n.Value = val;

            if (IsRed(n.Right) && !IsRed(n.Left))
                n = RotateToLeft(n as RbNode);
            if (IsRed(n.Left) && IsRed(n.Left.Left))
                n = RotateToRight(n as RbNode);
            if (IsRed(n.Left) && IsRed(n.Right))
                FlipColors(n as RbNode);

            n.Size = SizeOf(n.Left) + SizeOf(n.Right) + 1;
            return n;
        }

        protected internal RbNode Balance(RbNode n)
        {
            if (IsRed(n.Right))
                n = RotateToLeft(n);
            if (IsRed(n.Left) && IsRed(n.Left.Left))
                n = RotateToRight(n);
            if (IsRed(n.Left) && IsRed(n.Right))
                FlipColors(n);
            n.Size = SizeOf(n.Left) + SizeOf(n.Right) + 1;
            return n;
        }

        protected internal bool IsRed(Node n)
        {
            var rn = n as RbNode;
            return rn != null && rn.IsRed;
        }

        protected internal RbNode RotateToLeft(RbNode n)
        {
            var x = n.Right as RbNode;
            n.Right = x.Left;
            x.Left = n;
            x.IsRed = n.IsRed;
            n.IsRed = true;
            n.Size = SizeOf(n.Left) + SizeOf(n.Right) + 1;
            x.Size = SizeOf(x.Left) + SizeOf(x.Right) + 1;
            return x;
        }

        protected internal RbNode RotateToRight(RbNode n)
        {
            var x = n.Left as RbNode;
            n.Left = x.Right;
            x.Right = n;
            x.IsRed = n.IsRed;
            n.IsRed = true;
            n.Size = SizeOf(n.Left) + SizeOf(n.Right) + 1;
            x.Size = SizeOf(x.Left) + SizeOf(x.Right) + 1;
            return x;
        }

        protected internal void FlipColors(RbNode n)
        {
            n.IsRed = !n.IsRed;
            ((RbNode) n.Left).IsRed = !((RbNode) n.Left).IsRed;
            ((RbNode) n.Right).IsRed = !((RbNode) n.Right).IsRed;
        }

        #region Nested type: RBNode

        protected internal class RbNode : Node
        {
            public bool IsRed = true;

            public RbNode(TKey key, TValue val) : base(key, val)
            {
                IsRed = true;
            }
        }

        #endregion
    }
}