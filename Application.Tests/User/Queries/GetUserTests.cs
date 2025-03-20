using Application.Common.Exceptions;
using Application.Product.Queries.GetProduct;
using Database.Queries;
using Moq;

namespace Application.Tests.User.Queries
{
    public class GetUserTests : UnitTestBase
    {
        private readonly GetUserQueryHandler _getUserQueryHandler;


        public GetUserTests()
        {
            Mock<DbUserQueries> dbUserQueries = new(DbContextMock);
            MockDatabase();
            _getUserQueryHandler = new GetUserQueryHandler(dbUserQueries.Object);
        }

        internal void MockDatabase()
        {
            DbContextMock.User.Add(new Domain.Entities.User { Id = Guid.NewGuid(), Firstname = "toto" });
            DbContextMock.SaveChanges();
        }

        [Fact]
        public async Task Product_not_found_should_throw_BadRequestException()
        {
            Task act() => _getUserQueryHandler.Handle(
                new GetUserQuery()
                {
                    ClientId = Guid.Empty
                }, new CancellationToken());
            NotFoundException exception = await Assert.ThrowsAsync<NotFoundException>(act);
            Assert.Equal("product", exception.Message);
        }

        [Fact]
        public async Task Success()
        {
            var result = await _getUserQueryHandler.Handle(
                new GetUserQuery()
                {
                    ClientId = DbContextMock.User.FirstOrDefault().Id
                }, new CancellationToken());
            Assert.Equal("toto", result.Result.Firstname);
        }
    }
}
