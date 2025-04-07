using Application.Common.Requests;
using Application.Faq.Queries.GetFaq;
using Application.Product.Queries.GetProduct;
using Application.User.Commands.UpdateUser;
using Application.User.Queries.GetUser;
using Domain.Enums.Product;
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

        [HttpGet("{lang}", Name = "GetFaq")]
        [ProducesResponseType(typeof(RequestResult<GetFaqDto>), StatusCodes.Status200OK)]
        [AllowAnonymous]
        public async Task<IActionResult> GetProducts([FromRoute] string lang, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(new GetFaqQuery { Lang = lang }, cancellationToken);
            return Ok(result);
        }
    }
}