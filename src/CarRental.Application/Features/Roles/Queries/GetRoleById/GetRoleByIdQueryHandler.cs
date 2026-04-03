using CarRental.Application.Common;
using CarRental.Application.DTOs.Identity;
using CarRental.Application.Interfaces;
using MediatR;

namespace CarRental.Application.Features.Roles.Queries.GetRoleById;

public sealed class GetRoleByIdQueryHandler : IRequestHandler<GetRoleByIdQuery, Result<RoleDto>>
{
    private readonly IRoleManagementService _service;

    public GetRoleByIdQueryHandler(IRoleManagementService service)
    {
        _service = service;
    }

    public Task<Result<RoleDto>> Handle(GetRoleByIdQuery request, CancellationToken cancellationToken)
    {
        return _service.GetByIdAsync(request.RoleId, cancellationToken);
    }
}
