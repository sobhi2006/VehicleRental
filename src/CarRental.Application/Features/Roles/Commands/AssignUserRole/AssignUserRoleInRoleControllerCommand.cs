using CarRental.Application.Common;
using CarRental.Application.DTOs.Identity;
using MediatR;

namespace CarRental.Application.Features.Roles.Commands.AssignUserRole;

public sealed record AssignUserRoleInRoleControllerCommand(string UserId, string RoleName) : IRequest<Result<UserDto>>;
