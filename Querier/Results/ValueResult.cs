using System.Threading.Tasks;

namespace Querier
{
    public sealed record ValueResult<T>(T Value) : IResult<IValueResultHandler>
    {
        public Task ExecuteAsync(IValueResultHandler handler, IResultContext context)
        {
            return handler.ExecuteValueAsync(Value);
        }
    }
}