using Application.Common.Exceptions;
using Application.Common.Interfaces.Repositories;
using Application.Common.Requests;
using MediatR;

namespace Application.UserProduct.GetProduct
{
    public class GetUserProductQueryHandler : IRequestHandler<GetUserProductQuery, RequestResult<GetUserProductDto>>
    {
        private IUserProductRepository _userProductRepository { get; }

        public GetUserProductQueryHandler(IUserProductRepository userProductRepository)
        {
            _userProductRepository = userProductRepository;
        }


        public async Task<RequestResult<GetUserProductDto>> Handle(GetUserProductQuery request, CancellationToken cancellationToken)
        {
            //var userPproduct = await _userProductRepository.GetByProductCodeAsync(request.ProductCode, cancellationToken);
            //if (userPproduct == null) throw new NotFoundException("userProductNotFound");

            return new RequestResult<GetUserProductDto>
            {
                //Result = new GetUserProductDto
                //{
                //}
            };
        }
    }
}
