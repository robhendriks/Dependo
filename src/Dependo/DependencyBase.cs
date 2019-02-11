namespace Dependo
{
    using System;
    using System.Collections.Generic;
    using Exceptions;

    public abstract class DependencyBase<T, TKey> : IDependency<T, TKey>
        where T : DependencyBase<T, TKey>
        where TKey : IComparable<TKey>, IEquatable<TKey>
    {
        private readonly HashSet<T> _children;

        public TKey Key { get; private set; }

        public T Parent { get; private set; }

        public IEnumerable<T> Children => _children;

        protected DependencyBase(TKey key)
        {
            _children = new HashSet<T>(new DependencyBaseComparer<T, TKey>());
            Key = key;
            Parent = null;
        }

        protected virtual void ChildAdded(T child)
        {
            child.Parent = (T) this;
        }

        protected virtual void ChildRemoved(T child)
        {
            child.Parent = null;
        }

        public int CompareTo(T other)
        {
            if (ReferenceEquals(this, other))
            {
                return 0;
            }

            if (ReferenceEquals(null, other))
            {
                return 1;
            }

            return Key.CompareTo(other.Key);
        }

        public bool Equals(T other)
        {
            return CompareTo(other) == 0;
        }

        public bool HasChildren()
        {
            return _children.Count > 0;
        }

        public void AddChild(T child)
        {
            if (child == null)
            {
                throw new ArgumentNullException(nameof(child));
            }

            if (!_children.Add(child))
            {
                throw new DependencyException("Unable to add child.");
            }

            ChildAdded(child);
        }

        public bool TryAddChild(T child)
        {
            if (child == null || !_children.Add(child))
            {
                return false;
            }

            ChildAdded(child);
            return true;
        }

        public void RemoveChild(T child)
        {
            if (child == null)
            {
                throw new ArgumentNullException(nameof(child));
            }

            if (!_children.Remove(child))
            {
                throw new DependencyException("Unable to remove child.");
            }

            ChildRemoved(child);
        }

        public bool TryRemoveChild(T child)
        {
            if (child == null || !_children.Remove(child))
            {
                return false;
            }

            ChildRemoved(child);
            return true;
        }

        public bool HasChild(T child)
        {
            return child != null && _children.Contains(child);
        }

        public bool IsParentOf(T child)
        {
            if (child == null)
            {
                throw new ArgumentNullException(nameof(child));
            }

            return Equals(child.Parent);
        }

        public bool IsChildOf(T parent)
        {
            if (parent == null)
            {
                throw new ArgumentNullException(nameof(parent));
            }

            return parent.IsParentOf((T) this);
        }

        public bool IsSiblingOf(T sibling)
        {
            if (sibling == null)
            {
                throw new ArgumentNullException(nameof(sibling));
            }

            return sibling.Parent != null && sibling.Parent.Equals(Parent);
        }
    }

    internal class DependencyBaseComparer<T, TKey> : IEqualityComparer<DependencyBase<T, TKey>>
        where T : DependencyBase<T, TKey>
        where TKey : IComparable<TKey>, IEquatable<TKey>
    {
        public bool Equals(DependencyBase<T, TKey> x, DependencyBase<T, TKey> y)
        {
            return x.Key.Equals(y.Key);
        }

        public int GetHashCode(DependencyBase<T, TKey> obj)
        {
            return obj.Key.GetHashCode();
        }
    }
}
