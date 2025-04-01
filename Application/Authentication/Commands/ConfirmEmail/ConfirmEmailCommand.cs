using Application.Common.Requests;
using MediatR;
using System.ComponentModel.DataAnnotations;

namespace Application.Authentication.Commands.ConfirmEmail;

public class ConfirmEmailCommand : IRequest<RequestResult>
{
    [EmailAddress]
    public required string Email { get; set; }
    public required string Token { get; set; }
}
