using System.Collections.Generic;

namespace Querier
{
    public interface IAsyncRequestHandler<in TRequest>
        where TRequest : IAsyncRequest
    {
        IAsyncEnumerable<IResult> HandleAsync(TRequest request, IApplicationState state);
    }
}