namespace CarRental.Application.DTOs.Identity;

public sealed class RefreshTokenRequestDto
{
    public string RefreshToken { get; init; } = string.Empty;
}
