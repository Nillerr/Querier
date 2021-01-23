using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace Querier.Microsoft.Extensions.Logging
{
    public sealed record Log<TCategoryName>(
        LogLevel Level,
        string Message,
        params object[] Args
    ) : IResult<ILogger<TCategoryName>>
    {
        public Task ExecuteAsync(ILogger<TCategoryName> logger, IResultContext context)
        {
            logger.Log(Level, Message, Args);
            return Task.CompletedTask;
        }
    }
}