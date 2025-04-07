using Application.Common.Requests;
using Application.Faq.Queries.GetFaq;
using Application.Product.Queries.GetProduct;
using Application.User.Commands.UpdateUser;
using Application.User.Queries.GetUser;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MurderParty.Helpers;

namespace MurderParty.Api.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class FaqController : ControllerBase
    {
        private readonly IMediator _mediator;

        public FaqController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet(Name = "GetFaq")]
        [ProducesResponseType(typeof(RequestResult<GetFaqDto>), StatusCodes.Status200OK)]
        [Authorize]
        public async Task<IActionResult> GetProducts(CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(new GetUserQuery {  ClientId = Request.UserId(), Email = Request.Email() }, cancellationToken);
            return Ok(result);
        }
    }
}