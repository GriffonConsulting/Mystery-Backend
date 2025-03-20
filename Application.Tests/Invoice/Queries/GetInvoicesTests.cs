using Application.Invoices.Queries.GetInvoices;
using Database.Queries;
using Moq;

namespace Application.Tests.Invoice.Queries
{
    public class GetInvoicesTests : UnitTestBase
    {
        private readonly GetInvoicesQueryHandler _getInvoicesQueryHandler;


        public GetInvoicesTests()
        {
            Mock<DbOrderQueries> dbOrderQueries = new(DbContextMock);
            MockDatabase();
            _getInvoicesQueryHandler = new GetInvoicesQueryHandler(dbOrderQueries.Object);
        }

        internal void MockDatabase()
        {
            DbContextMock.Order.Add(new Domain.Entities.Order { Id = Guid.NewGuid(), StripeId = "", Amount = 0, ReceiptUrl = "abc", UserId = Guid.NewGuid() });
            DbContextMock.SaveChanges();
        }

        [Fact]
        public async Task Success()
        {
            var result = await _getInvoicesQueryHandler.Handle(
                new GetInvoicesQuery()
                {
                    UserId = DbContextMock.Order.FirstOrDefault().UserId

                }, new CancellationToken());
            Assert.Equal("abc", result.Result.FirstOrDefault()?.ReceiptUrl);
        }
    }
}
