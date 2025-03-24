using System.Net.Mail;

namespace Application.Common.Interfaces
{
    public interface IEmailSender
    {
        public void SendEmail(MailAddress to, string subject, string body);
    }
}