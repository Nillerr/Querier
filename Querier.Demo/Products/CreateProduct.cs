using System.Collections.Generic;
using static Querier.Demo.Documents;
using static Querier.Notifications.Notifications;

namespace Querier.Demo.Products
{
    public sealed record CreateProduct(ProductSku Sku) : IRequest;

    public sealed class CreateProductHandler : IRequestHandler<CreateProduct>
    {
        public IEnumerable<IResult> Handle(CreateProduct request, IApplicationState state)
        {
            var guid = state.Query(NextGuid.Instance);
            var now = state.Query(CurrentTime.Instance);

            var id = new ProductId(guid);
            var product = new Product(id, request.Sku, now);

            yield return InsertOne(db => db.Products, id, product);
            yield return Publish(ProductCreated.Topic, new ProductCreated(id));
        }
    }
}