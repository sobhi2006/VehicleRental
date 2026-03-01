using CarRental.Domain.Enums;

namespace CarRental.Application.DTOs.MaintenanceVehicle;

/// <summary>
/// Data transfer object used to update MaintenanceVehicle.
/// </summary>
public record UpdateMaintenanceVehicleDto
{
    /// <summary>Gets or sets the identifier.</summary>
    public long Id { get; init; }
    /// <summary>Gets or sets the VehicleId.</summary>
    public long VehicleId { get; init; }
    /// <summary>Gets or sets the Cost.</summary>
    public decimal Cost { get; init; }
    /// <summary>Gets or sets the StartDate.</summary>
    public DateTime StartDate { get; init; }
    /// <summary>Gets or sets the EndDate.</summary>
    public DateTime EndDate { get; init; }
    /// <summary>Gets or sets the Notes.</summary>
    public string Notes { get; init; } = string.Empty;
    /// <summary>Gets or sets the Status.</summary>
    public MaintenanceStatus Status { get; init; }
}
