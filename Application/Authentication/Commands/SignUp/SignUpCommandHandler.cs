using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Application.Common.Interfaces.Repositories;
using Application.Common.Requests;
using Domain.Authorization;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System.Net.Mail;

namespace Application.Authentication.Commands.SignUp
{
    public class SignUpCommandHandler : IRequestHandler<SignUpCommand, RequestResult>
    {
        private readonly IUserRepository _userRepository;
        private readonly IEmailSender _emailSenderService;
        private readonly IAuthentication _authenticationService;

        public SignUpCommandHandler(IAuthentication authenticationService, IUserRepository userRepository, IEmailSender emailSenderService)
        {
            _authenticationService = authenticationService;
            _userRepository = userRepository;
            _emailSenderService = emailSenderService;
        }

        public async Task<RequestResult> Handle(SignUpCommand request, CancellationToken cancellationToken)
        {
            var user = new IdentityUser { UserName = request.Email, Email = request.Email };

            var result = await _authenticationService.CreateAsync(user, request.Password);

            if (!result.Succeeded) throw new DuplicateException("userDuplicate");

            await _authenticationService.AddToRoleAsync(user, nameof(UserRoles.User));
            var userId = await _authenticationService.GetUserIdAsync(user);
            var newUser = new Domain.Entities.User
            {
                Id = Guid.Parse(userId),
                MarketingEmail = request.MarketingEmail
            };
            await _userRepository.AddAsync(newUser, cancellationToken);
            var token = await _authenticationService.GenerateEmailConfirmationTokenAsync(user);
            _emailSenderService.SendEmail(new MailAddress(request.Email), "Création de compte", token);
            await _authenticationService.ConfirmEmailAsync(user, token);

            return new RequestResult {  };
        }
    }
}
