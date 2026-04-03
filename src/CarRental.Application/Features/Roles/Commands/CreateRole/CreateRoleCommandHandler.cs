using CarRental.Application.Common;
using CarRental.Application.DTOs.Identity;
using CarRental.Application.Interfaces;
using MediatR;

namespace CarRental.Application.Features.Roles.Commands.CreateRole;

public sealed class CreateRoleCommandHandler : IRequestHandler<CreateRoleCommand, Result<RoleDto>>
{
    private readonly IRoleManagementService _service;

    public CreateRoleCommandHandler(IRoleManagementService service)
    {
        _service = service;
    }

    public Task<Result<RoleDto>> Handle(CreateRoleCommand request, CancellationToken cancellationToken)
    {
        return _service.CreateAsync(request.Name, cancellationToken);
    }
}
