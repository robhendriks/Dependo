namespace Dependo.Builders
{
    using System;
    using System.Collections.Generic;

    public class DependencyBuilder<T, TKey> : IDependencyBuilder<T, TKey>
        where T : class
        where TKey : IComparable<TKey>, IEquatable<TKey>
    {
        private readonly List<IDependencyEdge<TKey>> _edges;

        public IDependency<T, TKey> Node { get; }

        public IEnumerable<IDependencyEdge<TKey>> Edges => _edges;

        public DependencyBuilder(IDependency<T, TKey> node)
        {
            _edges = new List<IDependencyEdge<TKey>>();
            Node = node ?? throw new ArgumentNullException(nameof(node));
        }

        public IDependencyBuilder<T, TKey> DependsOn(TKey key)
        {
            _edges.Add(new DependencyEdge<TKey>(Node.Key, key));
            return this;
        }
    }
}
