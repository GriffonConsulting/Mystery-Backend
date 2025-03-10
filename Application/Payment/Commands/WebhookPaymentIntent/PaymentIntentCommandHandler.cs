using Application.Common.Requests;
using Database.Commands;
using Domain.Entities;
using MediatR;
using Stripe;

namespace Application.Payment.Commands.WebhookPaymentIntent
{
    public class PaymentIntentCommandHandler : IRequestHandler<PaymentIntentCommand, RequestResult>
    {
        private DbOrderCommands _orderCommands { get; }
        private OrderContentCommands _orderContentCommands { get; }

        public PaymentIntentCommandHandler(DbOrderCommands orderCommands, OrderContentCommands orderContentCommands)
        {
            _orderCommands = orderCommands;
            _orderContentCommands = orderContentCommands;
        }


        public async Task<RequestResult> Handle(PaymentIntentCommand request, CancellationToken cancellationToken)
        {
            var productsIds = request.PaymentIntent.Metadata["ProductsIds"].Split(',');
            var chargeService = new ChargeService();
            var latestCharge = await chargeService.GetAsync(request.PaymentIntent.LatestChargeId, cancellationToken: cancellationToken);

            var orderId = await _orderCommands.AddAsync(
                new Order
                {
                    ReceiptUrl = latestCharge.ReceiptUrl,
                    Amount = request.PaymentIntent.Amount,
                    CreatedOn = DateTime.UtcNow,
                    ModifiedOn = DateTime.UtcNow,
                    UserId = Guid.Parse(request.PaymentIntent.Metadata["UserId"]),
                    StripeId = request.PaymentIntent.Id
                }, cancellationToken);

            foreach (var productId in productsIds)
            {
                await _orderContentCommands.AddAsync(
                    new OrderContent
                    {
                        ProductId = Guid.Parse(productId),
                        UserId = Guid.Parse(request.PaymentIntent.Metadata["UserId"]),
                        OrderId = orderId,
                        CreatedOn = DateTime.UtcNow,
                        ModifiedOn = DateTime.UtcNow,
                    }, cancellationToken);
            }

            return new RequestResult
            {
            };
        }
    }
}
