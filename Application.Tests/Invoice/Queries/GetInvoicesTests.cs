using Application.Common.Interfaces.Repositories;
using Application.Invoices.Queries.GetInvoices;
using Domain.Entities;
using Moq;

namespace Application.Tests.Invoice.Queries
{
    public class GetInvoicesQueryHandlerTests
    {
        private readonly Mock<IOrderRepository> _orderRepositoryMock;
        private readonly GetInvoicesQueryHandler _handler;

        public GetInvoicesQueryHandlerTests()
        {
            _orderRepositoryMock = new Mock<IOrderRepository>();
            _handler = new GetInvoicesQueryHandler(_orderRepositoryMock.Object);
        }

        [Fact]
        public async Task Handle_InvoicesFound_ReturnsInvoicesDtoArray()
        {
            // Arrange
            var invoices = new[]
            {
                new Order
                {
                    Amount = 100.50m,
                    CreatedOn = new DateTime(2025, 03, 21),
                    ReceiptUrl = "https://example.com/receipt1",
                    StripeId = "",
                    UserId = Guid.NewGuid(),
                },
                new Order
                {
                    Amount = 250.00m,
                    CreatedOn = new DateTime(2025, 03, 20),
                    ReceiptUrl = "https://example.com/receipt2",
                    StripeId = "",
                    UserId = Guid.NewGuid(),
                }
            };

            _orderRepositoryMock
                .Setup(repo => repo.GetByUserIdAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(invoices);

            var query = new GetInvoicesQuery { UserId = Guid.NewGuid() };

            // Act
            var result = await _handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(2, result.Result.Length);
            Assert.Equal(100.50m, result.Result[0].Amount);
            Assert.Equal(new DateTime(2025, 03, 21), result.Result[0].CreatedOn);
            Assert.Equal("https://example.com/receipt1", result.Result[0].ReceiptUrl);
            Assert.Equal(250.00m, result.Result[1].Amount);
            Assert.Equal(new DateTime(2025, 03, 20), result.Result[1].CreatedOn);
            Assert.Equal("https://example.com/receipt2", result.Result[1].ReceiptUrl);
        }

        [Fact]
        public async Task Handle_NoInvoicesFound_ReturnsEmptyArray()
        {
            // Arrange
            _orderRepositoryMock
                .Setup(repo => repo.GetByUserIdAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(Array.Empty<Order>());

            var query = new GetInvoicesQuery { UserId = Guid.NewGuid() };

            // Act
            var result = await _handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.NotNull(result);
            Assert.Empty(result.Result);
        }
    }
}
