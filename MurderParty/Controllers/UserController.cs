using Application.Common.Requests;
using Application.User.Commands.UpdateUser;
using Application.User.Queries.GetUser;
using Application.User.Queries.GetUserProduct;
using Application.User.Queries.GetUserProducts;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MurderParty.Api.Helpers;

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
        [ProducesResponseType(typeof(RequestResult<GetUserDto>), StatusCodes.Status200OK)]
        [Authorize]
        public async Task<IActionResult> GetUser(CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(new GetUserQuery { ClientId = Request.UserId(), Email = Request.Email() }, cancellationToken);
            return Ok(result);
        }

        [HttpPut(Name = "UpdateUser")]
        [ProducesResponseType(typeof(RequestResult), StatusCodes.Status200OK)]
        [Authorize]
        public async Task<IActionResult> UpdateUser(UpdateUserCommand updateUserQuery, CancellationToken cancellationToken)
        {
            updateUserQuery.OldEmail = Request.Email(); //todo use HttpContext.User
            updateUserQuery.UserId = Request.UserId();
            var result = await _mediator.Send(updateUserQuery, cancellationToken);
            return Ok(result);
        }

        [HttpGet("products", Name = "GetUserProducts")]
        [ProducesResponseType(typeof(RequestResult<GetUserProductsDto>), StatusCodes.Status200OK)]
        [Authorize]
        public async Task<IActionResult> GetUserProducts(CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(new GetUserProductsQuery { ClientId = Request.UserId() }, cancellationToken);
            return Ok(result);
        }

        [HttpGet("product/{userProductId}/", Name = "GetUserProduct")]
        [ProducesResponseType(typeof(RequestResult<GetUserProductDto>), StatusCodes.Status200OK)]
        [Authorize]
        public async Task<IActionResult> GetUserProduct([FromRoute]Guid userProductId, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(new GetUserProductQuery { UserProductId = userProductId }, cancellationToken);
            return Ok(result);
        }
    }
}