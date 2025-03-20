using Application.Common.Requests;
using Application.Product.Queries.GetProduct;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MurderParty.Helpers;

namespace MurderParty.Api.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IMediator _mediator;

        public UserController(IMediator mediator)
        {
            _mediator = mediator;
        }


        [HttpGet(Name = "GetUser")]
        [ProducesResponseType(typeof(RequestResult<GetProductDto[]>), StatusCodes.Status200OK)]
        [AllowAnonymous]
        public async Task<IActionResult> GetProducts(CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(new GetUserQuery {  ClientId = Request.UserId() }, cancellationToken);
            return Ok(result);
        }
    }
}