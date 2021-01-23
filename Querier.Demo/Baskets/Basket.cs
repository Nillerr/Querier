using System;
using System.Collections.Immutable;
using Querier.Demo.Products;

namespace Querier.Demo.Baskets
{
    public sealed record BasketId(Guid Value);
    public sealed record Basket(BasketId Id, ImmutableList<ProductId> Products);
}