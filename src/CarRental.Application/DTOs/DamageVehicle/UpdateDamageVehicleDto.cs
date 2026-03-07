using CarRental.Domain.Enums;
using Microsoft.AspNetCore.Http;

namespace CarRental.Application.DTOs.DamageVehicle;

/// <summary>
/// Data transfer object used to update DamageVehicle.
/// </summary>
public record UpdateDamageVehicleDto
{
    /// <summary>Gets or sets the identifier.</summary>
    public long Id { get; init; }
    /// <summary>Gets or sets the VehicleId.</summary>
    public long VehicleId { get; init; }
    /// <summary>Gets or sets the BookingId.</summary>
    public long BookingId { get; init; }
    /// <summary>Gets or sets the Severity.</summary>
    public SeverityStatus Severity { get; init; }
    /// <summary>Gets or sets the Description.</summary>
    public string Description { get; init; } = string.Empty;
    /// <summary>Gets or sets the Images.</summary>
    public List<IFormFile> Images { get; init; } = [];
    /// <summary>Gets or sets the RepairCost.</summary>
    public decimal RepairCost { get; init; }
    /// <summary>Gets or sets the DamageDate.</summary>
    public DateTime DamageDate { get; init; }
}
