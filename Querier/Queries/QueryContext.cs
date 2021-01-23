using System;

namespace Querier
{
    public sealed class QueryContext : IQueryContext
    {
        public QueryContext(IServiceProvider serviceProvider)
        {
            ServiceProvider = serviceProvider;
        }

        public IServiceProvider ServiceProvider { get; }
    }
}