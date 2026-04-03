using CarRental.Application.Common;
using CarRental.Application.DTOs.Identity;
using MediatR;

namespace CarRental.Application.Features.Roles.Commands.UpdateRole;

public sealed record UpdateRoleCommand(string RoleId, string Name) : IRequest<Result<RoleDto>>;

