﻿using Microsoft.AspNetCore.Identity;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace Application.Common.Interfaces
{
    public interface IAuthentication
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
        public Task<IdentityResult> ResetPasswordAsync(IdentityUser identityUser, string token, string newPassword);
        public Task<string> GenerateChangeEmailTokenAsync(IdentityUser identityUser, string email);
        public Task<IdentityResult> ChangeEmailAsync(IdentityUser identityUser, string newEmail, string token);
    }
}
