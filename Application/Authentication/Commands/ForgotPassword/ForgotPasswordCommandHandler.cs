using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Application.Common.Requests;
using Email;
using MediatR;
using Microsoft.Extensions.Configuration;
using System.Net.Mail;
using System.Web;

namespace Application.Authentication.Commands.ForgotPassword
{
    public class ForgotPasswordCommandHandler : IRequestHandler<ForgotPasswordCommand, RequestResult>
    {
        private readonly IAuthentication _authentication;
        private readonly IEmailSender _emailSender;
        private readonly IConfiguration _configuration;

        public ForgotPasswordCommandHandler(IAuthentication authentication, IEmailSender emailSender, IConfiguration configuration)
        {
            _authentication = authentication;
            _emailSender = emailSender;
            _configuration = configuration;
        }

        public async Task<RequestResult> Handle(ForgotPasswordCommand request, CancellationToken cancellationToken)
        {
            var user = await _authentication.FindByEmailAsync(request.Email) ?? throw new NotFoundException("userNotFound");

            var token = await _authentication.GeneratePasswordResetTokenAsync(user);
            var resetPasswordUrl = $"{_configuration["Urls:FrontEndUrl"]}{_configuration["Urls:ResetPasswordUrl"]}?email={HttpUtility.UrlEncode(request.Email)}&token={HttpUtility.UrlEncode(token)}";
            //todo get lang in user table
            _emailSender.SendEmail(new MailAddress(request.Email), TemplateHtml.ForgotPassword, "fr", new KeyValuePair<string, string>("resetPasswordUrl", resetPasswordUrl));

            return new RequestResult
            {
            };
        }
    }
}
