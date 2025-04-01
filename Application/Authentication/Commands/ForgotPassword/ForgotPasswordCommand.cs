using Application.Common.Requests;
using MediatR;
using System.ComponentModel.DataAnnotations;

namespace Application.Authentication.Commands.ForgotPassword;

public class ForgotPasswordCommand : IRequest<RequestResult>
{
    [EmailAddress]
    public required string Email { get; set; }
}
