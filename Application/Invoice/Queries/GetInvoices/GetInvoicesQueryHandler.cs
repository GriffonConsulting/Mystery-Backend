using Application.Common.Interfaces.Repositories;
using Application.Common.Requests;
using MediatR;

namespace Application.Invoices.Queries.GetInvoices
{
    public class GetInvoicesQueryHandler : IRequestHandler<GetInvoicesQuery, RequestResult<GetInvoicesDto[]>>
    {
        private IOrderRepository _orderRepository { get; }

        public GetInvoicesQueryHandler(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }


        public async Task<RequestResult<GetInvoicesDto[]>> Handle(GetInvoicesQuery request, CancellationToken cancellationToken)
        {
            var invoices = await _orderRepository.GetByUserIdAsync(request.UserId, cancellationToken);

            return new RequestResult<GetInvoicesDto[]>
            {
                Result = invoices.Select((i) => new GetInvoicesDto
                {
                    Amount = i.Amount,
                    CreatedOn = i.CreatedOn,
                    ReceiptUrl = i.ReceiptUrl,
                }).ToArray()
            };
        }
    }
}
