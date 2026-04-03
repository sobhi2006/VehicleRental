using CarRental.Application.Common;
using MediatR;

namespace CarRental.Application.Features.Auth.Commands.Logout;

public sealed record LogoutCommand(string Token) : IRequest<Result>;
