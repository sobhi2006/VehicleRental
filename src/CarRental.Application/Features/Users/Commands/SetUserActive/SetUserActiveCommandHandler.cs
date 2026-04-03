using CarRental.Application.Common;
using CarRental.Application.DTOs.Identity;
using CarRental.Application.Interfaces;
using MediatR;

namespace CarRental.Application.Features.Users.Commands.SetUserActive;

public sealed class SetUserActiveCommandHandler : IRequestHandler<SetUserActiveCommand, Result<UserDto>>
{
    private readonly IUserManagementService _service;

    public SetUserActiveCommandHandler(IUserManagementService service)
    {
        _service = service;
    }

    public Task<Result<UserDto>> Handle(SetUserActiveCommand request, CancellationToken cancellationToken)
    {
        return _service.SetActiveAsync(request.UserId, request.IsActive, cancellationToken);
    }
}
