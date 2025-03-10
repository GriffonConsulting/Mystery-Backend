using EmailSender;
using Microsoft.Extensions.Configuration;
using System.Net;
using System.Net.Mail;

namespace Email
{
    public class EmailSenderService : IEmailSenderService
    {
        private readonly IConfiguration _configuration;

        public EmailSenderService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void SendEmail(MailAddress to, string subject, string body)
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

            using var message = new MailMessage(from.Address, to.Address)
            {
                Subject = subject,
                Body = $"<!doctypehtml><html lang=fr><meta charset=UTF-8><meta content=\"width=device-width,initial-scale=1\"name=viewport><title>Murder Party - Invitation</title><style>body{{margin:0;font-family:Arial,sans-serif;background-color:#1a1a1a;color:#f5f5f5}}.email-container{{max-width:600px;margin:0 auto;background-color:#2c2c2c;border-radius:8px;overflow:hidden}}.header{{background-color:#b22222;text-align:center;padding:20px}}.header h1{{margin:0;color:#fff;font-size:28px}}.content{{padding:20px}}.content p{{line-height:1.6}}.content h2{{color:#f5a623;margin-bottom:10px}}.cta-button{{display:inline-block;margin-top:20px;padding:10px 20px;background-color:#f5a623;color:#fff;text-decoration:none;border-radius:5px;font-weight:700}}.cta-button:hover{{background-color:#d48f20}}.footer{{text-align:center;padding:10px;background-color:#1a1a1a;font-size:12px;color:#ccc}}.footer a{{color:#f5a623;text-decoration:none}}</style><div class=email-container><div class=header><h1>🔪 Murder Party - Votre Invitation</h1><div><div class=\"content\">{body}</div><div class=footer><p>Vous avez des questions ? Consultez notre <a href=\"[Lien FAQ]\">FAQ</a> ou contactez-nous à <a href=mailto:contact@murderparty.com>contact@murderparty.com</a>.<p>© 2024 Murder Party. Tous droits réservés.</div></div>",
                IsBodyHtml = true
            };

            smtp.Send(message);
        }
    }
}
