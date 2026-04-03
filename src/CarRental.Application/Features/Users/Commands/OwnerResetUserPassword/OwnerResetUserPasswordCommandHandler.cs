using CarRental.Application.Common;
using CarRental.Application.Interfaces;
using MediatR;

namespace CarRental.Application.Features.Users.Commands.OwnerResetUserPassword;

public sealed class OwnerResetUserPasswordCommandHandler : IRequestHandler<OwnerResetUserPasswordCommand, Result>
{
    private readonly IUserManagementService _service;

    public OwnerResetUserPasswordCommandHandler(IUserManagementService service)
    {
        _service = service;
    }

    public Task<Result> Handle(OwnerResetUserPasswordCommand request, CancellationToken cancellationToken)
    {
        return _service.ResetPasswordAsync(request.UserId, request.NewPassword, cancellationToken);
    }
}
