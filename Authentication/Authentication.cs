﻿using Application.Common.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace EmailSender
{
    public class Authentication : IAuthentication
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly IConfiguration _configuration;

        public Authentication(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager, IConfiguration configuration)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _configuration = configuration;
        }
        public async Task<string> GenerateChangeEmailTokenAsync(IdentityUser identityUser, string newEmail)
        {
            return await _userManager.GenerateChangeEmailTokenAsync(identityUser, newEmail);
        }

        public async Task<IdentityResult> ChangeEmailAsync(IdentityUser identityUser, string newEmail, string token)
        {
            return await _userManager.ChangeEmailAsync(identityUser, newEmail, token);
        }

        public async Task<IdentityUser?> FindByEmailAsync(string email)
        {
            return await _userManager.FindByEmailAsync(email);
        }

        public async Task<IdentityResult> ConfirmEmailAsync(IdentityUser identityUser, string token)
        {
            return await _userManager.ConfirmEmailAsync(identityUser, token);
        }

        public async Task<string> GeneratePasswordResetTokenAsync(IdentityUser identityUser)
        {
            return await _userManager.GeneratePasswordResetTokenAsync(identityUser);
        }

        public async Task<SignInResult> CheckPasswordSignInAsync(IdentityUser identityUser, string password, bool lockOUtOnFailure)
        {
            return await _signInManager.CheckPasswordSignInAsync(identityUser, password, lockOUtOnFailure);
        }

        public async Task<IList<string>> GetRolesAsync(IdentityUser identityUser)
        {
            return await _userManager.GetRolesAsync(identityUser);
        }
        public async Task<IdentityResult> CreateAsync(IdentityUser identityUser, string password)
        {
            return await _userManager.CreateAsync(identityUser, password);
        }
        public async Task<IdentityResult> AddToRoleAsync(IdentityUser identityUser, string role)
        {
            return await _userManager.AddToRoleAsync(identityUser, role);
        }
        public async Task<string> GetUserIdAsync(IdentityUser identityUser)
        {
            return await _userManager.GetUserIdAsync(identityUser);
        }
        public async Task<string> GenerateEmailConfirmationTokenAsync(IdentityUser identityUser)
        {
            return await _userManager.GenerateEmailConfirmationTokenAsync(identityUser);
        }

        public async Task<IdentityResult> ResetPasswordAsync(IdentityUser identityUser, string token, string newPassword)
        {
            return await _userManager.ResetPasswordAsync(identityUser, token, newPassword);
        }
        public JwtSecurityToken GetToken(List<Claim> authClaims)
        {
            var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]!));

            return new JwtSecurityToken(
                issuer: _configuration["JWT:ValidIssuer"],
                audience: _configuration["JWT:ValidAudience"],
                expires: DateTime.Now.AddHours(3),
                claims: authClaims,
                signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
                );
        }
    }
}
