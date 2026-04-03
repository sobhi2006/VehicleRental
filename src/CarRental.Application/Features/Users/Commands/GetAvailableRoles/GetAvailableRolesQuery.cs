using CarRental.Application.Common;
using MediatR;

namespace CarRental.Application.Features.Users.Commands.GetAvailableRoles;

public sealed record GetAvailableRolesQuery : IRequest<Result<IReadOnlyCollection<string>>>;
