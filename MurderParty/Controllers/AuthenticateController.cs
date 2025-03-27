using Application.Authentication.Commands.ConfirmEmail;
using Application.Authentication.Commands.ForgotPassword;
using Application.Authentication.Commands.ResetPassword;
using Application.Authentication.Commands.SignUp;
using Application.Authentication.Queries.SignIn;
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
        [ProducesResponseType(typeof(RequestResult<SignInDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(RequestResult), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(RequestResult<SignInDto>), StatusCodes.Status400BadRequest)]
        [AllowAnonymous]
        public async Task<IActionResult> SignUp(SignUpCommand signUpCommand, CancellationToken cancellationToken)
        {
            var signUpResult = await _mediator.Send(signUpCommand, cancellationToken);
            var signInResult = await _mediator.Send(new SignInCommand { Email = signUpCommand.Email, Password = signUpCommand.Password }, cancellationToken);

            return Ok(signInResult);
        }


        [HttpPost("ConfirmEmail", Name = "ConfirmEmail")]
        [AllowAnonymous]
        public async Task<IActionResult> ConfirmEmail(ConfirmEmailCommand confirmEmailCommand, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(confirmEmailCommand, cancellationToken);
            return Ok(result);
        }

        [HttpPost("SignIn", Name = "SignIn")]
        [ProducesResponseType(typeof(RequestResult<SignInDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(RequestResult<SignInDto>), StatusCodes.Status400BadRequest)]
        [AllowAnonymous]
        public async Task<IActionResult> SignIn(SignInCommand signInCommand, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(signInCommand, cancellationToken);
            return Ok(result);
        }

        [HttpPost("ForgotPassword", Name = "ForgotPassword")]
        [ProducesResponseType(typeof(RequestResult<SignInDto>), StatusCodes.Status200OK)]
        [AllowAnonymous]
        public async Task<IActionResult> ForgotPassword(ForgotPasswordCommand forgotPasswordCommand, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(forgotPasswordCommand, cancellationToken);
            return Ok(result);
        }

        [HttpPost("ResetPassword", Name = "ResetPassword")]
        [ProducesResponseType(typeof(RequestResult), StatusCodes.Status200OK)]
        [AllowAnonymous]
        public async Task<IActionResult> ResetPassword(ResetPasswordCommand resetPasswordCommand, CancellationToken cancellationToken)
        {
            await _mediator.Send(resetPasswordCommand, cancellationToken);
            return Ok();
        }
    }
}