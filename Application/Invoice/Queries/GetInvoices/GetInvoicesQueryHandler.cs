using Application.Common.Interfaces.Repositories;
using Application.Common.Requests;
using MediatR;

namespace Application.Invoices.Queries.GetInvoices
{
    public class GetInvoicesQueryHandler : IRequestHandler<GetInvoicesQuery, RequestResult<GetInvoicesDto[]>>
    {
        private readonly IOrderRepository orderRepository;

        public GetInvoicesQueryHandler(IOrderRepository orderRepository)
        {
            this.orderRepository = orderRepository;
        }


        public async Task<RequestResult<GetInvoicesDto[]>> Handle(GetInvoicesQuery request, CancellationToken cancellationToken)
        {
            var invoices = await orderRepository.GetByUserIdAsync(request.UserId, cancellationToken);

            return new RequestResult<GetInvoicesDto[]>
            {
                Result = invoices.Select((i) => new GetInvoicesDto
                {
                    Id = i.Id,
                    Amount = i.Amount,
                    CreatedOn = i.CreatedOn,
                }).OrderByDescending(i => i.CreatedOn).ToArray()
            };
        }
    }
}
