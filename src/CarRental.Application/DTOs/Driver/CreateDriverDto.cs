namespace CarRental.Application.DTOs.Driver;

/// <summary>
/// Data transfer object used to create Driver.
/// </summary>
public record CreateDriverDto
{
    /// <summary>Gets or sets the PersonId.</summary>
    public long PersonId { get; init; }
    /// <summary>Gets or sets the DriverLicenseNumber.</summary>
    public string DriverLicenseNumber { get; init; } = string.Empty;
    /// <summary>Gets or sets the DriverLicenseExpiryDate.</summary>
    public DateOnly DriverLicenseExpiryDate { get; init; }
}
