using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Application.Common.Interfaces.Repositories;
using Application.Common.Requests;
using MediatR;

namespace Application.User.Commands.UpdateUser;

public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand, RequestResult>
{
    private readonly IAuthentication _authentication;
    private readonly IUserRepository _userRepository;

    public UpdateUserCommandHandler(IUserRepository userRepository, IAuthentication authentication)
    {
        _userRepository = userRepository;
        _authentication = authentication;
    }

    public async Task<RequestResult> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetById(request.UserId, cancellationToken) ?? throw new NotFoundException("userNotFound");
        var authenticationUser = await _authentication.FindByEmailAsync(request.OldEmail) ?? throw new NotFoundException("oldEmailNotFound"); ;

        if (request.Password != null)
        {
            var token = await _authentication.GeneratePasswordResetTokenAsync(authenticationUser);
            await _authentication.ResetPasswordAsync(authenticationUser, token, request.Password);
        }

        if (request.NewEmail != request.OldEmail)
        {
            var token = await _authentication.GenerateChangeEmailTokenAsync(authenticationUser, request.NewEmail); 
            await _authentication.ChangeEmailAsync(authenticationUser, token, request.NewEmail);
        }

        user.Firstname = request.Firstname;
        user.Address = request.Address;
        user.ComplementaryAddress = request.ComplementaryAddress;
        user.Lastname = request.Lastname;
        user.Country = request.Country;
        user.MarketingEmail = request.MarketingEmail;
        user.City = request.City;
        user.PostalCode = request.PostalCode;

        await _userRepository.UpdateEntityAsync(user, cancellationToken);

        return new RequestResult
        {
        };
    }
}