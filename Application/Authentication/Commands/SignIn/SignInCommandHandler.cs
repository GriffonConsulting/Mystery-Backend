using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Application.Common.Requests;
using MediatR;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace Application.Authentication.Commands.SignIn
{
    public class SignInCommandHandler : IRequestHandler<SignInCommand, RequestResult<SignInDto>>
    {
        private readonly IAuthentication _authenticationService;

        public SignInCommandHandler(IAuthentication authenticationService)
        {
            _authenticationService = authenticationService;
        }

        public async Task<RequestResult<SignInDto>> Handle(SignInCommand request, CancellationToken cancellationToken)
        {
            var user = await _authenticationService.FindByEmailAsync(request.Email) ?? throw new NotFoundException("user");
            var result = await _authenticationService.CheckPasswordSignInAsync(user, request.Password, false);
            if (!result.Succeeded) throw new HttpRequestException("password");

            var userRoles = await _authenticationService.GetRolesAsync(user);

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

            var token = _authenticationService.GetToken(authClaims);


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
