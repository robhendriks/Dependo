namespace Dependo
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TKey"></typeparam>
    public interface IDependencyEdge<out TKey>
    {
        TKey From { get; }

        TKey To { get; }
    }
}
