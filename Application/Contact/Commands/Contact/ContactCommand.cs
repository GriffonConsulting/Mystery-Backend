using Application.Common.Requests;
using MediatR;

namespace Application.Contact.Commands.Contact;

public class ContactCommand : IRequest<RequestResult>
{
    public required string Email { get; set; }
    public required string Message { get; set; }
}
