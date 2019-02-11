namespace Dependo
{
    public interface IDependencyEdge<out TKey>
    {
        TKey From { get; }

        TKey To { get; }
    }
}
