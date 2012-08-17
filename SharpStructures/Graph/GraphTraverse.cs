using System;
using System.Collections.Generic;
using SharpStructures.Elementary;

namespace SharpStructures.Graph
{
    public abstract class GraphVisitor<TVertex>
    {
        protected IDictionary<TVertex, TVertex> EdgeTo;
        protected Digraph<TVertex> G;
        protected IDictionary<TVertex, bool> Marked;
        protected TVertex Source;

        protected GraphVisitor(Digraph<TVertex> g)
        {
            this.G = g;
            Init();
        }

        protected void Init()
        {
            Marked = new Dictionary<TVertex, bool>();
            EdgeTo = new Dictionary<TVertex, TVertex>();
        }

        public void Traverse(TVertex src)
        {
            Source = src;
            Init();

            Visit(src);
        }

        public void Traverse()
        {
            Init();
            foreach (TVertex v in G.Vertices)
            {
                if (!Marked.ContainsKey(v) || !Marked[v])
                {
                    Visit(v);
                }
            }
        }

        protected abstract void Visit(TVertex v);

        public bool Visited(TVertex u)
        {
            return Marked[u];
        }

        public virtual IList<TVertex> PathTo(TVertex dst)
        {
            if (!G._adjList.ContainsKey(dst))
                throw new InvalidOperationException("graph does not contains vertex " + dst);
            var ret = new ArrayList<TVertex>();
            if (!EdgeTo.ContainsKey(dst))
                return ret;

            var s = new Elementary.Stack<TVertex>();
            for (TVertex v = dst; !v.Equals(Source); v = EdgeTo[v])
            {
                s.Push(v);
            }
            s.Push(Source);
            while (!s.IsEmpty)
                ret.Add(s.Pop());
            return ret;
        }
    }
}