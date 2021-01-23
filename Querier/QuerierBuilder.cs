using Microsoft.Extensions.DependencyInjection;

namespace Querier
{
    internal sealed class QuerierBuilder : IQuerierBuilder
    {
        public QuerierBuilder(IServiceCollection services)
        {
            Services = services;
        }

        public IServiceCollection Services { get; }
    }
}