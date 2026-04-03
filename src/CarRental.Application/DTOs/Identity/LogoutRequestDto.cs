namespace CarRental.Application.DTOs.Identity;

public sealed class LogoutRequestDto
{
    public string Token { get; init; } = string.Empty;
}
