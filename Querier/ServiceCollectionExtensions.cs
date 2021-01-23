using System;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Reflection;
using Microsoft.Extensions.DependencyInjection;

namespace Querier
{
    public static class ServiceCollectionExtensions
    {
        public static IQuerierBuilder AddQuerier(this IServiceCollection services, params Assembly[] assemblies)
        {
            services.AddScoped<IQuerier, Querier>();
            services.AddScoped<IApplicationState, ApplicationState>();

            var builder = new QuerierBuilder(services);
            builder.AddRequestHandlers(assemblies);
            builder.AddAsyncIdentityQueries(assemblies);

            builder.AddQueryExecutor<ServiceProviderQueryExecutor>();
            builder.AddAsyncQueryExecutor<ServiceProviderAsyncQueryExecutor>();

            return builder;
        }

        public static IQuerierBuilder AddQueryExecutor<TImplementation>(this IQuerierBuilder builder)
            where TImplementation : IQueryExecutor
        {
            return builder.AddQueryExecutor(typeof(TImplementation));
        }

        public static IQuerierBuilder AddQueryExecutor(this IQuerierBuilder builder, Type implementationType)
        {
            builder.Services.AddScoped(typeof(IQueryExecutor), implementationType);
            return builder;
        }

        public static IQuerierBuilder AddAsyncQueryExecutor<TImplementation>(this IQuerierBuilder builder)
            where TImplementation : IAsyncQueryExecutor
        {
            return builder.AddAsyncQueryExecutor(typeof(TImplementation));
        }

        public static IQuerierBuilder AddAsyncQueryExecutor(this IQuerierBuilder builder, Type implementationType)
        {
            builder.Services.AddScoped(typeof(IAsyncQueryExecutor), implementationType);
            return builder;
        }

        public static IQuerierBuilder AddRequestHandlers(this IQuerierBuilder builder, params Assembly[] assemblies)
        {
            foreach (var assembly in assemblies)
            {
                builder.AddRequestHandlers(assembly);
            }

            return builder;
        }

        public static IQuerierBuilder AddRequestHandlers(this IQuerierBuilder builder, Assembly assembly)
        {
            foreach (var handlerType in assembly.GetExportedTypes())
            {
                if (handlerType.TryGetAsyncRequestHandler(out var asyncRequestType))
                {
                    builder.AddAsyncRequestHandler(asyncRequestType, handlerType);
                }
                
                if (handlerType.TryGetRequestHandler(out var requestType))
                {
                    builder.AddRequestHandler(requestType, handlerType);
                }
            }

            return builder;
        }

        public static IQuerierBuilder AddAsyncRequestHandler<TRequest, TRequestHandler>(this IQuerierBuilder builder)
            where TRequest : IAsyncRequest
            where TRequestHandler : IAsyncRequestHandler<TRequest>
        {
            return builder.AddAsyncRequestHandler(typeof(TRequest), typeof(TRequestHandler));
        }

        public static IQuerierBuilder AddAsyncRequestHandler(this IQuerierBuilder builder, Type requestType, Type handlerType)
        {
            builder.Services.AddScoped(typeof(IAsyncRequestHandler<>).MakeGenericType(requestType), handlerType);
            builder.Services.AddScoped(typeof(IAsyncRequestHandlerInvoker), typeof(AsyncRequestHandlerInvoker<>).MakeGenericType(requestType));
            return builder;
        }

        private static bool TryGetAsyncRequestHandler(this Type handlerType, [MaybeNullWhen(false)] out Type requestType)
        {
            if (!handlerType.IsClass || handlerType.IsAbstract)
            {
                requestType = null;
                return false;
            }

            var interfaces = handlerType.GetInterfaces();
                    
            var requestHandlerInterface = interfaces.FirstOrDefault(e => e.IsConstructedGenericType && e.GetGenericTypeDefinition() == typeof(IAsyncRequestHandler<>));
            if (requestHandlerInterface is null)
            {
                requestType = null;
                return false;
            }

            requestType = requestHandlerInterface.GetGenericArguments()[0];
            return true;
        }

