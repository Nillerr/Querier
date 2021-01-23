using System.Threading.Tasks;

namespace Querier
{
    public abstract class AbstractAsyncQueryExecutor<TExecutorSource> : IAsyncQueryExecutor
    {
        public async Task<QueryExecutionResult<TResult>> ExecuteAsync<TSource, TResult>(IAsyncQuery<TSource, TResult> query, IQueryContext context)
        {
            if (query is IAsyncQuery<TExecutorSource, TResult> typedQuery)
            {
                TResult result = await ExecuteAsync<TResult>(typedQuery, context);
                return result;
            }

            return default;
        }

        public abstract Task<TResult> ExecuteAsync<TResult>(IAsyncQuery<TExecutorSource, TResult> query, IQueryContext context);
    }
}