using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Application.Common.Requests;
using MediatR;
using System.ComponentModel.DataAnnotations;

namespace Application.Authentication.Commands.ResetPassword
{
    public class ResetPasswordCommandHandler : IRequestHandler<ResetPasswordCommand, RequestResult>
    {
        private readonly IAuthentication _authentication;

        public ResetPasswordCommandHandler(IAuthentication authentication)
        {
            _authentication = authentication;
        }

        public async Task<RequestResult> Handle(ResetPasswordCommand request, CancellationToken cancellationToken)
        {
            var user = await _authentication.FindByEmailAsync(request.Email) ?? throw new NotFoundException("userNotFound");
            var result = await _authentication.ResetPasswordAsync(user, request.Token, request.Password);

            if (!result.Succeeded)
                throw new ValidationException(string.Join(",", result.Errors.Select(e => e.Code)));

            return new RequestResult
            {
            };
        }
    }
}
