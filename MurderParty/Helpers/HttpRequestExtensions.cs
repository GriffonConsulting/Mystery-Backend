using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace MurderParty.Helpers
{
    public static class HttpRequestExtensions
    {
        public static Guid UserId(this HttpRequest request)
        {
            var authHeader = request.Headers.Authorization.FirstOrDefault();
            var handler = new JwtSecurityTokenHandler();
            var token = authHeader.Substring("Bearer ".Length);
            var jsonToken = handler.ReadToken(token) as JwtSecurityToken;
            var userId = jsonToken.Claims.First(claim => claim.Type == ClaimTypes.NameIdentifier).Value;

            return Guid.Parse(userId);
        }
        public static string Email(this HttpRequest request)
        {
            var authHeader = request.Headers.Authorization.FirstOrDefault();
            var handler = new JwtSecurityTokenHandler();
            var token = authHeader.Substring("Bearer ".Length);
            var jsonToken = handler.ReadToken(token) as JwtSecurityToken;
            var email = jsonToken.Claims.First(claim => claim.Type == ClaimTypes.Email).Value;

            return email;
        }
    }
}
