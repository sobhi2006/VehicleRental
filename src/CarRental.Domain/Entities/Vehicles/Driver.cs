using CarRental.Domain.Common;

namespace CarRental.Domain.Entities.Vehicles;

/// <summary>
/// Represents the Driver entity.
/// </summary>
public class Driver : BaseEntity
{
    /// <summary>Gets or sets the PersonId.</summary>
    public long PersonId { get; set; }
    /// <summary>Gets or sets the DriverLicenseNumber.</summary>
    public string DriverLicenseNumber { get; set; } = string.Empty;
    /// <summary>Gets or sets the DriverLicenseExpiryDate.</summary>
    public DateOnly DriverLicenseExpiryDate { get; set; }
    /// <summary>Gets or sets the related Person.</summary>
    public virtual Person Person { get; set; } = null!;
}
