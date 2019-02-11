namespace Dependo
{
    public class DependencyEdge<TKey> : IDependencyEdge<TKey>
    {
        public TKey From { get; }
        public TKey To { get; }

        public DependencyEdge(TKey from, TKey to)
        {
            From = from;
            To = to;
        }
    }
}
