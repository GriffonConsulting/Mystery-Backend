using System.Net.Mail;

namespace Application.Common.Interfaces
{
    public interface IEmailSenderService
    {
        public void SendEmail(MailAddress to, string subject, string body);
    }
}