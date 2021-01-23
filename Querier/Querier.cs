using System;
using System.Threading.Tasks;

namespace Querier
{
    public class Querier : IQuerier
    {
        private readonly IAsyncRequestHandlerInvoker[] _asyncRequestHandlers;
        private readonly IRequestHandlerInvoker[] _requestHandlers;
        private readonly IApplicationState _applicationState;
        private readonly IServiceProvider _serviceProvider;

        protected Querier(
            IAsyncRequestHandlerInvoker[] asyncRequestHandlers,
            IRequestHandlerInvoker[] requestHandlers,
            IApplicationState applicationState,
            IServiceProvider serviceProvider
        )
        {
            _asyncRequestHandlers = asyncRequestHandlers;
            _requestHandlers = requestHandlers;
            _applicationState = applicationState;
            _serviceProvider = serviceProvider;
        }

        public async Task InvokeHandlerAsync(IRequest request)
        {
            foreach (var requestHandler in _requestHandlers)
            {
                var invocationResult = requestHandler.InvokeHandler(request, _applicationState);
                if (invocationResult.Type == HandlerInvocationResultType.Success)
                {
                    var context = new ResultContext(_serviceProvider);
                    
                    foreach (var result in invocationResult.Result)
                    {
                        await result.ExecuteAsync(context);
                    }

                    // Return to signal successful execution of handler and results
                    return;
                }
            }

            throw new InvalidOperationException("A handler for the given request was not found.");
        }

        public async Task InvokeHandlerAsync(IAsyncRequest request)
        {
            foreach (var requestHandler in _asyncRequestHandlers)
            {
                var invocationResult = requestHandler.InvokeHandler(request, _applicationState);
                if (invocationResult.Type == HandlerInvocationResultType.Success)
                {
                    var context = new ResultContext(_serviceProvider);
                    
                    await foreach (var result in invocationResult.Result)
                    {
                        await result.ExecuteAsync(context);
                    }

                    // Return to signal successful execution of handler and results
                    return;
                }
            }

            throw new InvalidOperationException("A handler for the given request was not found.");
        }
    }
}