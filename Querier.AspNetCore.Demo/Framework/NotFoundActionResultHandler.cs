using System.Threading.Tasks;

namespace Querier.AspNetCore.Demo.Framework
{
    public class NotFoundActionResultHandler : INotFoundResultHandler
    {
        private readonly IActionResultSource _source;

        public NotFoundActionResultHandler(IActionResultSource source)
        {
            _source = source;
        }

        public Task ExecuteNotFoundAsync<TKey>(TKey key)
        {
            _source.ActionResult = new global::Microsoft.AspNetCore.Mvc.NotFoundObjectResult(key);
            return Task.CompletedTask;
        }
    }
}