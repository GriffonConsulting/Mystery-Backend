using Application.Common.Requests;
using MediatR;
using System.Text.Json.Serialization;

namespace Application.Invoices.Queries.GetInvoices;

public class GetInvoicesQuery : IRequest<RequestResult<GetInvoicesResult[]>>
{
    [JsonIgnore]
    public Guid UserId { get; set; }
}
