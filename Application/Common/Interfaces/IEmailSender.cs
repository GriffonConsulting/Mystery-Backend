using Email;
using System.Net.Mail;

namespace Application.Common.Interfaces
{
    public interface IEmailSender
    {
        public void SendEmail(MailAddress to, string templateHtml, string lang, params KeyValuePair<string, string>[] args);
    }
}