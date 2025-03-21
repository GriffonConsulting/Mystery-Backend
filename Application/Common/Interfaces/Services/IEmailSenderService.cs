using System.Net.Mail;

namespace Application.Common.Interfaces.Services
{
    public interface IEmailSenderService
    {
        public void SendEmail(MailAddress to, string subject, string body);
    }
}