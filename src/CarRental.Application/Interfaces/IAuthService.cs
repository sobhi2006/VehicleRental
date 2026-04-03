using CarRental.Application.Common;
using CarRental.Application.DTOs.Identity;

namespace CarRental.Application.Interfaces;

public interface IAuthService
{
    Task<Result<AuthResponseDto>> LoginAsync(LoginRequestDto request, CancellationToken cancellationToken);
    Task<Result<AuthResponseDto>> RefreshTokenAsync(RefreshTokenRequestDto request, CancellationToken cancellationToken);
    Task<Result> LogoutAsync(LogoutRequestDto request, CancellationToken cancellationToken);
}
