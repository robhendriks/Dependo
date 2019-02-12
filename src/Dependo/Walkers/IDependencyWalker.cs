namespace Dependo.Walkers
{
    using System;

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="TKey"></typeparam>
    public interface IDependencyWalker<T, TKey>
        where T : class
        where TKey : IComparable<TKey>, IEquatable<TKey>
    {
        void Walk(IDependency<T, TKey> dependency);
    }
}
