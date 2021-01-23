using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;

namespace Querier.AspNetCore.Demo.Framework
{
    public sealed class AspNetCoreQuerier : IAspNetCoreQuerier
    {
        private readonly IQuerier _querier;
        private readonly IServiceProvider _serviceProvider;

        public AspNetCoreQuerier(IQuerier querier, IServiceProvider serviceProvider)
        {
            _querier = querier;
            _serviceProvider = serviceProvider;
        }

        public async Task<IActionResult> InvokeHandlerAsync(IRequest request)
        {
            var actionResultSource = _serviceProvider.GetRequiredService<IActionResultSource>();
            
            await _querier.InvokeHandlerAsync(request);
            
            var actionResult = actionResultSource.ActionResult;
            return actionResult ?? new NoContentResult();
        }

        public async Task<IActionResult> InvokeHandlerAsync(IAsyncRequest request)
        {
            var actionResultSource = _serviceProvider.GetRequiredService<IActionResultSource>();
            
            await _querier.InvokeHandlerAsync(request);
            
            var actionResult = actionResultSource.ActionResult;
            return actionResult ?? new NoContentResult();
        }
    }
}