using Application.Authentication.Commands.ConfirmEmail;
using Application.Authentication.Commands.ForgotPassword;
using Application.Authentication.Commands.ResetPassword;
using Application.Authentication.Commands.SignUp;
using Application.Authentication.Queries.SignIn;
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
        private readonly IConfiguration _configuration;

        public AuthenticateController(IMediator mediator, IConfiguration configuration)
        {
            _mediator = mediator;
            _configuration = configuration;
        }

        [HttpPost("SignUp", Name = "SignUp")]
        [ProducesResponseType(typeof(RequestResult<SignInDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(RequestResult), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(RequestResult<SignInDto>), StatusCodes.Status400BadRequest)]
        [AllowAnonymous]
        public async Task<IActionResult> SignUp(SignUpCommand signUpCommand, CancellationToken cancellationToken)
        {
            var signUpResult = await _mediator.Send(signUpCommand, cancellationToken);
            var signInResult = await _mediator.Send(new SignInQuery { Email = signUpCommand.Email, Password = signUpCommand.Password }, cancellationToken);

            Response.Cookies.Append("access_token", signInResult.Result.Token, new CookieOptions
            {
                HttpOnly = true,
                Secure = bool.Parse(_configuration["Cookies:Secure"]),  
                SameSite = SameSiteMode.Strict,
                Expires = signInResult.Result.ExpirationDate,
                Path = "/"
            });

            return Ok();
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
        public async Task<IActionResult> SignIn(SignInQuery signInCommand, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(signInCommand, cancellationToken);

            Response.Cookies.Append("access_token", result.Result.Token, new CookieOptions
            {
                HttpOnly = true,
                Secure = bool.Parse(_configuration["Cookies:Secure"]),    
                SameSite = SameSiteMode.Strict,
                Expires = result.Result.ExpirationDate,
                Path = "/"
            });

            return Ok();
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

        [HttpGet("Me", Name = "Me")]
        [Authorize] 
        public IActionResult Me()
        {
            return Ok();
        }

        [HttpPost("SignOut", Name = "SignOut")]
        public IActionResult Logout()
        {
            Response.Cookies.Delete("access_token");

            return Ok();
        }
    }
}