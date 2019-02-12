namespace Dependo.Walkers
{
    using System;

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="TKey"></typeparam>
    public class AnonymousDependencyWalker<T, TKey> : BaseDependencyWalker<T, TKey>
        where T : class
        where TKey : IComparable<TKey>, IEquatable<TKey>
    {
        private readonly Action<WalkInfo, T> _action;

        public AnonymousDependencyWalker(Action<WalkInfo, T> action)
        {
            _action = action ?? throw new ArgumentNullException(nameof(action));
        }

        protected override void OnVisit(WalkInfo info, IDependency<T, TKey> dependency)
        {
            _action(info, (T) dependency);
        }
    }
}
