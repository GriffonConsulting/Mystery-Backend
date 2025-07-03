using Application.Common.Requests;
using Application.Invoices.Queries.GetInvoice;
using Application.Invoices.Queries.GetInvoices;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MurderParty.Api.Helpers;

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
        [ProducesResponseType(typeof(RequestResult<GetInvoicesDto[]>), StatusCodes.Status200OK)]
        [Authorize]
        public async Task<IActionResult> GetInvoicesByUserId(CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(new GetInvoicesQuery { UserId = HttpContext.User.GetUserId() }, cancellationToken);
            return Ok(result);
        }

        [HttpGet("ByOrderId/{orderId}", Name = "GetInvoicesByOrderId")]
        [ProducesResponseType(typeof(RequestResult<GetInvoiceDto>), StatusCodes.Status200OK)]
        [Authorize]
        public async Task<IActionResult> GetInvoiceByOrderId([FromRoute]Guid orderId, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(new GetInvoiceQuery { OrderId = orderId }, cancellationToken);
            return Ok(result);
        }
    }
}