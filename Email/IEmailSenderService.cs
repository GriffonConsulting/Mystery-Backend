using System.Net.Mail;

namespace EmailSender
{
    public interface IEmailSenderService
    {
        public void SendEmail(MailAddress to, string subject, string body);
    }
}