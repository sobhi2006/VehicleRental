using CarRental.Application.Common;
using CarRental.Application.Interfaces;
using MediatR;

namespace CarRental.Application.Features.Users.Commands.DeleteUser;

public sealed class DeleteUserCommandHandler : IRequestHandler<DeleteUserCommand, Result>
{
    private readonly IUserManagementService _service;

    public DeleteUserCommandHandler(IUserManagementService service)
    {
        _service = service;
    }

    public Task<Result> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
    {
        return _service.DeleteAsync(request.UserId, cancellationToken);
    }
}
