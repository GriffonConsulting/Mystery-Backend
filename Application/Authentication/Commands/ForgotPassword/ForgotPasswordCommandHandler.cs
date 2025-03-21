using Application.Common.Exceptions;
using Application.Common.Interfaces.Services;
using Application.Common.Requests;
using MediatR;
using Microsoft.Extensions.Configuration;
using System.Net.Mail;

namespace Application.Authentication.Commands.ForgotPassword
{
    public class ForgotPasswordCommandHandler : IRequestHandler<ForgotPasswordCommand, RequestResult>
    {
        private readonly IAuthenticationService _authenticationService;
        private readonly IEmailSenderService _emailSenderService;

        public ForgotPasswordCommandHandler(IAuthenticationService authenticationService, IEmailSenderService emailSenderService)
        {
            _authenticationService = authenticationService;
            _emailSenderService = emailSenderService;
        }

        public async Task<RequestResult> Handle(ForgotPasswordCommand request, CancellationToken cancellationToken)
        {
            var user = await _authenticationService.FindByEmailAsync(request.Email) ?? throw new NotFoundException("user");

            var token = await _authenticationService.GeneratePasswordResetTokenAsync(user);
            //todo conf
            _emailSenderService.SendEmail(new MailAddress(request.Email), "Réinitialisation de votre mot de passe", "<!doctypehtml><html lang=fr><meta charset=UTF-8><meta content=\"width=device-width,initial-scale=1\"name=viewport><title>Réinitialisation de mot de passe</title><style>body{font-family:Arial,sans-serif;background-color:#f9f9f9;margin:0;padding:0}.email-container{max-width:600px;margin:20px auto;background:#fff;padding:20px;border-radius:8px;box-shadow:0 2px 5px rgba(0,0,0,.1)}h1{font-size:24px;color:#333}p{font-size:16px;color:#555;line-height:1.5}a{display:inline-block;margin-top:20px;padding:10px 20px;font-size:16px;color:#fff;background-color:#007bff;text-decoration:none;border-radius:5px}a:hover{background-color:#0056b3}.footer{margin-top:20px;font-size:12px;color:#888}</style><div class=email-container><h1>Réinitialisation de votre mot de passe</h1><p>Bonjour [Prénom ou Nom d'utilisateur],<p>Nous avons reçu une demande pour réinitialiser votre mot de passe. Si vous êtes à l'origine de cette demande, veuillez cliquer sur le lien ci-dessous pour créer un nouveau mot de passe :</p><a href=https://www.example.com/reset-password>Réinitialiser mon mot de passe</a><p>Ce lien est valable pendant [durée de validité, ex. 24 heures].<p>Si vous n'êtes pas à l'origine de cette demande, vous pouvez ignorer cet email en toute sécurité. Votre mot de passe actuel restera inchangé.<p class=footer>Pour toute question ou assistance supplémentaire, contactez-nous à <a href=mailto:support@example.com>support@example.com</a>.<p class=footer>Merci,<br>L'équipe [Nom du site ou de l'entreprise]</div>");

            return new RequestResult
            {
            };
        }
    }
}
