# Querier

What if... Request Handlers didn't actually interact with external systems... What if... The request handler didn't 
care about how a query was fulfilled... What if... The result of a request handler was a series of operations but not 
invocations 🤔...

Well then you might want what Querier is:

> Querier is an experimental approach to implementing extremely loosely coupled request handlers by centralizing access
> to application state, and emitting declarative operations.

## Disclaimer

> None of the code in this repository is meant to be production ready at this stage. This project serves only to 
> play around with the concept, in order to evaluate what value an approach such as this may bring.

## Goals

- Enable writing declarative request handler logic with 0 explicit dependencies.
- Enable swapping out the implementation of a query without any changes to the request handlers.
- Enable writing tests without mocks.

## Show me the code

```c#
public sealed record AddToBasket(BasketId Id, ProductId Product);

public sealed class AddToBasketHandler : IAsyncRequestHandler<AddToBasket>
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

[ApiController]
[Route("basket")]
public sealed class BasketController
{
    private readonly IAspNetCoreQuerier _querier;

    public BasketController(IAspNetCoreQuerier querier)
    {
        _querier = querier;
    }

    [HttpPost("{id}")]
    public Task<ActionResult<Product>> Add(AddToBasket request)
    {
        // The implementation of IAspNetCoreQuerier will translate Querier IResult objects to instances of IActionResult.
        return _querier.InvokeHandlerAsync(request);
    }
}
```

## Questions

### What's the point?

To exlore and evaluate new patterns by playing around with code.

### How is this any different from using interfaces?

The primary difference is that code using this pattern does not even take dependency on an interface signature, but since 
request handlers inherently have a dependency on the signature of the requests and queries they use, that's arguably not a 
benefit.
