using Application.Common.Interfaces.Repositories;
using Application.Common.Requests;
using Domain.Entities;
using MediatR;
using Stripe;

namespace Application.Payment.Commands.PaymentIntentSucceeded
{
    public class PaymentIntentSucceededCommandHandler : IRequestHandler<PaymentIntentSucceededCommand, RequestResult>
    {
        private IOrderRepository _orderRepository { get; }
        private IOrderContentRepository _orderContentRepository { get; }

        public PaymentIntentSucceededCommandHandler(IOrderRepository orderRepository, IOrderContentRepository orderContentRepository)
        {
            _orderRepository = orderRepository;
            _orderContentRepository = orderContentRepository;
        }


        public async Task<RequestResult> Handle(PaymentIntentSucceededCommand request, CancellationToken cancellationToken)
        {
            var productsIds = request.PaymentIntent.Metadata["ProductsIds"].Split(',');
            var chargeService = new ChargeService();
            var latestCharge = await chargeService.GetAsync(request.PaymentIntent.LatestChargeId, cancellationToken: cancellationToken);

            var orderId = await _orderRepository.AddAsync(
                new Order
                {
                    Amount = request.PaymentIntent.Amount,
                    UserId = Guid.Parse(request.PaymentIntent.Metadata["UserId"]),
                    PaymentIntentId = request.PaymentIntent.Id,
                }, cancellationToken);

            foreach (var productId in productsIds)
            {
                await _orderContentRepository.AddAsync(
                    new OrderContent
                    {
                        ProductId = Guid.Parse(productId),
                        UserId = Guid.Parse(request.PaymentIntent.Metadata["UserId"]),
                        OrderId = orderId,
                    }, cancellationToken);
            }

            return new RequestResult
            {
            };
        }
    }
}
