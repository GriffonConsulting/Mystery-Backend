using Application.Common.Exceptions;
using Application.Common.Interfaces.Repositories;
using Application.Common.Requests;
using MediatR;

namespace Application.Payment.Commands.InvoiceFinalized
{
    public class InvoiceFinalizedCommandHandler : IRequestHandler<InvoiceFinalizedCommand, RequestResult>
    {
        private IOrderRepository _orderRepository { get; }

        public InvoiceFinalizedCommandHandler(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }


        public async Task<RequestResult> Handle(InvoiceFinalizedCommand request, CancellationToken cancellationToken)
        {
            var order = await _orderRepository.GetByPaymentIntentIdAsync(request.Invoice.Payments.FirstOrDefault().Payment.PaymentIntentId, cancellationToken);

            if (order == null) throw new NotFoundException("orderNotFound");

            order.ReceiptUrl = request.Invoice.InvoicePdf;
            await _orderRepository.UpdateEntityAsync(order, cancellationToken);

            return new RequestResult    
            {
            };
        }
    }
}
