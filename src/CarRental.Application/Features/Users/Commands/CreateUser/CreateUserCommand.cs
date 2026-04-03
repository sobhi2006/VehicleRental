using CarRental.Application.Common;
using CarRental.Application.DTOs.Identity;
using MediatR;

namespace CarRental.Application.Features.Users.Commands.CreateUser;

public sealed record CreateUserCommand : IRequest<Result<UserDto>>
{
    public string FirstName { get; init; } = string.Empty;
    public string LastName { get; init; } = string.Empty;
    public string Email { get; init; } = string.Empty;
    public string Password { get; init; } = string.Empty;
    public bool IsActive { get; init; } = true;
    public IReadOnlyCollection<string> Roles { get; init; } = [];
}
