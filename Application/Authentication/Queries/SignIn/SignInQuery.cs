using Application.Common.Requests;
using MediatR;

namespace Application.Authentication.Queries.SignIn;

public class SignInQuery : IRequest<RequestResult<SignInDto>>
{
    public required string Email { get; set; }
    public required string Password { get; set; }
}
