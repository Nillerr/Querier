using System.Threading.Tasks;

namespace Querier
{
    public sealed record NotFoundResult<TKey>(TKey Key) : IResult<INotFoundResultHandler>
    {
        public Task ExecuteAsync(INotFoundResultHandler handler, IResultContext context)
        {
            return handler.ExecuteNotFoundAsync(Key);
        }
    }
}