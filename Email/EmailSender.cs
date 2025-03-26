using Application.Common.Interfaces;
using Microsoft.Extensions.Configuration;
using System.Net;
using System.Net.Mail;

namespace Email
{
    public class EmailSenderService : IEmailSender
    {
        private readonly IConfiguration _configuration;

        public EmailSenderService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void SendEmail(MailAddress to, string templateHtml, string lang, params KeyValuePair<string, string>[] args)
        {
            var from = new MailAddress(_configuration["Email:Sender"]!);

            var smtp = new SmtpClient
            {
                Host = "smtp.gmail.com",
                Port = 587,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(from.Address, _configuration["Email:Secret"])
            };
            var path = Path.GetDirectoryName(AppDomain.CurrentDomain.BaseDirectory);
            string emailHtml = File.ReadAllText($"{path}\\Html\\{lang}\\{TemplateHtml.Base}.html");
            string htmlContent = File.ReadAllText($"{path}\\Html\\{lang}\\{templateHtml}.html");
            emailHtml = emailHtml.Replace("{{body}}", htmlContent);
            emailHtml = emailHtml.Replace("{{faq}}", _configuration["Email:Faq"]);
            emailHtml = emailHtml.Replace("{{mailTo}}", _configuration["Email:mailTo"]);
            emailHtml = emailHtml.Replace("{{year}}", DateTime.Now.Year.ToString());

            foreach (var arg in args)
            {
                emailHtml = emailHtml.Replace($"{{{{{arg.Key}}}}}", arg.Value);
            }

            using var message = new MailMessage(from.Address, to.Address)
            {
                Subject = templateHtml.ToString(),
                Body = emailHtml,
                IsBodyHtml = true
            };

            smtp.Send(message);
        }
    }
}
