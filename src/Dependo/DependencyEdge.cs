namespace Dependo
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TKey"></typeparam>
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
