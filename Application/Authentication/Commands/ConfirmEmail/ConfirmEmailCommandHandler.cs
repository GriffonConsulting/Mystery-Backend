using Application.Common.Interfaces;
using Application.Common.Requests;
using MediatR;
using System.ComponentModel.DataAnnotations;

namespace Application.Authentication.Commands.ConfirmEmail
{
    public class ConfirmEmailCommandHandler : IRequestHandler<ConfirmEmailCommand, RequestResult>
    {
        private readonly IAuthentication _authentication;

        public ConfirmEmailCommandHandler(IAuthentication authentication)
        {
            _authentication = authentication;
        }

        public async Task<RequestResult> Handle(ConfirmEmailCommand request, CancellationToken cancellationToken)
        {
            var user = await _authentication.FindByEmailAsync(request.Email) ?? throw new HttpRequestException("userError");

            var result = await _authentication.ConfirmEmailAsync(user, request.Token);
            if (!result.Succeeded) throw new ValidationException(result.Errors.First().Code);

            return new RequestResult { };
        }
    }
}
