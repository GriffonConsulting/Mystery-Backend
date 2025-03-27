using Application.Common.Requests;
using MediatR;

namespace Application.Authentication.Commands.ResetPassword;

public class ResetPasswordCommand : IRequest<RequestResult>
{
    public required string Token { get; set; }
    public required string Email { get; set; }
    public required string Password { get; set; }
}
