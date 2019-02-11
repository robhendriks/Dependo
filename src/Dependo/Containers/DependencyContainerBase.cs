namespace Dependo.Containers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class DependencyContainerBase<T, TKey> : IDependencyContainer<T, TKey>
        where T : DependencyBase<T, TKey>
        where TKey : IComparable<TKey>, IEquatable<TKey>
    {
        private readonly List<DependencyBase<T, TKey>> _roots;
        private readonly List<DependencyEdge<TKey>> _edges;

        public IEnumerable<IDependency<T, TKey>> Dependencies => _roots;

        public IEnumerable<IDependencyEdge<TKey>> Edges => _edges;

        public DependencyContainerBase()
        {
            _roots = new List<DependencyBase<T, TKey>>();
            _edges = new List<DependencyEdge<TKey>>();
        }

        public IDependencyContainer<T, TKey> RegisterDependency(T dependency, params TKey[] keys)
        {
            if (dependency == null)
            {
                throw new ArgumentNullException(nameof(dependency));
            }

            _roots.Add(dependency);
            keys?.Select(key => new DependencyEdge<TKey>(dependency.Key, key))
                .ToList()
                .ForEach(_edges.Add);

            return this;
        }
    }
}
