using Application.Common.Requests;
using Application.User.Queries.GetUser;
using MediatR;

namespace Application.Product.Queries.GetProduct
{
    public class GetUserQuery : IRequest<RequestResult<GetUserDto>>
    {
        public required Guid ClientId { get; set; }
    }
}
