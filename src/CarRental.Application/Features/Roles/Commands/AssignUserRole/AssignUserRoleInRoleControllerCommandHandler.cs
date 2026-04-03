using CarRental.Application.Common;
using CarRental.Application.DTOs.Identity;
using CarRental.Application.Interfaces;
using MediatR;

namespace CarRental.Application.Features.Roles.Commands.AssignUserRole;

public sealed class AssignUserRoleInRoleControllerCommandHandler : IRequestHandler<AssignUserRoleInRoleControllerCommand, Result<UserDto>>
{
    private readonly IUserManagementService _service;

    public AssignUserRoleInRoleControllerCommandHandler(IUserManagementService service)
    {
        _service = service;
    }

    public Task<Result<UserDto>> Handle(AssignUserRoleInRoleControllerCommand request, CancellationToken cancellationToken)
    {
        return _service.AssignRoleAsync(request.UserId, request.RoleName, cancellationToken);
    }
}
