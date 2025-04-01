using Application.Common.Requests;
using Application.User.Queries.GetUser;
using MediatR;
using System.Text.Json.Serialization;

namespace Application.Product.Queries.GetProduct
{
    public class GetUserQuery : IRequest<RequestResult<GetUserDto>>
    {
        [JsonIgnore]
        public required Guid ClientId { get; set; }
        [JsonIgnore]
        public required string Email { get; set; }
    }
}
