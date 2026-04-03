using CarRental.Application.Common;
using CarRental.Application.DTOs.Identity;
using CarRental.Application.Interfaces;
using MediatR;

namespace CarRental.Application.Features.Roles.Queries.GetAllRoles;

public sealed class GetAllRolesQueryHandler : IRequestHandler<GetAllRolesQuery, Result<IReadOnlyCollection<RoleDto>>>
{
    private readonly IRoleManagementService _service;

    public GetAllRolesQueryHandler(IRoleManagementService service)
    {
        _service = service;
    }

    public Task<Result<IReadOnlyCollection<RoleDto>>> Handle(GetAllRolesQuery request, CancellationToken cancellationToken)
    {
        return _service.GetAllAsync(cancellationToken);
    }
}
