using CarRental.Application.Common;
using CarRental.Application.DTOs.Identity;
using CarRental.Application.Interfaces;
using MediatR;

namespace CarRental.Application.Features.Roles.Commands.UpdateRole;

public sealed class UpdateRoleCommandHandler : IRequestHandler<UpdateRoleCommand, Result<RoleDto>>
{
    private readonly IRoleManagementService _service;

    public UpdateRoleCommandHandler(IRoleManagementService service)
    {
        _service = service;
    }

    public Task<Result<RoleDto>> Handle(UpdateRoleCommand request, CancellationToken cancellationToken)
    {
        return _service.UpdateAsync(request.RoleId, request.Name, cancellationToken);
    }
}
