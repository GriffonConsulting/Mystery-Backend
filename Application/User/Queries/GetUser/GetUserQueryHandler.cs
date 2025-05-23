﻿using Application.Common.Exceptions;
using Application.Common.Interfaces.Repositories;
using Application.Common.Requests;
using MediatR;

namespace Application.User.Queries.GetUser;

public class GetUserQueryHandler : IRequestHandler<GetUserQuery, RequestResult<GetUserDto>>
{
    private IUserRepository _userRepository { get; }

    public GetUserQueryHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<RequestResult<GetUserDto>> Handle(GetUserQuery request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetById(request.ClientId, cancellationToken);
        if (user == null) throw new NotFoundException("userNotFound");

        return new RequestResult<GetUserDto>
        {
            Result = new GetUserDto
            {
                Email = request.Email,
                Firstname = user.Firstname,
                Lastname = user.Lastname,
                Address = user.Address,
                ComplementaryAddress = user.ComplementaryAddress,
                PostalCode = user.PostalCode,
                City = user.City,
                Country = user.Country,
                MarketingEmail = user.MarketingEmail,
            }
        };
    }
}