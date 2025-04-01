using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Application.Common.Interfaces.Repositories;
using Application.Common.Requests;
using Domain.Authorization;
using Email;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System.Net.Mail;

namespace Application.Authentication.Commands.SignUp
{
    public class SignUpCommandHandler : IRequestHandler<SignUpCommand, RequestResult>
    {
        private readonly IUserRepository _userRepository;
        private readonly IEmailSender _emailSender;
        private readonly IAuthentication _authentication;

        public SignUpCommandHandler(IAuthentication authentication, IUserRepository userRepository, IEmailSender emailSender)
        {
            _authentication = authentication;
            _userRepository = userRepository;
            _emailSender = emailSender;
        }

        public async Task<RequestResult> Handle(SignUpCommand request, CancellationToken cancellationToken)
        {
            var user = new IdentityUser { UserName = request.Email, Email = request.Email };

            var result = await _authentication.CreateAsync(user, request.Password);

            if (!result.Succeeded) throw new DuplicateException("userDuplicate");

            await _authentication.AddToRoleAsync(user, nameof(UserRoles.User));
            var userId = await _authentication.GetUserIdAsync(user);
            var newUser = new Domain.Entities.User
            {
                Id = Guid.Parse(userId),
                MarketingEmail = request.MarketingEmail
            };
            await _userRepository.AddAsync(newUser, cancellationToken);
            var token = await _authentication.GenerateEmailConfirmationTokenAsync(user);
            //todo get lang in user table
            _emailSender.SendEmail(new MailAddress(request.Email), TemplateHtml.SignUp, "fr");
            await _authentication.ConfirmEmailAsync(user, token);

            return new RequestResult {  };
        }
    }
}
