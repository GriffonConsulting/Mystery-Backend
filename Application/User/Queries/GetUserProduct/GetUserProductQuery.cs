using Application.Common.Requests;
using MediatR;
using System.Text.Json.Serialization;

namespace Application.User.Queries.GetUserProduct
{
    public class GetUserProductQuery : IRequest<RequestResult<GetUserProductDto>>
    {
        [JsonIgnore]
        public required Guid UserProductId { get; set; }
    }
}
