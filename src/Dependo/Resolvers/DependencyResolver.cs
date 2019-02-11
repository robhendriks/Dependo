namespace Dependo.Resolvers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Containers;
    using Exceptions;

    public class DependencyResolver<T, TKey> : IDependencyResolver<T, TKey>
        where T : class
        where TKey : IComparable<TKey>, IEquatable<TKey>
    {
        public IEnumerable<IDependency<T, TKey>> ResolveDependencies(IDependencyContainer<T, TKey> container)
        {
            if (container == null)
            {
                throw new ArgumentNullException(nameof(container));
            }

            var sortedNodes = new List<IDependency<T, TKey>>();

            var nodes = container.Dependencies.ToList();
            if (nodes.Count == 0)
            {
                return sortedNodes;
            }

            var edges = container.Edges.ToList();
            if (edges.Count == 0)
            {
                return sortedNodes;
            }

            var nodesWithoutEdge = new Queue<IDependency<T, TKey>>(nodes.Where(node => edges.All(edge => !edge.To.Equals(node.Key))));

            while (nodesWithoutEdge.Count > 0)
            {
                var currentNode = nodesWithoutEdge.Dequeue();
                sortedNodes.Add(currentNode);

                var outgoingEdges = edges.Where(edge => edge.From.Equals(currentNode.Key)).ToList();

                foreach (var edge in outgoingEdges)
                {
                    var nextNode = nodes.FirstOrDefault(node => node.Key.Equals(edge.To));
                    if (nextNode == null)
                    {
                        throw new DependencyException($"Unknown dependency \"{edge.To}\".");
                    }

                    edges.Remove(edge);

                    if (edges.All(e2 => !e2.To.Equals(nextNode.Key)))
                    {
                        currentNode.AddChild((T) nextNode);
                        nodesWithoutEdge.Enqueue(nextNode);
                    }
                }
            }

            if (edges.Count > 0)
            {
                throw new DependencyException("Cyclic reference detected.");
            }

            return sortedNodes;
        }

        protected void Visit(IDependency<T, TKey> current)
        {
        }
    }
}
