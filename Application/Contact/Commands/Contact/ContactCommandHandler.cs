using Application.Common.Requests;
using EmailSender;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;

namespace Application.Contact.Commands.Contact;

public class ContactCommandHandler : IRequestHandler<ContactCommand, RequestResult>
{
    private readonly IEmailSenderService _emailSenderService;
    private readonly IConfiguration _configuration;

    public ContactCommandHandler(IEmailSenderService emailSenderService, IConfiguration configuration)
    {
        _emailSenderService = emailSenderService;
        _configuration = configuration;
    }

    public async Task<RequestResult> Handle(ContactCommand request, CancellationToken cancellationToken)
    {
        //todo conf
        _emailSenderService.SendEmail(new System.Net.Mail.MailAddress(_configuration["Email:Contact"]), $"contact {request.Email}", $"Email : {request.Email}\r\n{request.Message}");

        return new RequestResult { };
    }
}
