using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Application.Common.Requests;
using MediatR;
using System.ComponentModel.DataAnnotations;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace Application.Authentication.Queries.SignIn
{
    public class SignInQueryHandler : IRequestHandler<SignInQuery, RequestResult<SignInDto>>
    {
        private readonly IAuthentication _authentication;

        public SignInQueryHandler(IAuthentication authentication)
        {
            _authentication = authentication;
        }

        public async Task<RequestResult<SignInDto>> Handle(SignInQuery request, CancellationToken cancellationToken)
        {
            var user = await _authentication.FindByEmailAsync(request.Email) ?? throw new NotFoundException("userNotFound");
            var result = await _authentication.CheckPasswordSignInAsync(user, request.Password, false);
            if (!result.Succeeded) throw new ValidationException("passwordValidationError");

            var userRoles = await _authentication.GetRolesAsync(user);

            var authClaims = new List<Claim>
                {
                    new(ClaimTypes.NameIdentifier, user.Id),
                    new(ClaimTypes.Email, user.Email),
                    new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                };

            foreach (var userRole in userRoles)
            {
                authClaims.Add(new Claim(ClaimTypes.Role, userRole));
            }

            var token = _authentication.GetToken(authClaims);


            return new RequestResult<SignInDto>
            {
                Result = new SignInDto
                {
                    Token = new JwtSecurityTokenHandler().WriteToken(token),
                    ExpirationDate = token.ValidTo
                },


            };
        }

    }
}
