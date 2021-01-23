using System.Threading.Tasks;

namespace Querier.AspNetCore.Demo.Framework
{
    public class ForbidActionResultHandler : IForbidResultHandler
    {
        private readonly IActionResultSource _source;

        public ForbidActionResultHandler(IActionResultSource source)
        {
            _source = source;
        }

        public Task ExecuteForbidAsync()
        {
            _source.ActionResult = new global::Microsoft.AspNetCore.Mvc.ForbidResult();
            return Task.CompletedTask;
        }
    }
}