using CarRental.Application.Common;
using CarRental.Application.DTOs.Identity;
using MediatR;

namespace CarRental.Application.Features.Users.Queries.GetUserById;

public sealed record GetUserByIdQuery(string UserId) : IRequest<Result<UserDto>>;
