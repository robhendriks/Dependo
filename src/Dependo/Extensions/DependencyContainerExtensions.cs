namespace Dependo.Extensions
{
    using System;
    using System.Collections.Generic;

    public static class DependencyContainerExtensions
    {
        public static IEnumerable<IDependency<T, TKey>> ResolveDependencies<T, TKey>(this IDependencyContainer<T, TKey> container)
            where T : class
            where TKey : IComparable<TKey>, IEquatable<TKey>
        {
            return new DependencyResolver<T, TKey>().ResolveDependencies(container);
        }
    }
}
