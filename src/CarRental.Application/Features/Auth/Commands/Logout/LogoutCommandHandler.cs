using CarRental.Application.Common;
using CarRental.Application.DTOs.Identity;
using CarRental.Application.Interfaces;
using MediatR;

namespace CarRental.Application.Features.Auth.Commands.Logout;

public sealed class LogoutCommandHandler : IRequestHandler<LogoutCommand, Result>
{
    private readonly IAuthService _authService;

    public LogoutCommandHandler(IAuthService authService)
    {
        _authService = authService;
    }

    public Task<Result> Handle(LogoutCommand request, CancellationToken cancellationToken)
    {
        return _authService.LogoutAsync(new LogoutRequestDto { Token = request.Token }, cancellationToken);
    }
}
