using CarRental.Application.Common;
using CarRental.Application.DTOs.Identity;
using MediatR;

namespace CarRental.Application.Features.Roles.Commands.CreateRole;

public sealed record CreateRoleCommand(string Name) : IRequest<Result<RoleDto>>;
