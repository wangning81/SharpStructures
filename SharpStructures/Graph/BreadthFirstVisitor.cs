using SharpStructures.Elementary;

namespace SharpStructures.Graph
{
    public class BreadthFirstVisitor<TVertex> : GraphVisitor<TVertex>
    {
        public BreadthFirstVisitor(Digraph<TVertex> g) : base(g)
        {
        }

        protected override void Visit(TVertex src)
        {
            TVertex v = src;
            var q = new Queue<TVertex>();
            Marked[v] = true;
            q.Enqueue(v);
            while (!q.IsEmpty)
            {
                TVertex u = q.Dequeque();
                foreach (TVertex w in G.AdjList(u))
                {
                    if (!Marked[w])
                    {
                        EdgeTo[w] = u;
                        Marked[w] = true;
                        q.Enqueue(w);
                    }
                }
            }
        }
    }
}