using CarRental.Application.Common;
using MediatR;

namespace CarRental.Application.Features.Roles.Commands.DeleteRole;

public sealed record DeleteRoleCommand(string RoleId) : IRequest<Result>;
