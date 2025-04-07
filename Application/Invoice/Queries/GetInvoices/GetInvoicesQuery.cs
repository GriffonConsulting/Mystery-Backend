using Application.Common.Requests;
using MediatR;
using System.Text.Json.Serialization;

namespace Application.Invoices.Queries.GetInvoices;

public class GetInvoicesQuery : IRequest<RequestResult<GetInvoicesDto[]>>
{
    [JsonIgnore]
    public Guid UserId { get; set; }
}
