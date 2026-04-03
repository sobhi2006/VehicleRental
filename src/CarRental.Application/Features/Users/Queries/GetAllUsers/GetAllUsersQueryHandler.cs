using CarRental.Application.Common;
using CarRental.Application.DTOs.Identity;
using CarRental.Application.Interfaces;
using MediatR;

namespace CarRental.Application.Features.Users.Queries.GetAllUsers;

public sealed class GetAllUsersQueryHandler : IRequestHandler<GetAllUsersQuery, Result<PaginatedList<UserDto>>>
{
    private readonly IUserManagementService _service;

    public GetAllUsersQueryHandler(IUserManagementService service)
    {
        _service = service;
    }

    public Task<Result<PaginatedList<UserDto>>> Handle(GetAllUsersQuery request, CancellationToken cancellationToken)
    {
        return _service.GetAllAsync(request.PageNumber, request.PageSize, cancellationToken);
    }
}
