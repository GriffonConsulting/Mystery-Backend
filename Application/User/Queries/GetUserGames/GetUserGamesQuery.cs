using Application.Common.Requests;
using MediatR;
using System.Text.Json.Serialization;

namespace Application.User.Queries.GetUserGames
{
    public class GetUserGamesQuery : IRequest<RequestResult<GetUserGamesDto>>
    {
        [JsonIgnore]
        public required Guid ClientId { get; set; }
    }
}
