namespace Dependo.Extensions
{
    using System;

    public static class DependencyContainerExtensions
    {
        public static void ResolveDependencies<T, TKey>(this IDependencyContainer<T, TKey> container)
            where T : class
            where TKey : IComparable<TKey>, IEquatable<TKey>
        {
            new DependencyResolver<T, TKey>().ResolveDependencies(container);
        }
    }
}
