namespace Querier
{
    public interface IAsyncIdentityQuery<in TSource, out TIdentity, TResult> : IAsyncQuery<TSource, TResult>
    {
        TIdentity Id { get; }
    }
}