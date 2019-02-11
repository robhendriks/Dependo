namespace Dependo.Walkers
{
    using System;
    using System.Linq;

    public abstract class BaseDependencyWalker<T, TKey> : IDependencyWalker<T, TKey>
        where T : class
        where TKey : IComparable<TKey>, IEquatable<TKey>
    {
        private readonly WalkInfo _info;

        protected BaseDependencyWalker()
        {
            _info = new WalkInfo {Level = 0, IsFirst = false, IsLast = false};
        }

        protected abstract void OnVisit(WalkInfo info, IDependency<T, TKey> dependency);

        public virtual void Walk(IDependency<T, TKey> dependency)
        {
            OnVisit(_info, dependency);

            ++_info.Level;

            var children = dependency.Children.ToList();
            var childCount = children.Count;

            for (var i = 0; i < childCount; ++i)
            {
                _info.IsFirst = i == 0;
                _info.IsLast = i == childCount - 1;

                Walk((IDependency<T, TKey>) children[i]);
            }

            --_info.Level;
        }
    }

    public class WalkInfo
    {
        public int Level { get; set; }

        public bool IsFirst { get; set; }

        public bool IsLast { get; set; }
    }
}
