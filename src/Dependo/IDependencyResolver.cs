namespace Dependo
{
    public interface IDependencyResolver<T, TKey> where T : class
    {
        void ResolveDependencies(IDependencyContainer<T, TKey> container);
    }
}
