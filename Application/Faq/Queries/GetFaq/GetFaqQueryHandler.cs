using Application.Common.Interfaces.Repositories;
using Application.Common.Requests;
using MediatR;

namespace Application.Faq.Queries.GetFaq
{
    public class GetFaqQueryHandler : IRequestHandler<GetFaqQuery, RequestResult<GetFaqDto[]>>
    {
        private IFaqRepository _faqRepository { get; }

        public GetFaqQueryHandler(IFaqRepository faqRepository)
        {
            _faqRepository = faqRepository;
        }


        public async Task<RequestResult<GetFaqDto[]>> Handle(GetFaqQuery request, CancellationToken cancellationToken)
        {
            var faqResult = await _faqRepository.GetByLangAsync(request.Lang, cancellationToken);

            return new RequestResult<GetFaqDto[]>
            {
                Result = faqResult.Select((f) => new GetFaqDto
                {
                    Question = f.Question,
                    Answer = f.Answer,

                }).ToArray()
            };
        }
    }
}
