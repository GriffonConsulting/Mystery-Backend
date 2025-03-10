using Application.Common.Requests;
using Application.Invoices.Queries.GetInvoices;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MurderParty.Helpers;

namespace MurderParty.Api.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class InvoiceController : ControllerBase
    {
        private readonly IMediator _mediator;

        public InvoiceController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("ByUserId", Name = "GetInvoicesByUserId")]
        [ProducesResponseType(typeof(RequestResult<GetInvoicesResult[]>), StatusCodes.Status200OK)]
        [Authorize]
        public async Task<IActionResult> GetInvoicesByUserId(CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(new GetInvoicesQuery { UserId = Request.UserId() }, cancellationToken);
            return Ok(result);
        }

    }
}