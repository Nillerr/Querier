using System;

namespace Querier
{
    public sealed class ServiceProviderQueryExecutor : IQueryExecutor
    {
        private readonly IServiceProvider _serviceProvider;

        public ServiceProviderQueryExecutor(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public QueryExecutionResult<TResult> Execute<TSource, TResult>(
            IQuery<TSource, TResult> query,
            IQueryContext context
        )
        {
            var source = _serviceProvider.GetService(typeof(TSource));
            if (source is not null)
            {
                return query.Execute((TSource) source, context);
            }

            return default;
        }
    }
}