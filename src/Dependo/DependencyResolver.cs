namespace Dependo
{
    using System;

    public class DependencyResolver<T, TKey> : IDependencyResolver<T, TKey> where T : class
    {
        public void ResolveDependencies(IDependencyContainer<T, TKey> container)
        {
            if (container == null)
            {
                throw new ArgumentNullException(nameof(container));
            }

            // TODO
        }
    }
}
