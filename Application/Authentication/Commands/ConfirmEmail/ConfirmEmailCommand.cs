using Application.Common.Requests;
using MediatR;

namespace Application.Authentication.Commands.ConfirmEmail;

public class ConfirmEmailCommand : IRequest<RequestResult>
{
    public required string Email { get; set; }
    public required string Token { get; set; }
}
