using Application.Common.Exceptions;
using Application.Common.Interfaces.Repositories;
using Application.Product.Queries.GetProduct;
using Moq;

namespace Application.Tests.User.Queries
{
    public class GetUserQueryHandlerTests
    {
        private readonly Mock<IUserRepository> _userRepositoryMock;
        private readonly GetUserQueryHandler _handler;

        public GetUserQueryHandlerTests()
        {
            _userRepositoryMock = new Mock<IUserRepository>();
            _handler = new GetUserQueryHandler(_userRepositoryMock.Object);
        }

        [Fact]
        public async Task Handle_UserFound_ReturnsGetUserDto()
        {
            // Arrange
            var user = new Domain.Entities.User
            {
                Firstname = "John",
                Lastname = "Doe",
                Address = "123 Main St",
                ComplementaryAddress = "Apt 4B",
                PostalCode = "75001",
                City = "Paris",
                Country = "France",
                MarketingEmail = true
            };

            _userRepositoryMock
                .Setup(repo => repo.GetById(It.IsAny<Guid>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(user);

            var query = new GetUserQuery { ClientId = Guid.NewGuid() };

            // Act
            var result = await _handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.NotNull(result);
            Assert.Equal("John", result.Result.Firstname);
            Assert.Equal("Doe", result.Result.Lastname);
            Assert.Equal("123 Main St", result.Result.Address);
            Assert.Equal("Apt 4B", result.Result.ComplementaryAddress);
            Assert.Equal("75001", result.Result.PostalCode);
            Assert.Equal("Paris", result.Result.City);
            Assert.Equal("France", result.Result.Country);
            Assert.True(result.Result.MarketingEmail);
        }

        [Fact]
        public async Task Handle_UserNotFound_ThrowsNotFoundException()
        {
            // Arrange
            _userRepositoryMock
                .Setup(repo => repo.GetById(It.IsAny<string>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync((Domain.Entities.User)null);

            var query = new GetUserQuery { ClientId = Guid.NewGuid() };

            // Act & Assert
            await Assert.ThrowsAsync<NotFoundException>(() => _handler.Handle(query, CancellationToken.None));
        }
    }
}
