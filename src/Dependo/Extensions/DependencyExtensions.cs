namespace Dependo.Extensions
{
    using System;
    using System.Collections.Generic;
    using Walkers;

    public static class DependencyExtensions
    {
        public static void Walk<T, TKey>(this IDependency<T, TKey> dependency, Action<WalkInfo, T> action)
            where T : class
            where TKey : IComparable<TKey>, IEquatable<TKey>
        {
            new AnonymousDependencyWalker<T, TKey>(action).Walk(dependency);
        }
    }
}
