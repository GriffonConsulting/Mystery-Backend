using Application.Common.Interfaces;
using Application.Common.Requests;
using Email;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;

namespace Application.Contact.Commands.Contact;

public class ContactCommandHandler : IRequestHandler<ContactCommand, RequestResult>
{
    private readonly IEmailSender _emailSenderService;
    private readonly IConfiguration _configuration;

    public ContactCommandHandler(IEmailSender emailSenderService, IConfiguration configuration)
    {
        _emailSenderService = emailSenderService;
        _configuration = configuration;
    }

    public async Task<RequestResult> Handle(ContactCommand request, CancellationToken cancellationToken)
    {           
        //todo get lang in user table
        _emailSenderService.SendEmail(new System.Net.Mail.MailAddress(_configuration["Email:Contact"]), TemplateHtml.Contact, "fr");

        return new RequestResult { };
    }
}
