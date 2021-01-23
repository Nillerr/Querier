using System.Collections.Generic;
using Querier.Demo.Products;
using Querier.Microsoft.Extensions.Logging;
using static Querier.Demo.Documents;
using static Querier.Notifications.Notifications;
using static Querier.Results;

namespace Querier.Demo.Baskets
{
    public sealed record AddToBasket(BasketId Id, ProductId Product) : IAsyncRequest;

    public sealed class AddToBasketHandler : IAsyncRequestHandler<AddToBasket>
    {
        public async IAsyncEnumerable<IResult> HandleAsync(AddToBasket request, IApplicationState state)
        {
            var id = state.Query(NextGuid.Instance);
            var now = state.Query(CurrentTime.Instance);

            yield return this.LogDebug("Starting request {RequestId} at time {Timestamp}", id, now);
            
            var basket = await state.QueryAsync(new BasketById(request.Id));
            if (basket is null)
            {
                yield return NotFound(request.Id);
                yield break;
            }

            yield return UpdateOne(db => db.Baskets, request.Id, e => e with
            {
                Products = e.Products.Add(request.Product)
            });
            
            yield return Publish(ProductAddedToBasket.Topic, new ProductAddedToBasket(request.Id, request.Product));
        }
    }
}