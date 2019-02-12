namespace Dependo.Builders
{
    using System;
    using System.Collections.Generic;

    public interface IDependencyBuilder<T, TKey>
        where T : class
        where TKey : IComparable<TKey>, IEquatable<TKey>
    {
        IDependency<T, TKey> Node { get; }

        IEnumerable<IDependencyEdge<TKey>> Edges { get; }

        IDependencyBuilder<T, TKey> DependsOn(TKey key);
    }
}
