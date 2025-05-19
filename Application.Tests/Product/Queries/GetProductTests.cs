using Application.Common.Exceptions;
using Application.Common.Interfaces.Repositories;
using Application.Product.Queries.GetProduct;
using Domain.Entities;
using Moq;

namespace Application.Tests.Product.Queries
{
    public class GetProductQueryHandlerTests
    {
        private readonly Mock<IProductRepository> _productRepositoryMock;
        private readonly GetProductQueryHandler _handler;

        public GetProductQueryHandlerTests()
        {
            _productRepositoryMock = new Mock<IProductRepository>();
            _handler = new GetProductQueryHandler(_productRepositoryMock.Object);
        }

        [Fact]
        public async Task Handle_ProductFound_ReturnsGetProductDto()
        {
            // Arrange
            var product = new Domain.Entities.Product
            {
                Id = Guid.NewGuid(),
                ProductCode = "PROD123",
                Title = "Amazing Product",
                Subtitle = "Best product ever",
                Description = "This product is amazing for all your needs.",
                NbPlayerMin = 2,
                NbPlayerMax = 6,
                Price = 49.99m,
                Duration = "90",
                Difficulty = Domain.Enums.Product.Difficulty.Medium,
                ProductType = Domain.Enums.Product.ProductType.MurderParty,
                PriceCode = "",
                ProductImage = new List<ProductImage>
                {
                    new ProductImage { Link = "https://example.com/image1.jpg" },
                    new ProductImage { Link = "https://example.com/image2.jpg" }
                }
            };

            _productRepositoryMock
                .Setup(repo => repo.GetByProductCodeAsync(It.IsAny<string>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(product);

            var query = new GetProductQuery { ProductCode = "PROD123" };

            // Act
            var result = await _handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(product.Id, result.Result.Id);
            Assert.Equal("PROD123", result.Result.ProductCode);
            Assert.Equal("Amazing Product", result.Result.Title);
            Assert.Equal("Best product ever", result.Result.Subtitle);
            Assert.Equal("This product is amazing for all your needs.", result.Result.Description);
            Assert.Equal(2, result.Result.NbPlayerMin);
            Assert.Equal(6, result.Result.NbPlayerMax);
            Assert.Equal(49.99m, result.Result.Price);
            Assert.Equal("90", result.Result.Duration);
            Assert.Equal(Domain.Enums.Product.Difficulty.Medium, result.Result.Difficulty);
            Assert.Equal(Domain.Enums.Product.ProductType.MurderParty, result.Result.ProductType);
            Assert.Equal(2, result.Result.Images.Count);
            Assert.Contains("https://example.com/image1.jpg", result.Result.Images);
            Assert.Contains("https://example.com/image2.jpg", result.Result.Images);
        }

        [Fact]
        public async Task Handle_ProductNotFound_ThrowsNotFoundException()
        {
            // Arrange
            _productRepositoryMock
                .Setup(repo => repo.GetByProductCodeAsync(It.IsAny<string>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync((Domain.Entities.Product)null);

            var query = new GetProductQuery { ProductCode = "PROD123" };

            // Act & Assert
            await Assert.ThrowsAsync<NotFoundException>(() => _handler.Handle(query, CancellationToken.None));
        }
    }
}
