namespace Querier
{
    public interface IQuery<in TSource, out TResult>
    {
        TResult Execute(TSource source, IQueryContext context);
    }
}