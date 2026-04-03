using CarRental.Application.Common;
using CarRental.Application.DTOs.Identity;
using MediatR;

namespace CarRental.Application.Features.Users.Commands.SetUserActive;

public sealed record SetUserActiveCommand(string UserId, bool IsActive) : IRequest<Result<UserDto>>;
