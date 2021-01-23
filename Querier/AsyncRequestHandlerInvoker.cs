namespace Querier
{
    /// <summary>
    /// Wraps an instance of <see cref="IAsyncRequestHandler{TRequest}"/> in an implementation of
    /// <see cref="IAsyncRequestHandlerInvoker"/>, which will call the wrapped handler if the incoming request matches the
    /// wrapped handler's request type.
    /// </summary>
    /// <typeparam name="THandlerRequest">
    /// The type of request handled by the wrapped <see cref="IAsyncRequestHandler{TRequest}"/>.
    /// </typeparam>
    public sealed class AsyncRequestHandlerInvoker<THandlerRequest> : IAsyncRequestHandlerInvoker
        where THandlerRequest : IAsyncRequest
    {
        private readonly IAsyncRequestHandler<THandlerRequest> _handler;

        public AsyncRequestHandlerInvoker(IAsyncRequestHandler<THandlerRequest> handler)
        {
            _handler = handler;
        }

        public AsyncHandlerInvocationResult InvokeHandler(IAsyncRequest request, IApplicationState state)
        {
            if (request is THandlerRequest typedRequest)
            {
                var result = _handler.HandleAsync(typedRequest, state);
                return new AsyncHandlerInvocationResult(result);
            }

            return default;
        }
    }
}