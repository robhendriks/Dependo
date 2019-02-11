namespace Dependo
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class DependencyResolver<T, TKey> : IDependencyResolver<T, TKey>
        where T : class
        where TKey : IComparable<TKey>, IEquatable<TKey>
    {
        public void ResolveDependencies(IDependencyContainer<T, TKey> container)
        {
            if (container == null)
            {
                throw new ArgumentNullException(nameof(container));
            }

            var nodes = container.Dependencies.ToList();
            var edges = container.Edges.ToList();

            var sortedNodes = new List<IDependency<T, TKey>>();
            var noEdgeNodes = new Queue<IDependency<T, TKey>>(nodes.Where(node => edges.All(edge => !edge.To.Equals(node.Key))));

            while (noEdgeNodes.Count > 0)
            {
                var n = noEdgeNodes.Dequeue();
                sortedNodes.Add(n);

                var myEdges = edges.Where(edge => edge.From.Equals(n.Key)).ToList();

                foreach (var edge in myEdges)
                {
                    var m = nodes.FirstOrDefault(node => node.Key.Equals(edge.To));
                    if (m == null)
                    {
                        throw new DependencyException($"Unknown dependency \"{edge.To}\".");
                    }

                    edges.Remove(edge);

                    if (edges.All(e2 => !e2.To.Equals(m.Key)))
                    {
                        noEdgeNodes.Enqueue(m);
                    }
                }
            }

            if (edges.Count > 0)
            {
                throw new DependencyException("Cyclic reference detected.");
            }

            foreach (var l in sortedNodes)
            {
                Console.WriteLine(l.Key);
            }

            /*
                 L ← Empty list that will contain the sorted elements
                 S ← Set of all nodes with no incoming edge
                 while S is non-empty do
                     remove a node n from S
                     add n to tail of L
                     for each node m with an edge e from n to m do
                         remove edge e from the graph
                         if m has no other incoming edges then
                             insert m into S
                 if graph has edges then
                     return error   (graph has at least one cycle)
                 else 
                     return L   (a topologically sorted order)
              */
        }

        protected void Visit(IDependency<T, TKey> current)
        {
        }
    }
}
