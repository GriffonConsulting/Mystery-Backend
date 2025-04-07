using Application.Common.Interfaces.Repositories;
using Application.Common.Requests;
using MediatR;

namespace Application.Faq.Queries.GetFaq
{
    public class GetFaqQueryHandler : IRequestHandler<GetFaqQuery, RequestResult<GetFaqResult[]>>
    {
        private IFaqRepository _faqRepository { get; }

        public GetFaqQueryHandler(IFaqRepository faqRepository)
        {
            _faqRepository = faqRepository;
        }


        public async Task<RequestResult<GetFaqResult[]>> Handle(GetFaqQuery request, CancellationToken cancellationToken)
        {
            var faqResult = await _faqRepository.GetAllAsync(cancellationToken);

            return new RequestResult<GetFaqResult[]>
            {
                Result = faqResult.Select((f) => new GetFaqResult
                {
                    Question = f.Question,
                    Answer = f.Answer,

                }).ToArray()
            };
        }
    }
}
