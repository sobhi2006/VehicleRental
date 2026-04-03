using CarRental.Application.Common;
using CarRental.Application.DTOs.Identity;
using MediatR;

namespace CarRental.Application.Features.Users.Queries.GetCurrentUser;

public sealed record GetCurrentUserQuery(string UserId) : IRequest<Result<UserDto>>;
