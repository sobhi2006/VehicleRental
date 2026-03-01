using CarRental.Domain.Common;

namespace CarRental.Domain.Entities;

/// <summary>
/// Represents the Person entity.
/// </summary>
public class Person : BaseEntity
{
    /// <summary>Gets or sets the FirstName.</summary>
    public string FirstName { get; set; } = string.Empty;
    /// <summary>Gets or sets the MiddleName.</summary>
    public string? MiddleName { get; set; }
    /// <summary>Gets or sets the LastName.</summary>
    public string LastName { get; set; } = string.Empty;
    /// <summary>Gets or sets the NationalNo.</summary>
    public string NationalNo { get; set; } = string.Empty;
    /// <summary>Gets or sets the DateOfBirth.</summary>
    public DateOnly DateOfBirth { get; set; }
    /// <summary>Gets or sets the Email.</summary>
    public string Email { get; set; } = string.Empty;
    /// <summary>Gets or sets the PhoneNumber.</summary>
    public string PhoneNumber { get; set; } = string.Empty;
    /// <summary>Gets or sets the Address.</summary>
    public string Address { get; set; } = string.Empty;
}
