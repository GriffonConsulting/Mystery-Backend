using Microsoft.Extensions.Configuration;
using Moq;
using System.Net.Mail;

namespace Email.Tests
{
    public class EmailSenderServiceTests
    {
        private readonly Mock<IConfiguration> _configurationMock;
        private readonly EmailSenderService _emailSenderService;

        public EmailSenderServiceTests()
        {
            _configurationMock = new Mock<IConfiguration>();
            _configurationMock.Setup(c => c["Email:Sender"]).Returns("test@example.com");
            _configurationMock.Setup(c => c["Email:Secret"]).Returns("password");
            _configurationMock.Setup(c => c["Email:Faq"]).Returns("https://faq.example.com");
            _configurationMock.Setup(c => c["Email:MailTo"]).Returns("support@example.com");

            _emailSenderService = new EmailSenderService(_configurationMock.Object);
        }

        [Fact]
        public void SendEmail_ShouldThrowException_WhenRecipientIsNull()
        {
            // Arrange
            var exception = Record.Exception(() =>
                _emailSenderService.SendEmail(null, TemplateHtml.SignUp, "fr"));

            // Assert
            Assert.NotNull(exception);
            Assert.IsType<NullReferenceException>(exception);
        }

        [Fact]
        public void SendEmail_ShouldThrowException_WhenTemplateHtmlIsNull()
        {
            // Arrange
            var exception = Record.Exception(() =>
                _emailSenderService.SendEmail(new MailAddress("recipient@example.com"), null, "fr"));

            // Assert
            Assert.NotNull(exception);
            Assert.IsType<FileNotFoundException>(exception);
        }

        [Fact]
        public void SendEmail_ShouldThrowException_WhenLangIsNull()
        {
            // Arrange
            var exception = Record.Exception(() =>
                _emailSenderService.SendEmail(new MailAddress("recipient@example.com"), TemplateHtml.SignUp, null));

            // Assert
            Assert.NotNull(exception);
            Assert.IsType<FileNotFoundException>(exception);
        }
    }
}
