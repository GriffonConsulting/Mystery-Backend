using Application.Common.Exceptions;
using Application.Common.Requests;
using Application.User.Queries.GetUser;
using Database.Queries;
using MediatR;

namespace Application.Product.Queries.GetProduct;

public class GetUserQueryHandler : IRequestHandler<GetUserQuery, RequestResult<GetUserDto>>
{
    private DbUserQueries _dbUserQueries { get; }

    public GetUserQueryHandler(DbUserQueries dbUserQueries)
    {
        _dbUserQueries = dbUserQueries;
    }

    public async Task<RequestResult<GetUserDto>> Handle(GetUserQuery request, CancellationToken cancellationToken)
    {
        var user = await _dbUserQueries.GetById(request.ClientId, cancellationToken);
        if (user == null) throw new NotFoundException("user");

        return new RequestResult<GetUserDto>
        {
            Result = new GetUserDto
            {
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