namespace Dependo.Walkers
{
    using System;

    public interface IDependencyWalker<T, TKey>
        where T : class
        where TKey : IComparable<TKey>, IEquatable<TKey>
    {
        void Walk(IDependency<T, TKey> dependency);
    }
}
