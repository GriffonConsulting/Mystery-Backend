using Application.Authentication.Commands.ConfirmEmail;
using Application.Authentication.Commands.SignIn;
using Application.Authentication.Commands.SignUp;
using Application.Common.Exceptions;
using Application.Common.Requests;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MurderParty.Api.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class AuthenticateController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AuthenticateController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("SignUp", Name = "SignUp")]
        [ProducesResponseType(typeof(RequestResult<SignInDto>), RequestStatusCodes.Status200OK)]
        [ProducesResponseType(typeof(RequestResult), RequestStatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(RequestResult<SignInDto>), RequestStatusCodes.Status400BadRequest)]
        [AllowAnonymous]
        public async Task<IActionResult> SignUp(SignUpCommand signUpCommand, CancellationToken cancellationToken)
        {
            try
            {
                var signUpResult = await _mediator.Send(signUpCommand, cancellationToken);
                var signInResult = await _mediator.Send(new SignInCommand { Email = signUpCommand.Email, Password = signUpCommand.Password }, cancellationToken);

                return Ok(signInResult);
            }
            catch (HttpRequestException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (NotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }


        [HttpPost("ConfirmEmail", Name = "ConfirmEmail")]
        [AllowAnonymous]
        public async Task<IActionResult> ConfirmEmail(ConfirmEmailCommand confirmEmailCommand, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(confirmEmailCommand, cancellationToken);
            return Ok(result);
        }

        [HttpPost("SignIn", Name = "SignIn")]
        [ProducesResponseType(typeof(RequestResult<SignInDto>), RequestStatusCodes.Status200OK)]
        [ProducesResponseType(typeof(RequestResult<SignInDto>), RequestStatusCodes.Status400BadRequest)]
        [AllowAnonymous]
        public async Task<IActionResult> SignIn(SignInCommand signInCommand, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(signInCommand, cancellationToken);
            return Ok(result);
        }
    }
}