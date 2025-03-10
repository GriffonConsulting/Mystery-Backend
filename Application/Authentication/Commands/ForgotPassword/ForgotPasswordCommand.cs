using Application.Common.Requests;
using MediatR;

namespace Application.Authentication.Commands.ForgotPassword;

public class ForgotPasswordCommand : IRequest<RequestResult>
{
    public required string Email { get; set; }
    public required string Password { get; set; }
}
