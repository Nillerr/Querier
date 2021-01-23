using System.Threading.Tasks;

namespace Querier
{
    public interface IAsyncQueryExecutor
    {
        Task<QueryExecutionResult<TResult>> ExecuteAsync<TSource, TResult>(
            IAsyncQuery<TSource, TResult> query,
            IQueryContext context
        );
    }
}