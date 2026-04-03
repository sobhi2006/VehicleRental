namespace CarRental.Application.DTOs.Identity;

public sealed class ChangePasswordDto
{
    public string OldPassword { get; init; } = string.Empty;
    public string NewPassword { get; init; } = string.Empty;
    public string ConfirmNewPassword { get; init; } = string.Empty;
}
