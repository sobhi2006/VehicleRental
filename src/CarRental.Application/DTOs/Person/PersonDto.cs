namespace CarRental.Application.DTOs.Person;

/// <summary>
/// Data transfer object for Person.
/// </summary>
public record PersonDto
{
    /// <summary>Gets or sets the identifier.</summary>
    public long Id { get; init; }
    /// <summary>Gets or sets the FirstName.</summary>
    public string FirstName { get; init; } = string.Empty;
    /// <summary>Gets or sets the MiddleName.</summary>
    public string? MiddleName { get; init; } = string.Empty;
    /// <summary>Gets or sets the LastName.</summary>
    public string LastName { get; init; } = string.Empty;
    /// <summary>Gets or sets the DateOfBirth.</summary>
    public DateOnly DateOfBirth { get; init; }
    /// <summary>Gets or sets the Email.</summary>
    public string Email { get; init; } = string.Empty;
    /// <summary>Gets or sets the PhoneNumber.</summary>
    public string PhoneNumber { get; init; } = string.Empty;
    /// <summary>Gets or sets the Address.</summary>
    public string Address { get; init; } = string.Empty;
    /// <summary>Gets or sets the creation timestamp.</summary>
    public DateTime CreatedAt { get; init; }
    /// <summary>Gets or sets the last update timestamp.</summary>
    public DateTime? UpdatedAt { get; init; }
}
