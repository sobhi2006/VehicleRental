using CarRental.Domain.Enums;
using Microsoft.AspNetCore.Http;

namespace CarRental.Application.DTOs.Vehicle;

/// <summary>
/// Data transfer object used to update Vehicle.
/// </summary>
public record UpdateVehicleDto
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
    public float CurrentMileage { get; init; }
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
    /// <summary>Gets or sets the Images.</summary>
    public List<IFormFile> Images { get; init; } = [];
}
