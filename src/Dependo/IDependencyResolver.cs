namespace Dependo
{
    using System;

    public interface IDependencyResolver<T, TKey>
        where T : class
        where TKey : IComparable<TKey>, IEquatable<TKey>
    {
        void ResolveDependencies(IDependencyContainer<T, TKey> container);
    }
}
