using Application.Common.Exceptions;
using Application.Product.Queries.GetProducts;
using Database.Queries;
using Domain.Enums.Product;
using Moq;

namespace Application.Tests.Product.Queries
{
    public class GetProductsTests : UnitTestBase
    {
        private readonly GetProductsQueryHandler _getProductsQueryHandler;


        public GetProductsTests()
        {
            Mock<DbProductQueries> dbProductQueries = new(DbContextMock);
            MockDatabase();
            _getProductsQueryHandler = new GetProductsQueryHandler(dbProductQueries.Object);
        }

        internal void MockDatabase()
        {
            DbContextMock.Product.Add(new Domain.Entities.Product { Title = "", Description = "", Duration = "", PriceCode = "", ProductCode = "abc", ProductImage = [], Subtitle = "", ProductType = ProductType.MurderParty });
            DbContextMock.SaveChanges();
        }

        [Fact]
        public async Task Product_not_found_should_throw_BadRequestException()
        {
            Task act() => _getProductsQueryHandler.Handle(
                new GetProductsQuery()
                {
                    ProductType = (ProductType)999
                }, new CancellationToken());
            NotFoundException exception = await Assert.ThrowsAsync<NotFoundException>(act);
            Assert.Equal("products", exception.Message);
        }

        [Fact]
        public async Task Success()
        {
            var result = await _getProductsQueryHandler.Handle(
                new GetProductsQuery()
                {
                    ProductType = ProductType.MurderParty
                }, new CancellationToken());
            Assert.Equal("abc", result.Result.FirstOrDefault()?.ProductCode);
        }
    }
}
