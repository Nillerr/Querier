using System;
using System.Threading.Tasks;

namespace Querier
{
    public sealed class ServiceProviderAsyncQueryExecutor : IAsyncQueryExecutor
    {
        private readonly IServiceProvider _serviceProvider;

        public ServiceProviderAsyncQueryExecutor(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public async Task<QueryExecutionResult<TResult>> ExecuteAsync<TSource, TResult>(
            IAsyncQuery<TSource, TResult> query,
            IQueryContext context
        )
        {
            var source = _serviceProvider.GetService(typeof(TSource));
            if (source is not null)
            {
                return await query.ExecuteAsync((TSource) source, context);
            }

            return default;
        }
    }
}