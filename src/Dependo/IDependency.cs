namespace Dependo
{
    using System;
    using System.Collections.Generic;

    public interface IDependency<T, out TKey> : IComparable<T>, IEquatable<T>
        where T : class
    {
        TKey Key { get; }

        T Parent { get; }

        IEnumerable<T> Children { get; }

        void AddChild(T child);

        bool TryAddChild(T child);

        void RemoveChild(T child);

        bool TryRemoveChild(T child);

        bool HasChild(T child);

        bool IsParentOf(T child);

        bool IsChildOf(T parent);

        bool IsSiblingOf(T sibling);
    }
}
