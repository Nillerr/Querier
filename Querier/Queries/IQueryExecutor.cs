namespace Querier
{
    public interface IQueryExecutor
    {
        QueryExecutionResult<TResult> Execute<TSource, TResult>(
            IQuery<TSource, TResult> query,
            IQueryContext context
        );
    }
}