using Microsoft.Extensions.Logging;

namespace Querier.Microsoft.Extensions.Logging
{
    public static class RequestHandlerLogExtensions
    {
        public static Log<TRequest> LogDebug<TRequest>(this IAsyncRequestHandler<TRequest> source, string message, params object[] args)
            where TRequest : IAsyncRequest
        {
            return new(LogLevel.Debug, message, args);
        }
        
        public static Log<TRequest> LogDebug<TRequest>(this IRequestHandler<TRequest> source, string message, params object[] args)
            where TRequest : IRequest
        {
            return new(LogLevel.Debug, message, args);
        }
    }
}