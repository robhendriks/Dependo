namespace Dependo
{
    using System;

    public class DependencyContainerBase<T, TKey> : IDependencyContainer<T, TKey> where T : DependencyBase<T, TKey>
    {
        public IDependencyContainer<T, TKey> RegisterDependency(T dependency, Action<IDependencyBuilder<T, TKey>> action = null)
        {
            return this;
        }
    }
}
