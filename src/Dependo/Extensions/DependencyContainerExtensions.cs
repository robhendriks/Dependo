namespace Dependo.Extensions
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Containers;
    using Resolvers;

    public static class DependencyContainerExtensions
    {
        public static IEnumerable<T> ResolveDependencies<T, TKey>(this IDependencyContainer<T, TKey> container, bool rootsOnly = true)
            where T : class
            where TKey : IComparable<TKey>, IEquatable<TKey>
        {
            var nodes = new DependencyResolver<T, TKey>().ResolveDependencies(container);
            return (rootsOnly ? nodes.Where(node => node.Parent == null) : nodes).Cast<T>();
        }
    }
}
