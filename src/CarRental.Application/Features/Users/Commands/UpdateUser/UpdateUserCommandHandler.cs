using CarRental.Application.Common;
using CarRental.Application.DTOs.Identity;
using CarRental.Application.Interfaces;
using MediatR;

namespace CarRental.Application.Features.Users.Commands.UpdateUser;

public sealed class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand, Result<UserDto>>
{
    private readonly IUserManagementService _service;

    public UpdateUserCommandHandler(IUserManagementService service)
    {
        _service = service;
    }

    public Task<Result<UserDto>> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
    {
        var dto = new UpdateUserDto
        {
            FirstName = request.FirstName,
            LastName = request.LastName,
            Email = request.Email,
            IsActive = request.IsActive
        };

        return _service.UpdateAsync(request.UserId, dto, cancellationToken);
    }
}
