namespace Dependo.Builders
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="TKey"></typeparam>
    public interface IDependencyBuilder<T, TKey>
        where T : class
        where TKey : IComparable<TKey>, IEquatable<TKey>
    {
        IDependency<T, TKey> Node { get; }

        IEnumerable<IDependencyEdge<TKey>> Edges { get; }

        IDependencyBuilder<T, TKey> DependsOn(TKey key);
    }
}
