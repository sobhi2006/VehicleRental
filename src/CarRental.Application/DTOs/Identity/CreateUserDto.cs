namespace CarRental.Application.DTOs.Identity;

public sealed class CreateUserDto
{
    public string FirstName { get; init; } = string.Empty;
    public string LastName { get; init; } = string.Empty;
    public string Email { get; init; } = string.Empty;
    public string Password { get; init; } = string.Empty;
    public bool IsActive { get; init; } = true;
    public IReadOnlyCollection<string> Roles { get; init; } = [];
}
