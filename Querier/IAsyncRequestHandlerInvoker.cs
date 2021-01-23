namespace Querier
{
    /// <summary>
    /// Attempts to invoke a <see cref="IAsyncRequestHandler{TRequest}"/>.
    /// </summary>
    public interface IAsyncRequestHandlerInvoker
    {
        AsyncHandlerInvocationResult InvokeHandler(IAsyncRequest request, IApplicationState state);
    }
}