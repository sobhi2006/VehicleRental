namespace CarRental.Application.DTOs.Identity;

public sealed class UserDto
{
    public string Id { get; init; } = string.Empty;
    public string FirstName { get; init; } = string.Empty;
    public string LastName { get; init; } = string.Empty;
    public string Email { get; init; } = string.Empty;
    public bool IsActive { get; init; }
    public IList<string> Roles { get; init; } = [];
}
