using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace MurderParty.Api.Helpers
{
    public static class HttpRequestExtensions
    {
        public static Guid UserId(this HttpRequest request)
        {
            var access_token = request.Cookies["access_token"];
            var handler = new JwtSecurityTokenHandler();
            var jsonToken = handler.ReadToken(access_token) as JwtSecurityToken;
            var userId = jsonToken.Claims.First(claim => claim.Type == ClaimTypes.NameIdentifier).Value;

            return Guid.Parse(userId);
        }
        public static string Email(this HttpRequest request)
        {
            var access_token = request.Cookies["access_token"];
            var handler = new JwtSecurityTokenHandler();
            var jsonToken = handler.ReadToken(access_token) as JwtSecurityToken;
            var email = jsonToken.Claims.First(claim => claim.Type == ClaimTypes.Email).Value;

            return email;
        }
    }
}
