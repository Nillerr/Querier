using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Querier.AspNetCore.Demo.Framework;
using Querier.Demo;
using Querier.Demo.Baskets;

namespace Querier.AspNetCore.Demo.Controllers
{
    public class BasketController : Controller
    {
        private readonly IAspNetCoreQuerier _querier;

        public BasketController(IAspNetCoreQuerier querier)
        {
            _querier = querier;
        }

        [HttpPost("add")]
        public Task<IActionResult> Add(AddToBasket request)
        {
            return _querier.InvokeHandlerAsync(request);
        }
    }
}