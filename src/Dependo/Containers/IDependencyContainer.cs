namespace Dependo.Containers
{
    using System;
    using System.Collections.Generic;

    public interface IDependencyContainer<T, TKey>
        where T : class
        where TKey : IComparable<TKey>, IEquatable<TKey>
    {
        IEnumerable<IDependency<T, TKey>> Dependencies { get; }

        IEnumerable<IDependencyEdge<TKey>> Edges { get; }

        IDependencyContainer<T, TKey> RegisterDependency(T dependency, params TKey[] keys);
    }
}
