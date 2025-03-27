using Application.Common.Requests;
using MediatR;

namespace Application.Authentication.Queries.SignIn;

public class SignInCommand : IRequest<RequestResult<SignInDto>>
{
    public required string Email { get; set; }
    public required string Password { get; set; }
}
