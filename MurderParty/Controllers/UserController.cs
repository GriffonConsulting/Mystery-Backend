using Application.Common.Requests;
using Application.User.Commands.UpdateUser;
using Application.User.Queries.GetUser;
using Application.User.Queries.GetUserGames;
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
            updateUserQuery.OldEmail = Request.Email();
            updateUserQuery.UserId = Request.UserId();
            var result = await _mediator.Send(updateUserQuery, cancellationToken);
            return Ok(result);
        }

        [HttpGet("games", Name = "GetUserGames")]
        [ProducesResponseType(typeof(RequestResult<GetUserGamesDto>), StatusCodes.Status200OK)]
        [Authorize]
        public async Task<IActionResult> GetUserGames(CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(new GetUserGamesQuery { ClientId = Request.UserId() }, cancellationToken);
            return Ok(result);
        }
    }
}