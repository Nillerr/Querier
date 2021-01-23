using System.Threading.Tasks;

namespace Querier.AspNetCore.Demo.Framework
{
    public class ValueActionResultHandler : IValueResultHandler
    {
        private readonly IActionResultSource _source;

        public ValueActionResultHandler(IActionResultSource source)
        {
            _source = source;
        }
        
        public Task ExecuteValueAsync<T>(T value)
        {
            _source.ActionResult = new global::Microsoft.AspNetCore.Mvc.OkObjectResult(value);
            return Task.CompletedTask;
        }
    }
}