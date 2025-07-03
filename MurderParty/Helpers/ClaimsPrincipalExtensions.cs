using System.Security.Claims;

namespace MurderParty.Api.Helpers
{
    public static class ClaimsPrincipalExtensions
    {
        public static string GetEmail(this ClaimsPrincipal user)
        {
            return user.FindFirstValue(ClaimTypes.Email) ?? throw new NullReferenceException(ClaimTypes.Email);
        }

        public static Guid GetUserId(this ClaimsPrincipal user)
        {
            var nameIdentifier = user.FindFirstValue(ClaimTypes.NameIdentifier) ?? throw new NullReferenceException(ClaimTypes.NameIdentifier);
            return Guid.Parse(nameIdentifier);
        }
    }
}
