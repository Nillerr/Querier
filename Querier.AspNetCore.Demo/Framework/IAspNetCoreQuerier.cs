using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Querier.AspNetCore.Demo.Framework
{
    public interface IAspNetCoreQuerier
    {
        Task<IActionResult> InvokeHandlerAsync(IRequest request);
        Task<IActionResult> InvokeHandlerAsync(IAsyncRequest request);
    }
}