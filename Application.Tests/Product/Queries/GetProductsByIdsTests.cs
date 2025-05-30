﻿using Application.Common.Exceptions;
using Application.Common.Interfaces.Repositories;
using Application.Product.Queries.GetProductsByIds;
using Domain.Entities;
using Domain.Enums.Product;
using Moq;

namespace Application.Tests.Product.Queries
{
    public class GetProductsByIdsQueryHandlerTests
    {
        private readonly Mock<IProductRepository> _productRepositoryMock;
        private readonly GetProductsByIdsQueryHandler _handler;

        public GetProductsByIdsQueryHandlerTests()
        {
            _productRepositoryMock = new Mock<IProductRepository>();
            _handler = new GetProductsByIdsQueryHandler(_productRepositoryMock.Object);
        }

        [Fact]
        public async Task Handle_ProductsFoundByIds_ReturnsGetProductDtoArray()
        {
            // Arrange
            var products = new[]
            {
                new Domain.Entities.Product
                {
                    Id = Guid.NewGuid(),
                    ProductCode = "PROD001",
                    Title = "Product 1",
                    Subtitle = "Subtitle 1",
                    Description = "Description 1",
                    NbPlayerMin = 2,
                    NbPlayerMax = 4,
                    Price = 29.99m,
                    Duration = "90",
                    Difficulty = Difficulty.Medium,
                    ProductType = ProductType.MurderParty,
                    PriceCode = "",
                    ProductImage =
                    [
                        new() { Link = "https://example.com/image1.jpg" }
                    ]
                },
                new Domain.Entities.Product
                {
                    Id = Guid.NewGuid(),
                    ProductCode = "PROD002",
                    Title = "Product 2",
                    Subtitle = "Subtitle 2",
                    Description = "Description 2",
                    NbPlayerMin = 1,
                    NbPlayerMax = 2,
                    Price = 49.99m,
                    Duration = "90",
                    Difficulty = Difficulty.Medium,
                    ProductType = ProductType.MurderParty,
                    PriceCode = "",
                    ProductImage =
                    [
                        new ProductImage { Link = "https://example.com/image2.jpg" }
                    ]
                }
            };

            _productRepositoryMock
                .Setup(repo => repo.GetByProductsIdsAsync(It.IsAny<Guid[]>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(products);

            var query = new GetProductsByIdsQuery { ProductsIds = new Guid[] { products[0].Id, products[1].Id } };

            // Act
            var result = await _handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(2, result.Result.Length);
            Assert.Equal("PROD001", result.Result[0].ProductCode);
            Assert.Equal("Product 1", result.Result[0].Title);
            Assert.Equal("Subtitle 1", result.Result[0].Subtitle);
            Assert.Equal("Description 1", result.Result[0].Description);
            Assert.Equal(2, result.Result[0].NbPlayerMin);
            Assert.Equal(4, result.Result[0].NbPlayerMax);
            Assert.Equal(29.99m, result.Result[0].Price);
            Assert.Equal("90", result.Result[0].Duration);
            Assert.Equal(Difficulty.Medium, result.Result[0].Difficulty);
            Assert.Equal(ProductType.MurderParty, result.Result[0].ProductType);
            Assert.Contains("https://example.com/image1.jpg", result.Result[0].Images);
        }

        [Fact]
        public async Task Handle_NoProductsFound_ThrowsNotFoundException()
        {
            // Arrange
            _productRepositoryMock
                .Setup(repo => repo.GetByProductsIdsAsync(It.IsAny<Guid[]>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(Array.Empty<Domain.Entities.Product>());

            var query = new GetProductsByIdsQuery { ProductsIds = [Guid.NewGuid()] };

            // Act & Assert
            await Assert.ThrowsAsync<NotFoundException>(() => _handler.Handle(query, CancellationToken.None));
        }
    }
}
