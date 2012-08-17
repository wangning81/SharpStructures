using System;
using System.Collections.Generic;
using System.Linq;

namespace SharpStructures.Graph
{
    public class Digraph<TVertex>
    {
        protected internal IDictionary<TVertex, List<TVertex>> _adjList = new Dictionary<TVertex, List<TVertex>>();

        public int VCount
        {
            get { return _adjList.Keys.Count; }
        }

        public TVertex AnyVertex
        {
            get
            {
                if (_adjList.Count == 0) throw new InvalidOperationException("graph is empty.");
                return _adjList.Keys.First();
            }
        }

        public IEnumerable<TVertex> Vertices
        {
            get { return _adjList.Keys; }
        }

        public void AddVertex(TVertex v)
        {
            if (_adjList.ContainsKey(v))
                throw new InvalidOperationException("vertex " + v + " is already in the graph.");
            _adjList.Add(v, new List<TVertex>());
        }

        public void AddEdge(TVertex u, TVertex v)
        {
            if (!_adjList.ContainsKey(u))
                _adjList.Add(u, new List<TVertex>());
            if (!_adjList.ContainsKey(v))
                _adjList.Add(v, new List<TVertex>());
            _adjList[u].Add(v);
        }

        public IList<TVertex> AdjList(TVertex v)
        {
            if (!_adjList.ContainsKey(v))
                throw new InvalidOperationException("vertex " + v + " is NOT in graph");
            return _adjList[v];
        }
    }
}