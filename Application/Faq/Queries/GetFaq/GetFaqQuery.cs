using Application.Common.Requests;
using MediatR;

namespace Application.Faq.Queries.GetFaq;

public class GetFaqQuery : IRequest<RequestResult<GetFaqDto[]>>
{
    public required string Lang { get; set; }
}
