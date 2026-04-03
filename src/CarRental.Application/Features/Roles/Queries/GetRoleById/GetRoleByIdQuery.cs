using CarRental.Application.Common;
using CarRental.Application.DTOs.Identity;
using MediatR;

namespace CarRental.Application.Features.Roles.Queries.GetRoleById;

public sealed record GetRoleByIdQuery(string RoleId) : IRequest<Result<RoleDto>>;
