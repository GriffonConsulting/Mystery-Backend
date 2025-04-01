using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Application.Common.Interfaces.Repositories;
using Application.User.Commands.UpdateUser;
using Microsoft.AspNetCore.Identity;
using Moq;

namespace Application.Tests.User.Commands
{
    public class UpdateUserTests
    {
        private readonly Mock<IUserRepository> _userRepositoryMock;
        private readonly Mock<IAuthentication> _authenticationMock;
        private readonly UpdateUserCommandHandler _handler;

        public UpdateUserTests()
        {
            _userRepositoryMock = new Mock<IUserRepository>();
            _authenticationMock = new Mock<IAuthentication>();
            _handler = new UpdateUserCommandHandler(_userRepositoryMock.Object, _authenticationMock.Object);
        }

        [Fact]
        public async Task Handle_UserNotFound_ThrowsNotFoundException()
        {
            // Arrange
            var command = new UpdateUserCommand { UserId = Guid.NewGuid(), NewEmail = "", OldEmail = "", };

            _userRepositoryMock
                .Setup(repo => repo.GetById(It.IsAny<string>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync((Domain.Entities.User)null);

            // Act & Assert
            await Assert.ThrowsAsync<NotFoundException>(() => _handler.Handle(command, CancellationToken.None));
        }

        [Fact]
        public async Task Handle_OldEmailNotFound_ThrowsNotFoundException()
        {
            // Arrange
            var command = new UpdateUserCommand { UserId = Guid.NewGuid(), NewEmail = "", OldEmail = "", };

            _userRepositoryMock.Setup(repo => repo.GetById(command.UserId, It.IsAny<CancellationToken>()))
                               .ReturnsAsync(new Domain.Entities.User());
            _authenticationMock.Setup(auth => auth.FindByEmailAsync(command.OldEmail))
                               .ReturnsAsync((IdentityUser)null);

            // Act & Assert
            await Assert.ThrowsAsync<NotFoundException>(() => _handler.Handle(command, CancellationToken.None));
        }

        [Fact]
        public async Task Handle_UpdatesUserSuccessfully()
        {
            // Arrange
            var user = new Domain.Entities.User();
            var authUser = new IdentityUser();
            var updateUserCommand = new UpdateUserCommand
            {
                UserId = Guid.NewGuid(),
                OldEmail = "old@example.com",
                NewEmail = "new@example.com",
                Password = "newpassword",
                Firstname = "John",
                Lastname = "Doe",
                Address = "123 Main St",
                City = "City",
                PostalCode = "12345",
                Country = "Country",
                MarketingEmail = true
            };

            _userRepositoryMock.Setup(repo => repo.GetById(updateUserCommand.UserId, It.IsAny<CancellationToken>()))
                               .ReturnsAsync(user);
            _authenticationMock.Setup(auth => auth.FindByEmailAsync(updateUserCommand.OldEmail))
                               .ReturnsAsync(authUser);
            _authenticationMock.Setup(auth => auth.GeneratePasswordResetTokenAsync(authUser))
                               .ReturnsAsync("token");
            _authenticationMock.Setup(x => x.ResetPasswordAsync(authUser, "", updateUserCommand.Password))
                .ReturnsAsync(IdentityResult.Success);
            _authenticationMock.Setup(auth => auth.GenerateChangeEmailTokenAsync(authUser, updateUserCommand.NewEmail))
                               .ReturnsAsync("emailToken");
            _authenticationMock.Setup(auth => auth.ChangeEmailAsync(authUser, "emailToken", updateUserCommand.NewEmail))
                               .ReturnsAsync(IdentityResult.Success);
            _userRepositoryMock.Setup(repo => repo.UpdateEntityAsync(user, It.IsAny<CancellationToken>()));

            // Act
            var result = await _handler.Handle(updateUserCommand, CancellationToken.None);

            // Assert
            Assert.NotNull(result);
            _userRepositoryMock.Verify(repo => repo.UpdateEntityAsync(user, It.IsAny<CancellationToken>()), Times.Once);
        }
    }
}