        public static IQuerierBuilder AddRequestHandler<TRequest, TRequestHandler>(this IQuerierBuilder builder)
            where TRequest : IRequest
            where TRequestHandler : IRequestHandler<TRequest>
        {
            return builder.AddRequestHandler(typeof(TRequest), typeof(TRequestHandler));
        }

        public static IQuerierBuilder AddRequestHandler(this IQuerierBuilder builder, Type requestType, Type handlerType)
        {
            builder.Services.AddScoped(typeof(IRequestHandler<>).MakeGenericType(requestType), handlerType);
            builder.Services.AddScoped(typeof(IRequestHandlerInvoker), typeof(RequestHandlerInvoker<>).MakeGenericType(requestType));
            return builder;
        }

        private static bool TryGetRequestHandler(this Type handlerType, [MaybeNullWhen(false)] out Type requestType)
        {
            if (!handlerType.IsClass || handlerType.IsAbstract)
            {
                requestType = null;
                return false;
            }

            var interfaces = handlerType.GetInterfaces();
                    
            var requestHandlerInterface = interfaces.FirstOrDefault(e => e.IsConstructedGenericType && e.GetGenericTypeDefinition() == typeof(IAsyncRequestHandler<>));
            if (requestHandlerInterface is null)
            {
                requestType = null;
                return false;
            }

            requestType = requestHandlerInterface.GetGenericArguments()[0];
            return true;
        }

        public static IQuerierBuilder AddAsyncIdentityQueries(this IQuerierBuilder builder, params Assembly[] assemblies)
        {
            foreach (var assembly in assemblies)
            {
                builder.AddAsyncIdentityQueries(assembly);
            }

            return builder;
        }

        public static IQuerierBuilder AddAsyncIdentityQueries(this IQuerierBuilder builder, Assembly assembly)
        {
            foreach (var requestType in assembly.GetTypes())
            {
                if (requestType.TryGetAsyncIdentityQueryTypeInfo(out var t))
                {
                    var requestHandlerType = typeof(AsyncIdentityQueryAsyncRequestHandler<,,,>)
                        .MakeGenericType(t.RequestType, t.SourceType, t.IdentityType, t.ResultType);

                    builder.AddAsyncRequestHandler(t.RequestType, requestHandlerType);
                }
            }

            return builder;
        }

        private static bool TryGetAsyncIdentityQueryTypeInfo(this Type requestType, [MaybeNullWhen(false)] out AsyncIdentityQueryTypeInfo typeInfo)
        {
            if (!requestType.IsClass || requestType.IsAbstract)
            {
                typeInfo = null;
                return false;
            }

            var interfaces = requestType.GetInterfaces();
                    
            var queryType = interfaces.FirstOrDefault(e => e.IsConstructedGenericType && e.GetGenericTypeDefinition() == typeof(IAsyncIdentityQuery<,,>));
            if (queryType is null)
            {
                typeInfo = null;
                return false;
            }

            var sourceType = queryType.GetGenericArguments()[0];
            var identityType = queryType.GetGenericArguments()[1];
            var resultType = queryType.GetGenericArguments()[2];
            
            typeInfo = new AsyncIdentityQueryTypeInfo(requestType, sourceType, identityType, resultType);
            return true;
        }

        private sealed class AsyncIdentityQueryTypeInfo
        {
            public AsyncIdentityQueryTypeInfo(Type requestType, Type sourceType, Type identityType, Type resultType)
            {
                RequestType = requestType;
                SourceType = sourceType;
                IdentityType = identityType;
                ResultType = resultType;
            }

            public Type RequestType { get; }
            public Type SourceType { get; }
            public Type IdentityType { get; }
            public Type ResultType { get; }
        }
    }
}