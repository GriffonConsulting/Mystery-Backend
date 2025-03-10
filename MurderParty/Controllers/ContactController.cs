using Application.Common.Requests;
using Application.Contact.Commands.Contact;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MurderParty.Api.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ContactController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ContactController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost(Name = "Contact")]
        [ProducesResponseType(typeof(RequestResult), RequestStatusCodes.Status200OK)]
        [ProducesResponseType(typeof(RequestResult), RequestStatusCodes.Status400BadRequest)]
        [AllowAnonymous]
        public async Task<IActionResult> Contact(ContactCommand contactCommand, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(contactCommand, cancellationToken);

            return Ok(result);
        }


    }
}