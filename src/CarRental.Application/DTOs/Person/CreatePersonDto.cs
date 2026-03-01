namespace CarRental.Application.DTOs.Person;

/// <summary>
/// Data transfer object used to create Person.
/// </summary>
public record CreatePersonDto
{
    /// <summary>Gets or sets the FirstName.</summary>
    public string FirstName { get; init; } = string.Empty;
    /// <summary>Gets or sets the MiddleName.</summary>
    public string MiddleName { get; init; } = string.Empty;
    /// <summary>Gets or sets the LastName.</summary>
    public string LastName { get; init; } = string.Empty;
    /// <summary>Gets or sets the NationalNo.</summary>
    public string NationalNo { get; init; } = string.Empty;
    /// <summary>Gets or sets the DateOfBirth.</summary>
    
    public DateOnly DateOfBirth { get; init; }
    /// <summary>Gets or sets the Email.</summary>
    public string Email { get; init; } = string.Empty;
    /// <summary>Gets or sets the PhoneNumber.</summary>
    public string PhoneNumber { get; init; } = string.Empty;
    /// <summary>Gets or sets the Address.</summary>
    public string Address { get; init; } = string.Empty;
}
