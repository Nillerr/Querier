using System.Threading.Tasks;

namespace Querier
{
    public sealed record ForbidResult : IResult<IForbidResultHandler>
    {
        public Task ExecuteAsync(IForbidResultHandler handler, IResultContext context)
        {
            return handler.ExecuteForbidAsync();
        }
    }
}