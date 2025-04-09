using Application.Common.Requests;
using MediatR;
using System.Text.Json.Serialization;

namespace Application.User.Queries.GetUser
{
    public class GetUserQuery : IRequest<RequestResult<GetUserDto>>
    {
        [JsonIgnore]
        public required Guid ClientId { get; set; }
        [JsonIgnore]
        public required string Email { get; set; }
    }
}
