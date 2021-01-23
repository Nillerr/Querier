using System;
using System.Collections.Immutable;
using System.Threading.Tasks;
using Querier.Demo.Baskets;
using Querier.Demo.Products;
using Querier.Microsoft.Extensions.Logging;
using Xunit;
using static Querier.Testing.Demo.ResultExtensions;

namespace Querier.Testing.Demo
{
    public class AddToBasketTests : IDisposable
    {
        private readonly TestApplicationState _state = new();
        private readonly AddToBasketHandler _handler = new();
        
        private readonly AddToBasket _request;

        public AddToBasketTests()
        {
            var id = new BasketId(Guid.Parse("d34b0ea1-35c2-4801-88d2-6c89396ede70"));
            var productId = new ProductId(Guid.Parse("4b666b4b-21bb-4e17-ab37-c5d0bfbad8df"));
            
            _state.Expect(CurrentTime.Instance, DateTimeOffset.UtcNow);
            _state.Expect(NextGuid.Instance, Guid.Parse("f9fb01be-426d-43e0-b3d6-beb876717750"));

            _request = new AddToBasket(id, productId);
        }

        public void Dispose()
        {
            _state.Dispose();
        }

        [Fact]
        public async Task WhenBasketByIdReturnsNull_ShouldReturnNotFound()
        {
            // Arrange
            _state.Expect(new BasketById(_request.Id), null);

            // Act
            var results = await _handler.HandleAsync(_request, _state).IgnoreLogging();
            
            // Assert
            Assert.Collection(
                results,
                NotFound(_request.Id)
            );
        }

        [Fact]
        public async Task WhenBasketByIdReturnsBasket_ShouldReturnResults()
        {
            // Arrange
            var basket = new Basket(_request.Id, ImmutableList<ProductId>.Empty);
            
            _state.Expect(new BasketById(_request.Id), basket);
            
            // Act
            var results = await _handler.HandleAsync(_request, _state).IgnoreLogging();
            
            // Assert
            Assert.Collection(
                results,
                UpdateOne<Basket, BasketId>(_request.Id),
                Publish(ProductAddedToBasket.Topic, new ProductAddedToBasket(_request.Id, _request.Product))
            );
        }
    }
}