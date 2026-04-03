using CarRental.Application.Common;
using CarRental.Application.DTOs.Identity;
using MediatR;

namespace CarRental.Application.Features.Roles.Commands.UnassignUserRole;

public sealed record UnassignUserRoleInRoleControllerCommand(string UserId, string RoleName) : IRequest<Result<UserDto>>;
