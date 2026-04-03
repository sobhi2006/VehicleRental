using CarRental.Application.Common;
using CarRental.Application.Interfaces;
using MediatR;

namespace CarRental.Application.Features.Users.Commands.GetAvailableRoles;

public sealed class GetAvailableRolesQueryHandler : IRequestHandler<GetAvailableRolesQuery, Result<IReadOnlyCollection<string>>>
{
    private readonly IUserManagementService _service;

    public GetAvailableRolesQueryHandler(IUserManagementService service)
    {
        _service = service;
    }

    public Task<Result<IReadOnlyCollection<string>>> Handle(GetAvailableRolesQuery request, CancellationToken cancellationToken)
    {
        return _service.GetAvailableRolesAsync(cancellationToken);
    }
}
