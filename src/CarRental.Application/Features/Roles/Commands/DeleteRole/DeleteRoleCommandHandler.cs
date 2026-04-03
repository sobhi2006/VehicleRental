using CarRental.Application.Common;
using CarRental.Application.Interfaces;
using MediatR;

namespace CarRental.Application.Features.Roles.Commands.DeleteRole;

public sealed class DeleteRoleCommandHandler : IRequestHandler<DeleteRoleCommand, Result>
{
    private readonly IRoleManagementService _service;

    public DeleteRoleCommandHandler(IRoleManagementService service)
    {
        _service = service;
    }

    public Task<Result> Handle(DeleteRoleCommand request, CancellationToken cancellationToken)
    {
        return _service.DeleteAsync(request.RoleId, cancellationToken);
    }
}
