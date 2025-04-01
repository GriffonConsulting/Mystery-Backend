using Application.Common.Requests;
using MediatR;
using System.ComponentModel.DataAnnotations;

namespace Application.Authentication.Commands.SignUp
{
    public class SignUpCommand : IRequest<RequestResult>
    {
        [EmailAddress]
        public required string Email { get; set; }
        public required string Password { get; set; }
        public required bool MarketingEmail { get; set; }
    }
}
