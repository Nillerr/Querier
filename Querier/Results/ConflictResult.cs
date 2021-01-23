using System.Threading.Tasks;

namespace Querier
{
    public sealed record ConflictResult<TKey>(TKey Key) : IResult<IConflictResultHandler>
    {
        public Task ExecuteAsync(IConflictResultHandler handler, IResultContext context)
        {
            return handler.ExecuteConflictAsync(Key);
        }
    }
}