using System.Collections.Generic;

namespace Querier
{
    public sealed class AsyncIdentityQueryAsyncRequestHandler<TRequest, TSource, TIdentity, TResult> : IAsyncRequestHandler<TRequest>
        where TRequest : IAsyncRequest, IAsyncIdentityQuery<TSource, TIdentity, TResult>
    {
        public async IAsyncEnumerable<IResult> HandleAsync(TRequest request, IApplicationState state)
        {
            var result = await state.QueryAsync(request);
            if (result is null)
            {
                yield return Results.NotFound(request.Id);
            }
            else
            {
                yield return Results.Value(result);
            }
        }
    }
}