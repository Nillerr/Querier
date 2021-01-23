using System.Collections.Generic;
using System.Threading.Tasks;
using Querier.Demo.Database;

namespace Querier.Demo.Products
{
    public sealed record ProductById(ProductId Id) : IAsyncIdentityQuery<DatabaseContext, ProductId, Product?>
    {
        public Task<Product?> ExecuteAsync(DatabaseContext source, IQueryContext context)
        {
            return Task.FromResult(source.Products.GetValueOrDefault(Id));
        }
    }
}