namespace Dependo
{
    using System;
    using System.Collections.Generic;

    public interface IDependency<T, out TKey> : IComparable<T>, IEquatable<T>
        where T : class
        where TKey : IComparable<TKey>, IEquatable<TKey>
    {
        TKey Key { get; }

        T Parent { get; }

        IEnumerable<T> Children { get; }

        IEnumerable<T> Ancestors { get; }

        IEnumerable<T> Descendants { get; }

        bool HasChildren();

        void AddChild(T child);

        bool TryAddChild(T child);

        void RemoveChild(T child);

        bool TryRemoveChild(T child);

        bool HasChild(T child);

        bool IsParentOf(T child);

        bool IsChildOf(T parent);

        bool IsSiblingOf(T sibling);

        bool IsAncestorOf(T descendant);

        bool IsDescendantOf(T ancestor);
    }
}
