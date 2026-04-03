using CarRental.Application.Common;
using MediatR;

namespace CarRental.Application.Features.Users.Commands.DeleteUser;

public sealed record DeleteUserCommand(string UserId) : IRequest<Result>;
