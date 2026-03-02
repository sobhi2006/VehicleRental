using CarRental.Domain.Common;
using CarRental.Domain.Enums;

namespace CarRental.Domain.Entities.Vehicles;

/// <summary>
/// Represents the Vehicle entity.
/// </summary>
public class Vehicle : BaseEntity
{
    /// <summary>Gets or sets the MakeId.</summary>
    public long MakeId { get; set; }
    /// <summary>Gets or sets the ModelYear.</summary>
    public DateOnly ModelYear { get; set; }
    /// <summary>Gets or sets the VIN.</summary>
    public string VIN { get; set; } = string.Empty;
    /// <summary>Gets or sets the PlateNumber.</summary>
    public string PlateNumber { get; set; } = string.Empty;
    /// <summary>Gets or sets the CurrentMileage.</summary>
    public decimal CurrentMileage { get; set; }
    /// <summary>Gets or sets the ClassificationId.</summary>
    public long ClassificationId { get; set; }
    /// <summary>Gets or sets the Transmission.</summary>
    public Transmission Transmission { get; set; }
    /// <summary>Gets or sets the FuelType.</summary>
    public FuelType FuelType { get; set; }
    /// <summary>Gets or sets the DoorsNumber.</summary>
    public int DoorsNumber { get; set; }
    /// <summary>Gets or sets the Status.</summary>
    public StatusVehicle Status { get; set; }
    /// <summary>Gets or sets the ImageUrl.</summary>
    public List<string> ImageUrl { get; set; } = [];
    /// <summary>Gets or sets the related Make.</summary>
    public virtual Make Make { get; set; } = null!;
    /// <summary>Gets or sets the related Classification.</summary>
    public virtual Classification Classification { get; set; } = null!;
}
