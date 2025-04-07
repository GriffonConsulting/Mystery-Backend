using Application.Common.Requests;
using MediatR;
using System.Text.Json.Serialization;

namespace Application.Faq.Queries.GetFaq;

public class GetFaqQuery : IRequest<RequestResult<GetFaqDto[]>>
{
    [JsonIgnore]
    public Guid UserId { get; set; }
}
