using CarRental.Application.Common;
using CarRental.Application.DTOs.Identity;
using CarRental.Application.Interfaces;
using MediatR;

namespace CarRental.Application.Features.Users.Queries.GetUserById;

public sealed class GetUserByIdQueryHandler : IRequestHandler<GetUserByIdQuery, Result<UserDto>>
{
    private readonly IUserManagementService _service;

    public GetUserByIdQueryHandler(IUserManagementService service)
    {
        _service = service;
    }

    public Task<Result<UserDto>> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
    {
        return _service.GetByIdAsync(request.UserId, cancellationToken);
    }
}
