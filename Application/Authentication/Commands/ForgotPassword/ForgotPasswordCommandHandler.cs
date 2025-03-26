using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Application.Common.Requests;
using Email;
using MediatR;
using Microsoft.Extensions.Configuration;
using System.Net.Mail;

namespace Application.Authentication.Commands.ForgotPassword
{
    public class ForgotPasswordCommandHandler : IRequestHandler<ForgotPasswordCommand, RequestResult>
    {
        private readonly IAuthentication _authenticationService;
        private readonly IEmailSender _emailSenderService;
        private readonly IConfiguration _configuration;

        public ForgotPasswordCommandHandler(IAuthentication authenticationService, IEmailSender emailSenderService, IConfiguration configuration)
        {
            _authenticationService = authenticationService;
            _emailSenderService = emailSenderService;
            _configuration = configuration;
        }

        public async Task<RequestResult> Handle(ForgotPasswordCommand request, CancellationToken cancellationToken)
        {
            var user = await _authenticationService.FindByEmailAsync(request.Email) ?? throw new NotFoundException("userNotFound");

            var token = await _authenticationService.GeneratePasswordResetTokenAsync(user);
            var resetPasswordUrl = _configuration["Urls:FrontEndUrl"] + _configuration["Urls:ResetPasswordUrl"] + token;
            //todo get lang in user table
            _emailSenderService.SendEmail(new MailAddress(request.Email), TemplateHtml.ForgotPassword, "fr", new KeyValuePair<string, string>("resetPasswordUrl", resetPasswordUrl));

            return new RequestResult
            {
            };
        }
    }
}
