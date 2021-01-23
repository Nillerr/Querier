using Microsoft.Extensions.DependencyInjection;

namespace Querier
{
    public interface IQuerierBuilder
    {
        IServiceCollection Services { get; }
    }
}