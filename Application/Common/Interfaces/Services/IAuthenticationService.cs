using Microsoft.AspNetCore.Identity;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace Application.Common.Interfaces.Services
{
    public interface IAuthenticationService
    {
        public Task<IdentityUser?> FindByEmailAsync(string email);
        public Task<IdentityResult> ConfirmEmailAsync(IdentityUser identityUser, string token);
        public Task<string> GeneratePasswordResetTokenAsync(IdentityUser identityUser);
        public Task<SignInResult> CheckPasswordSignInAsync(IdentityUser identityUser, string password, bool lockOUtOnFailure);
        public Task<IList<string>> GetRolesAsync(IdentityUser identityUser);
        public Task<IdentityResult> CreateAsync(IdentityUser identityUser, string password);
        public Task<IdentityResult> AddToRoleAsync(IdentityUser identityUser, string role);
        public Task<string> GetUserIdAsync(IdentityUser identityUser);
        public Task<string> GenerateEmailConfirmationTokenAsync(IdentityUser identityUser);
        public JwtSecurityToken GetToken(List<Claim> authClaims);
    }
}
