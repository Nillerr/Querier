using System.Threading.Tasks;

namespace Querier.AspNetCore.Demo.Framework
{
    public class ConflictActionResultHandler : IConflictResultHandler
    {
        private readonly IActionResultSource _source;

        public ConflictActionResultHandler(IActionResultSource source)
        {
            _source = source;
        }

        public Task ExecuteConflictAsync<TKey>(TKey key)
        {
            _source.ActionResult = new global::Microsoft.AspNetCore.Mvc.ConflictObjectResult(key);
            return Task.CompletedTask;
        }
    }
}