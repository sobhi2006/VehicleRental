using CarRental.Application.Common;
using CarRental.Application.DTOs.Identity;
using CarRental.Application.Interfaces;
using MediatR;

namespace CarRental.Application.Features.Users.Commands.AssignUserRole;

public sealed class AssignUserRoleCommandHandler : IRequestHandler<AssignUserRoleCommand, Result<UserDto>>
{
    private readonly IUserManagementService _service;

    public AssignUserRoleCommandHandler(IUserManagementService service)
    {
        _service = service;
    }

    public Task<Result<UserDto>> Handle(AssignUserRoleCommand request, CancellationToken cancellationToken)
    {
        return _service.AssignRoleAsync(request.UserId, request.RoleName, cancellationToken);
    }
}
