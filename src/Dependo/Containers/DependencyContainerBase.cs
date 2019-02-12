namespace Dependo.Containers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Builders;

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="TKey"></typeparam>
    public abstract class DependencyContainerBase<T, TKey> : IDependencyContainer<T, TKey>
        where T : DependencyBase<T, TKey>
        where TKey : IComparable<TKey>, IEquatable<TKey>
    {
        private readonly List<DependencyBase<T, TKey>> _roots;
        private readonly List<DependencyEdge<TKey>> _edges;

        public IEnumerable<IDependency<T, TKey>> Dependencies => _roots;

        public IEnumerable<IDependencyEdge<TKey>> Edges => _edges;

        protected DependencyContainerBase()
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

        public IDependencyContainer<T, TKey> RegisterDependency(IDependencyBuilder<T, TKey> builder)
        {
            _roots.Add((DependencyBase<T, TKey>) builder.Node);
            builder.Edges.ToList()
                .ForEach(edge => _edges.Add((DependencyEdge<TKey>) edge));

            return this;
        }
    }
}
