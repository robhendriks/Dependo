namespace Dependo
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Builders;
    using Exceptions;

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="TKey"></typeparam>
    public abstract class DependencyBase<T, TKey> : IDependency<T, TKey>
        where T : DependencyBase<T, TKey>
        where TKey : IComparable<TKey>, IEquatable<TKey>
    {
        private readonly HashSet<T> _children;

        public TKey Key { get; }

        public T Parent { get; private set; }

        public IEnumerable<T> Children => _children;

        public IEnumerable<T> Ancestors => GetAncestors();

        public IEnumerable<T> Descendants => GetDescendants();

        protected DependencyBase(TKey key)
        {
            _children = new HashSet<T>(new DependencyBaseComparer<T, TKey>());
            Key = key;
            Parent = null;
        }

        public IDependencyBuilder<T, TKey> DependsOn(TKey key)
        {
            return new DependencyBuilder<T, TKey>(this).DependsOn(key);
        }

        protected IEnumerable<T> GetAncestors()
        {
            if (Parent != null)
            {
                foreach (var ancestor in Parent.Ancestors)
                {
                    yield return ancestor;
                }

                yield return Parent;
            }
        }

        protected IEnumerable<T> GetDescendants()
        {
            foreach (var child in Children)
            {
                yield return child;

                foreach (var descendant in child.Descendants)
                {
                    yield return descendant;
                }
            }
        }

        protected void ChildAdded(T child)
        {
            child.Parent = (T) this;
        }

        protected void ChildRemoved(T child)
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

        public bool IsAncestorOf(T descendant)
        {
            if (descendant == null)
            {
                throw new ArgumentNullException(nameof(descendant));
            }

            return descendant.Ancestors.Contains(this);
        }

        public bool IsDescendantOf(T ancestor)
        {
            if (ancestor == null)
            {
                throw new ArgumentNullException(nameof(ancestor));
            }

            return ancestor.Descendants.Contains(this);
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
