using Querier.Demo.Products;
using Querier.Notifications;

namespace Querier.Demo.Baskets
{
    public sealed record ProductAddedToBasket(BasketId Id, ProductId Product)
    {
        public static readonly Topic<ProductAddedToBasket> Topic = "product-added-to-basket";
    }
}