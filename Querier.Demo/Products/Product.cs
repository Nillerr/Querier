using System;

namespace Querier.Demo.Products
{
    public sealed record ProductId(Guid Value);

    public sealed record ProductSku(string Value);
    
    public sealed record Product(ProductId Id, ProductSku Sku, DateTimeOffset CreatedAt);
}