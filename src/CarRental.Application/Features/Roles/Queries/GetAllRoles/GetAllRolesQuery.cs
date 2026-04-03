using CarRental.Application.Common;
using CarRental.Application.DTOs.Identity;
using MediatR;

namespace CarRental.Application.Features.Roles.Queries.GetAllRoles;

public sealed record GetAllRolesQuery : IRequest<Result<IReadOnlyCollection<RoleDto>>>;
