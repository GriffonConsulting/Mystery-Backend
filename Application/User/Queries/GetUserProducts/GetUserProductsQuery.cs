using Application.Common.Requests;
using MediatR;
using System.Text.Json.Serialization;

namespace Application.User.Queries.GetUserProducts

{
    public class GetUserProductsQuery : IRequest<RequestResult<GetUserProductsDto>>
    {
        [JsonIgnore]
        public required Guid ClientId { get; set; }
    }
}
