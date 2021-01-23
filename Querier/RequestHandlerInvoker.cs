namespace Querier
{
    /// <summary>
    /// Wraps an instance of <see cref="IRequestHandler{TRequest}"/> in an implementation of
    /// <see cref="IRequestHandlerInvoker"/>, which will call the wrapped handler if the incoming request matches the
    /// wrapped handler's request type.
    /// </summary>
    /// <typeparam name="THandlerRequest">
    /// The type of request handled by the wrapped <see cref="IRequestHandler{TRequest}"/>.
    /// </typeparam>
    public sealed class RequestHandlerInvoker<THandlerRequest> : IRequestHandlerInvoker
        where THandlerRequest : IRequest
    {
        private readonly IRequestHandler<THandlerRequest> _handler;

        public RequestHandlerInvoker(IRequestHandler<THandlerRequest> handler)
        {
            _handler = handler;
        }

        public HandlerInvocationResult InvokeHandler(IRequest request, IApplicationState state)
        {
            if (request is THandlerRequest typedRequest)
            {
                var result = _handler.Handle(typedRequest, state);
                return new HandlerInvocationResult(result);
            }

            return default;
        }
    }
}