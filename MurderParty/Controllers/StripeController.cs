using Application.Common.Requests;
using Application.Payment.Commands.Checkout;
using Application.Payment.Commands.WebhookPaymentIntent;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MurderParty.Helpers;
using Stripe;

namespace MurderParty.Api.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class StripeController : ControllerBase
    {
        private readonly IMediator _mediator;

        public StripeController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("Checkout", Name = "Checkout")]
        [ProducesResponseType(typeof(RequestResult<CheckoutOutDto>), StatusCodes.Status200OK)]
        [Authorize]
        public async Task<IActionResult> Checkout(CheckoutProductsCommand checkoutProductCommand, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(
                new CheckoutCommand
                {
                    CheckoutProductsCommand = checkoutProductCommand,
                    Email = Request.Email(),
                    UserId = Request.UserId()
                },
                cancellationToken);

            return Ok(result);
        }

        [HttpPost("Webhook/PaymentIntent/Succeeded")]
        [AllowAnonymous]
        public async Task<IActionResult> WebhookPaymentIntentSucceeded(CancellationToken cancellationToken)
        {
            var json = await new StreamReader(HttpContext.Request.Body).ReadToEndAsync(cancellationToken);
            //todo check signature

            try
            {
                var stripeEvent = EventUtility.ParseEvent(json);
                var paymentIntent = stripeEvent.Data.Object as PaymentIntent;

                var result = await _mediator.Send(
                    new PaymentIntentCommand
                    {
                        PaymentIntent = paymentIntent!
                    }, cancellationToken);

                return Ok();
            }
            catch (StripeException)
            {
                return BadRequest();
            }
        }

    }
}