using CarRental.Application.Common;
using CarRental.Application.DTOs.Identity;
using CarRental.Application.Interfaces;
using MediatR;

namespace CarRental.Application.Features.Roles.Commands.UnassignUserRole;

public sealed class UnassignUserRoleInRoleControllerCommandHandler : IRequestHandler<UnassignUserRoleInRoleControllerCommand, Result<UserDto>>
{
    private readonly IUserManagementService _service;

    public UnassignUserRoleInRoleControllerCommandHandler(IUserManagementService service)
    {
        _service = service;
    }

    public Task<Result<UserDto>> Handle(UnassignUserRoleInRoleControllerCommand request, CancellationToken cancellationToken)
    {
        return _service.UnassignRoleAsync(request.UserId, request.RoleName, cancellationToken);
    }
}
