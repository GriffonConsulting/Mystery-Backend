using Application.Authentication.Commands.ForgotPassword;
using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Email;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Moq;
using System.Net.Mail;

public class ForgotPasswordCommandHandlerTests
{
    private readonly Mock<IAuthentication> _authenticationServiceMock;
    private readonly Mock<IEmailSender> _emailSenderServiceMock;
    private readonly Mock<IConfiguration> _configurationMock;
    private readonly ForgotPasswordCommandHandler _handler;

    public ForgotPasswordCommandHandlerTests()
    {
        _authenticationServiceMock = new Mock<IAuthentication>();
        _emailSenderServiceMock = new Mock<IEmailSender>();
        _configurationMock = new Mock<IConfiguration>();

        _configurationMock.Setup(c => c["Urls:FrontEndUrl"]).Returns("https://frontend.com");
        _configurationMock.Setup(c => c["Urls:ResetPasswordUrl"]).Returns("/reset-password?token=");

        _handler = new ForgotPasswordCommandHandler(
            _authenticationServiceMock.Object,
            _emailSenderServiceMock.Object,
            _configurationMock.Object
        );
    }

    [Fact]
    public async Task Handle_ShouldThrowNotFoundException_WhenUserDoesNotExist()
    {
        // Arrange
        var command = new ForgotPasswordCommand { Email = "nonexistent@example.com" };

        IdentityUser? user = null;

        _authenticationServiceMock
            .Setup(s => s.FindByEmailAsync(It.IsAny<string>()))
            .ReturnsAsync(user);

        // Act & Assert
        await Assert.ThrowsAsync<NotFoundException>(async () =>
            await _handler.Handle(command, CancellationToken.None));
    }

    [Fact]
    public async Task Handle_ShouldSendEmail_WhenUserExists()
    {
        // Arrange
        IdentityUser? user = new IdentityUser { UserName = "user@example.com", Email = "user@example.com" };
        var command = new ForgotPasswordCommand { Email = user.Email };
        var token = "reset-token";
        var expectedUrl = "https://frontend.com/reset-password?token=" + token;



        _authenticationServiceMock
            .Setup(s => s.FindByEmailAsync(It.IsAny<string>()))
            .ReturnsAsync(user);

        _authenticationServiceMock
            .Setup(a => a.GeneratePasswordResetTokenAsync(user))
            .ReturnsAsync(token);

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        _emailSenderServiceMock.Verify(
            e => e.SendEmail(
                It.Is<MailAddress>(m => m.Address == user.Email),
                TemplateHtml.ForgotPassword,
                "fr",
                It.Is<KeyValuePair<string, string>[]>(args => args[0].Key == "resetPasswordUrl" && args[0].Value == expectedUrl)
            ),
            Times.Once
        );
    }
}