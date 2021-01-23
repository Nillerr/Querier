using System.Collections.Generic;
using Querier.Demo.Baskets;
using Querier.Demo.Products;

namespace Querier.Demo.Database
{
    public sealed class DatabaseContext
    {
        public Dictionary<BasketId, Basket> Baskets { get; } = new();
        public Dictionary<ProductId, Product> Products { get; } = new();
    }
}