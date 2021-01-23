using Querier.Notifications;

namespace Querier.Demo.Products
{
    public sealed record ProductCreated(ProductId Id)
    {
        public static readonly Topic<ProductCreated> Topic = "product-created";
    }
}