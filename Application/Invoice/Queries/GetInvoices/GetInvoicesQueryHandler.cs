using Application.Common.Interfaces.Repositories;
using Application.Common.Requests;
using MediatR;

namespace Application.Invoices.Queries.GetInvoices
{
    public class GetInvoicesQueryHandler : IRequestHandler<GetInvoicesQuery, RequestResult<GetInvoicesResult[]>>
    {
        private IOrderRepository _orderRepository { get; }

        public GetInvoicesQueryHandler(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }


        public async Task<RequestResult<GetInvoicesResult[]>> Handle(GetInvoicesQuery request, CancellationToken cancellationToken)
        {
            var invoices = await _orderRepository.GetByUserIdAsync(request.UserId, cancellationToken);

            return new RequestResult<GetInvoicesResult[]>
            {
                Result = invoices.Select((i) => new GetInvoicesResult
                {
                    Amount = i.Amount,
                    CreatedOn = i.CreatedOn,
                    ReceiptUrl = i.ReceiptUrl,
                }).ToArray()
            };
        }
    }
}
