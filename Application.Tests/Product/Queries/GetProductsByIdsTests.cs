//using Application.Common.Exceptions;
//using Application.Common.Interfaces.Repositories;
//using Application.Product.Queries.GetProductsByIds;
//using Domain.Enums.Product;
//using Moq;

//namespace Application.Tests.Product.Queries
//{
//    public class GetProductsByIdsTests : UnitTestBase
//    {
//        private readonly GetProductsByIdsQueryHandler _getProductsByIdsQueryHandler;


//        public GetProductsByIdsTests()
//        {
//            Mock<IProductRepository> dbProductQueries = new(DbContextMock);
//            MockDatabase();
//            _getProductsByIdsQueryHandler = new GetProductsByIdsQueryHandler(dbProductQueries.Object);
//        }

//        internal void MockDatabase()
//        {
//            DbContextMock.Product.Add(new Domain.Entities.Product { Id = Guid.NewGuid(), Title = "", Description = "", Duration = "", PriceCode = "", ProductCode = "abc", ProductImage = [], Subtitle = "", ProductType = ProductType.MurderParty });
//            DbContextMock.SaveChanges();
//        }

//        [Fact]
//        public async Task Product_not_found_should_throw_BadRequestException()
//        {
//            Task act() => _getProductsByIdsQueryHandler.Handle(
//                new GetProductsByIdsQuery()
//                {
//                    ProductsIds = [Guid.Empty]
//                }, new CancellationToken());
//            NotFoundException exception = await Assert.ThrowsAsync<NotFoundException>(act);
//            Assert.Equal("products", exception.Message);
//        }

//        [Fact]
//        public async Task Success()
//        {
//            var result = await _getProductsByIdsQueryHandler.Handle(
//                new GetProductsByIdsQuery()
//                {
//                    ProductsIds = [DbContextMock.Product.FirstOrDefault().Id]

//                }, new CancellationToken());
//            Assert.Equal("abc", result.Result.FirstOrDefault()?.ProductCode);
//        }
//    }
//}
