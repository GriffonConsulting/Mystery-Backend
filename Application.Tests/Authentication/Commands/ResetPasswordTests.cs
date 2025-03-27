using Application.Authentication.Commands.ResetPassword;
using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Microsoft.AspNetCore.Identity;
using Moq;
using System.ComponentModel.DataAnnotations;

namespace Application.Tests.Authentication.Commands
{
    public class ResetPasswordCommandHandlerTests
    {
        private readonly Mock<IAuthentication> _authenticationServiceMock;
        private readonly ResetPasswordCommandHandler _handler;

        public ResetPasswordCommandHandlerTests()
        {
            _authenticationServiceMock = new Mock<IAuthentication>();
            _handler = new ResetPasswordCommandHandler(_authenticationServiceMock.Object);
        }

        [Fact]
        public async Task Handle_ShouldThrowNotFoundException_WhenUserDoesNotExist()
        {
            // Arrange
            var command = new ResetPasswordCommand { Email = "nonexistent@example.com", Token = "token", Password = "password" };
            IdentityUser? user = null;

            _authenticationServiceMock
                .Setup(s => s.FindByEmailAsync(It.IsAny<string>()))
                .ReturnsAsync(user);

            // Act & Assert
            await Assert.ThrowsAsync<NotFoundException>(() => _handler.Handle(command, CancellationToken.None));
        }

        [Fact]
        public async Task Handle_ShouldThrowValidationException_WhenResetFails()
        {
            // Arrange
            var command = new ResetPasswordCommand { Email = "user@example.com", Token = "token", Password = "password" };
            var failedResult = IdentityResult.Failed(new IdentityError { Code = "Error1" }, new IdentityError { Code = "Error2" });
            IdentityUser? user = new IdentityUser { UserName = "user@example.com", Email = "user@example.com" };

            _authenticationServiceMock
                .Setup(s => s.FindByEmailAsync(It.IsAny<string>()))
                .ReturnsAsync(user);
            _authenticationServiceMock.Setup(x => x.ResetPasswordAsync(user, command.Token, command.Password))
                .ReturnsAsync(failedResult);

            // Act & Assert
            var exception = await Assert.ThrowsAsync<ValidationException>(() => _handler.Handle(command, CancellationToken.None));
            Assert.Contains("Error1,Error2", exception.Message);
        }

        [Fact]
        public async Task Handle_ShouldReturnRequestResult_WhenResetSucceeds()
        {
            // Arrange
            var command = new ResetPasswordCommand { Email = "user@example.com", Token = "token", Password = "password" };
            var successResult = IdentityResult.Success;
            IdentityUser? user = new IdentityUser { UserName = "user@example.com", Email = "user@example.com" };

            _authenticationServiceMock
                .Setup(s => s.FindByEmailAsync(It.IsAny<string>()))
                .ReturnsAsync(user);
            _authenticationServiceMock.Setup(x => x.ResetPasswordAsync(user, command.Token, command.Password))
                .ReturnsAsync(successResult);

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.NotNull(result);
        }
    }
}