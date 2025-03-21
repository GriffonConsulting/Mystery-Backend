//using Application.Common.Exceptions;
//using Application.Common.Interfaces.Repositories;
//using Application.Product.Queries.GetProduct;
//using Moq;

//namespace Application.Tests.Product.Queries
//{
//    public class GetProductTests : UnitTestBase
//    {
//        private readonly GetProductQueryHandler _getProductQueryHandler;


//        public GetProductTests()
//        {
//            Mock<IProductRepository> dbProductQueries = new(DbContextMock);
//            MockDatabase();
//            _getProductQueryHandler = new GetProductQueryHandler(dbProductQueries.Object);
//        }

//        internal void MockDatabase()
//        {
//            DbContextMock.Product.Add(new Domain.Entities.Product { Title = "", Description = "", Duration = "", PriceCode = "", ProductCode = "abc", ProductImage = [], Subtitle = "" });
//            DbContextMock.SaveChanges();
//        }

//        [Fact]
//        public async Task Product_not_found_should_throw_BadRequestException()
//        {
//            Task act() => _getProductQueryHandler.Handle(
//                new GetProductQuery()
//                {
//                    ProductCode = ""
//                }, new CancellationToken());
//            NotFoundException exception = await Assert.ThrowsAsync<NotFoundException>(act);
//            Assert.Equal("product", exception.Message);
//        }

//        [Fact]
//        public async Task Success()
//        {
//            var result = await _getProductQueryHandler.Handle(
//                new GetProductQuery()
//                {
//                    ProductCode = "abc"
//                }, new CancellationToken());
//            Assert.Equal("abc", result.Result.ProductCode);
//        }
//    }
//}
