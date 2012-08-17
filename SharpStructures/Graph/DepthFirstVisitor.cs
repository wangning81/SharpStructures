namespace SharpStructures.Graph
{
    public class DepthFirstVisitor<TVertex> : GraphVisitor<TVertex>
    {
        public DepthFirstVisitor(Digraph<TVertex> g) : base(g)
        {
        }

        protected override void Visit(TVertex v)
        {
            Marked[v] = true;
            foreach (TVertex u in G.AdjList(v))
            {
                if (!Marked[u])
                {
                    EdgeTo[u] = v;
                    Visit(u);
                }
            }
        }
    }
}