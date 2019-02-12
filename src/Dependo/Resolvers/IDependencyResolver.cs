namespace Dependo.Resolvers
{
    using System;
    using System.Collections.Generic;
    using Containers;

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="TKey"></typeparam>
    public interface IDependencyResolver<T, TKey>
        where T : class
        where TKey : IComparable<TKey>, IEquatable<TKey>
    {
        IEnumerable<IDependency<T, TKey>> ResolveDependencies(IDependencyContainer<T, TKey> container);
    }
}
