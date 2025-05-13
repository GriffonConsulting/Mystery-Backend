using Application.Common.Requests;
using Application.Payment.Commands.Checkout;
using Application.Payment.Commands.InvoiceFinalized;
using Application.Payment.Commands.PaymentIntentSucceeded;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MurderParty.Api.Helpers;
using Stripe;

namespace MurderParty.Api.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class StripeController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IConfiguration _configuration;

        public StripeController(IMediator mediator, IConfiguration configuration)
        {
            _mediator = mediator;
            _configuration = configuration;
        }

        [HttpPost("Checkout", Name = "Checkout")]
        [ProducesResponseType(typeof(RequestResult<CheckoutOutDto>), StatusCodes.Status200OK)]
        [Authorize]
        public async Task<IActionResult> Checkout(CheckoutCommand checkoutProductCommand, CancellationToken cancellationToken)
        {
            checkoutProductCommand.Email = Request.Email();
            checkoutProductCommand.UserId = Request.UserId();
            var result = await _mediator.Send(checkoutProductCommand,
                cancellationToken);

            return Ok(result);
        }

        [HttpPost("Webhook", Name = "Webhook")]
        [AllowAnonymous]
        public async Task<IActionResult> Index(CancellationToken cancellationToken)
        {
            var json = await new StreamReader(HttpContext.Request.Body).ReadToEndAsync(cancellationToken);

            try
            {
                var stripeEvent = EventUtility.ConstructEvent(json,
                    Request.Headers["Stripe-Signature"], _configuration["Stripe:WebhookKey"]);
                if (stripeEvent.Type == EventTypes.PaymentIntentSucceeded)
                {
                    var paymentIntent = stripeEvent.Data.Object as PaymentIntent;

                    var result = await _mediator.Send(
                        new PaymentIntentSucceededCommand
                        {
                            PaymentIntent = paymentIntent!
                        }, cancellationToken);

                    return Ok();
                }
                if (stripeEvent.Type == EventTypes.InvoicePaymentSucceeded)
                {
                    var invoice = stripeEvent.Data.Object as Invoice;

                    var result = await _mediator.Send(
                        new InvoicePaymentSucceededCommand
                        {
                            Invoice = invoice!
                        }, cancellationToken);

                    return Ok();
                }
                else
                {

                    return Ok();
                }
            }
            catch (StripeException)
            {
                return BadRequest();
            }
        }

    }
}