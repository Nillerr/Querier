using System.Threading.Tasks;
using Querier.Demo.Database;

namespace Querier.Demo.Baskets
{
    public sealed record BasketById(BasketId Id) : IAsyncIdentityQuery<DatabaseContext, BasketId, Basket?>
    {
        public Task<Basket?> ExecuteAsync(DatabaseContext source, IQueryContext context)
        {
            return Task.FromResult(source.Baskets.TryGetValue(Id, out var basket) ? basket : null);
        }
    }
}