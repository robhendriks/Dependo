namespace Dependo
{
    using System;

    public interface IDependencyContainer<T, TKey> where T : class
    {
        IDependencyContainer<T, TKey> RegisterDependency(T dependency, Action<IDependencyBuilder<T, TKey>> action = null);
    }
}
