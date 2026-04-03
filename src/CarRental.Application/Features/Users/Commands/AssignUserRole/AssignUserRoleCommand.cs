using CarRental.Application.Common;
using CarRental.Application.DTOs.Identity;
using MediatR;

namespace CarRental.Application.Features.Users.Commands.AssignUserRole;

public sealed record AssignUserRoleCommand(string UserId, string RoleName) : IRequest<Result<UserDto>>;
