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

        public ForgotPasswordCommandHandler(IAuthentication authenticationService, IEmailSender emailSenderService)
        {
            _authenticationService = authenticationService;
            _emailSenderService = emailSenderService;
        }

        public async Task<RequestResult> Handle(ForgotPasswordCommand request, CancellationToken cancellationToken)
        {
            var user = await _authenticationService.FindByEmailAsync(request.Email) ?? throw new NotFoundException("userNotFound");

            var token = await _authenticationService.GeneratePasswordResetTokenAsync(user);
            //todo get lang in user table
            _emailSenderService.SendEmail(new MailAddress(request.Email), TemplateHtml.ForgotPassword, "fr");

            return new RequestResult
            {
            };
        }
    }
}
