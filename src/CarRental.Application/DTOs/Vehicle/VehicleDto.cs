using CarRental.Domain.Enums;

namespace CarRental.Application.DTOs.Vehicle;

/// <summary>
/// Data transfer object for Vehicle.
/// </summary>
public record VehicleDto
{
    /// <summary>Gets or sets the identifier.</summary>
    public long Id { get; init; }
    /// <summary>Gets or sets the MakeId.</summary>
    public long MakeId { get; init; }
    /// <summary>Gets or sets the ModelYear.</summary>
    public DateOnly ModelYear { get; init; }
    /// <summary>Gets or sets the VIN.</summary>
    public string VIN { get; init; } = string.Empty;
    /// <summary>Gets or sets the PlateNumber.</summary>
    public string PlateNumber { get; init; } = string.Empty;
    /// <summary>Gets or sets the CurrentMileage.</summary>
    public decimal CurrentMileage { get; init; }
    /// <summary>Gets or sets the ClassificationId.</summary>
    public long ClassificationId { get; init; }
    /// <summary>Gets or sets the Transmission.</summary>
    public Transmission Transmission { get; init; }
    /// <summary>Gets or sets the FuelType.</summary>
    public FuelType FuelType { get; init; }
    /// <summary>Gets or sets the DoorsNumber.</summary>
    public int DoorsNumber { get; init; }
    /// <summary>Gets or sets the Status.</summary>
    public StatusVehicle Status { get; init; }
    /// <summary>Gets or sets the ImageUrl.</summary>
    public List<string> ImageUrl { get; init; } = [];
    /// <summary>Gets or sets the creation timestamp.</summary>
    public DateTime CreatedAt { get; init; }
    /// <summary>Gets or sets the last update timestamp.</summary>
    public DateTime? UpdatedAt { get; init; }
}
