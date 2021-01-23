namespace Querier
{
    /// <summary>
    /// Attempts to invoke a <see cref="IAsyncRequestHandler{TRequest}"/>.
    /// </summary>
    public interface IRequestHandlerInvoker
    {
        HandlerInvocationResult InvokeHandler(IRequest request, IApplicationState state);
    }
}