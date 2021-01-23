using System;
using System.Threading.Tasks;

namespace Querier
{
    public sealed class ApplicationState : IApplicationState
    {
        private readonly IQueryExecutor[] _executors;
        private readonly IAsyncQueryExecutor[] _asyncExecutors;
        private readonly IServiceProvider _serviceProvider;

        public ApplicationState(
            IQueryExecutor[] executors,
            IAsyncQueryExecutor[] asyncExecutors,
            IServiceProvider serviceProvider
        )
        {
            _asyncExecutors = asyncExecutors;
            _executors = executors;
            _serviceProvider = serviceProvider;
        }

        public TResult Query<TSource, TResult>(IQuery<TSource, TResult> query)
        {
            var context = new QueryContext(_serviceProvider);
            
            foreach (var executor in _executors)
            {
                var queryExecutionResult = executor.Execute(query, context);
                if (queryExecutionResult.Type == QueryExecutionResultType.Success)
                {
                    return queryExecutionResult.Result;
                }
            }

            throw new NoMatchingQueryExecutorException();
        }

        public async Task<T> QueryAsync<TSource, T>(IAsyncQuery<TSource, T> query)
        {
            var context = new QueryContext(_serviceProvider);
            
            foreach (var executor in _asyncExecutors)
            {
                var queryExecutionResult = await executor.ExecuteAsync(query, context);
                if (queryExecutionResult.Type == QueryExecutionResultType.Success)
                {
                    return queryExecutionResult.Result;
                }
            }

            throw new NoMatchingQueryExecutorException();
        }
    }
}