# Querier

What if... Request Handlers didn't actually interact with external systems... What if... The request handler didn't 
care about how a query was fulfilled... What if... The result of a request handler was a series of operations but not 
invocations. 

Well you'd have Querier.

```c#
public sealed record AddToBasket(BasketId Id, ProductId Product);

public sealed class AddToBasketHandler : IRequestHandler<AddToBasket>
{
    public async IAsyncEnumerable<IResult> HandleAsync(AddToBasket request, IApplicationState state)
    {
        var now = state.Query(CurrentTime.Instance);
        var id = state.Query(NewGuid.Instance);
        
        var basket = await state.Query(new BasketById(request.Id));
        if (basket is null)
        {
            yield return NotFound(request.Id);
        }
        else
        {
            yield return UpdateOne(db => db.Baskets, request.Id, e => e with
            {
                Products = e.Products.Add(request.Product)
            });
            
            yield return Publish(ProductAdded.Topic, new ProductAdded(request.Id, request.Product));
        }
    }
}
```