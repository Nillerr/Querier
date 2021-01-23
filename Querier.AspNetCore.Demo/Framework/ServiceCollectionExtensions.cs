using Microsoft.Extensions.DependencyInjection;

namespace Querier.AspNetCore.Demo.Framework
{
    public static class ServiceCollectionExtensions
    {
        public static IQuerierBuilder AddActionResults(this IQuerierBuilder builder)
        {
            builder.Services.AddScoped<ActionResultSource>();
            builder.Services.AddScoped<IForbidResultHandler, ForbidActionResultHandler>();
            builder.Services.AddScoped<IConflictResultHandler, ConflictActionResultHandler>();
            builder.Services.AddScoped<INotFoundResultHandler, NotFoundActionResultHandler>();
            builder.Services.AddScoped<IValueResultHandler, ValueActionResultHandler>();
            return builder;
        }
    }
}