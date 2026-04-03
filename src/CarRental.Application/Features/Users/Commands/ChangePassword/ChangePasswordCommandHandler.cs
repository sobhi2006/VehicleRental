using CarRental.Application.Common;
using CarRental.Application.Interfaces;
using MediatR;

namespace CarRental.Application.Features.Users.Commands.ChangePassword;

public sealed class ChangePasswordCommandHandler : IRequestHandler<ChangePasswordCommand, Result>
{
    private readonly IUserManagementService _service;

    public ChangePasswordCommandHandler(IUserManagementService service)
    {
        _service = service;
    }

    public Task<Result> Handle(ChangePasswordCommand request, CancellationToken cancellationToken)
    {
        return _service.ChangePasswordAsync(request.UserId, request.OldPassword, request.NewPassword, cancellationToken);
    }
}
