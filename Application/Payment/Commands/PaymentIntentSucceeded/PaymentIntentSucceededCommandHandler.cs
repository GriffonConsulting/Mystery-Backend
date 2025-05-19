using Application.Common.Interfaces.Repositories;
using Application.Common.Requests;
using Domain.Entities;
using MediatR;
using Newtonsoft.Json.Linq;

namespace Application.Payment.Commands.PaymentIntentSucceeded
{
    public class PaymentIntentSucceededCommandHandler : IRequestHandler<PaymentIntentSucceededCommand, RequestResult>
    {
        private readonly IProductRepository productRepository;
        private readonly IUserProductRepository userProductRepository;
        private readonly IOrderRepository orderRepository;
        private readonly IOrderContentRepository orderContentRepository;

        public PaymentIntentSucceededCommandHandler(IOrderRepository orderRepository, IProductRepository productRepository, IUserProductRepository userProductRepository, IOrderContentRepository orderContentRepository)
        {
            this.orderRepository = orderRepository;
            this.productRepository = productRepository;
            this.userProductRepository = userProductRepository;
            this.orderContentRepository = orderContentRepository;
        }


        public async Task<RequestResult> Handle(PaymentIntentSucceededCommand request, CancellationToken cancellationToken)
        {
            var productsIds = request.PaymentIntent.Metadata["ProductsIds"].Split(',');

            var orderId = await orderRepository.AddAsync(
                new Order
                {
                    Amount = request.PaymentIntent.Amount,
                    UserId = Guid.Parse(request.PaymentIntent.Metadata["UserId"]),
                    PaymentIntentId = request.PaymentIntent.Id,
                }, cancellationToken);

            foreach (var productId in productsIds)
            {
                var product = await productRepository.GetById(Guid.Parse(productId));

                var orderContentId = await orderContentRepository.AddAsync(
                    new OrderContent
                    {
                        ProductId = Guid.Parse(productId),
                        UserId = Guid.Parse(request.PaymentIntent.Metadata["UserId"]),
                        OrderId = orderId,
                    }, cancellationToken);

                await userProductRepository.AddAsync(
                    new UserProduct
                    {
                        ProductId = Guid.Parse(productId),
                        UserId = Guid.Parse(request.PaymentIntent.Metadata["UserId"]),
                        OrderContentId = orderContentId,
                        ProductUserConfiguration = "{}",
                        ProductType = product.ProductType,
                    }, cancellationToken);
            }

            return new RequestResult
            {
            };
        }
    }
}
