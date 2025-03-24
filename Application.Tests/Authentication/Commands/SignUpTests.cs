﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Threading;
using System.Threading.Tasks;
using Xunit;
using Moq;
using Microsoft.AspNetCore.Identity;
using Application.Common.Interfaces.Repositories;
using Application.Common.Requests;
using Application.Authentication.Commands.SignUp;
using Domain.Authorization;
using Application.Common.Interfaces;

namespace Application.Tests.Authentication.Commands.SignUp
{
    public class SignUpCommandHandlerTests
    {
        private readonly Mock<IAuthentication> _authenticationServiceMock;
        private readonly Mock<IUserRepository> _userRepositoryMock;
        private readonly Mock<IEmailSender> _emailSenderServiceMock;
        private readonly SignUpCommandHandler _handler;

        public SignUpCommandHandlerTests()
        {
            _authenticationServiceMock = new Mock<IAuthentication>();
            _userRepositoryMock = new Mock<IUserRepository>();
            _emailSenderServiceMock = new Mock<IEmailSender>();
            _handler = new SignUpCommandHandler(_authenticationServiceMock.Object, _userRepositoryMock.Object, _emailSenderServiceMock.Object);
        }

        [Fact]
        public async Task Handle_SignUpSuccessful_ReturnsRequestResult()
        {
            // Arrange
            var user = new IdentityUser { UserName = "user@example.com", Email = "user@example.com" };
            var result = IdentityResult.Success;
            var userId = Guid.NewGuid().ToString();
            var token = "confirmation_token";

            _authenticationServiceMock
                .Setup(s => s.CreateAsync(It.IsAny<IdentityUser>(), It.IsAny<string>()))
                .ReturnsAsync(result);

            _authenticationServiceMock
                .Setup(s => s.GetUserIdAsync(It.IsAny<IdentityUser>()))
                .ReturnsAsync(userId);

            _authenticationServiceMock
                .Setup(s => s.GenerateEmailConfirmationTokenAsync(It.IsAny<IdentityUser>()))
                .ReturnsAsync(token);

            // Act
            var response = await _handler.Handle(new SignUpCommand { Email = "user@example.com", Password = "Password123!", MarketingEmail = true }, CancellationToken.None);

            // Assert
            Assert.NotNull(response);
            _authenticationServiceMock.Verify(s => s.CreateAsync(It.IsAny<IdentityUser>(), It.IsAny<string>()), Times.Once);
            _authenticationServiceMock.Verify(s => s.AddToRoleAsync(It.IsAny<IdentityUser>(), nameof(UserRoles.User)), Times.Once);
            _emailSenderServiceMock.Verify(e => e.SendEmail(It.IsAny<MailAddress>(), "Création de compte", token), Times.Once);
            _authenticationServiceMock.Verify(s => s.ConfirmEmailAsync(It.IsAny<IdentityUser>(), token), Times.Once);
        }

        [Fact]
        public async Task Handle_CreateUserFails_ThrowsHttpRequestException()
        {
            // Arrange
            var user = new IdentityUser { UserName = "user@example.com", Email = "user@example.com" };
            var result = IdentityResult.Failed(new IdentityError { Code = "DuplicateUserName", Description = "Username already exists." });

            _authenticationServiceMock
                .Setup(s => s.CreateAsync(It.IsAny<IdentityUser>(), It.IsAny<string>()))
                .ReturnsAsync(result);

            // Act & Assert
            var exception = await Assert.ThrowsAsync<HttpRequestException>(() => 
                _handler.Handle(new SignUpCommand { Email = "user@example.com", Password = "Password123!", MarketingEmail = true }, CancellationToken.None));

            Assert.Contains("DuplicateUserName", exception.Message);
        }

        [Fact]
        public async Task Handle_ConfirmEmail_Success()
        {
            // Arrange
            var user = new IdentityUser { UserName = "user@example.com", Email = "user@example.com" };
            var token = "confirmation_token";

            _authenticationServiceMock
                .Setup(s => s.CreateAsync(It.IsAny<IdentityUser>(), It.IsAny<string>())).ReturnsAsync(IdentityResult.Success);

            _authenticationServiceMock
                .Setup(s => s.GetUserIdAsync(It.IsAny<IdentityUser>())).ReturnsAsync(Guid.NewGuid().ToString());

            _authenticationServiceMock
                .Setup(s => s.ConfirmEmailAsync(It.IsAny<IdentityUser>(), token));

            // Act
            var result = await _handler.Handle(new SignUpCommand { Email = "user@example.com", Password = "Password123!", MarketingEmail = true }, CancellationToken.None);

            // Assert
            Assert.NotNull(result);
        }
    }
}
