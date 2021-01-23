using System;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;

namespace Querier
{
    /// <summary>
    /// Represents a result of the invocation of an <see cref="IAsyncRequestHandler{TRequest}"/>.
    /// </summary>
    public interface IResult
    {
        Task ExecuteAsync(IResultContext context);
    }
    
    /// <summary>
    /// Represents a result of the invocation of an <see cref="IAsyncRequestHandler{TRequest}"/> targeted at the specified
    /// <see cref="THandler"/>, which will be resolved from the <see cref="IServiceProvider"/> in the
    /// <see cref="IResultContext"/>.
    /// </summary>
    public interface IResult<in THandler> : IResult
        where THandler : notnull
    {
        Task IResult.ExecuteAsync(IResultContext context)
        {
            var handler = context.ServiceProvider.GetRequiredService<THandler>();
            return ExecuteAsync(handler, context);
        }

        Task ExecuteAsync(THandler target, IResultContext context);
    }
}