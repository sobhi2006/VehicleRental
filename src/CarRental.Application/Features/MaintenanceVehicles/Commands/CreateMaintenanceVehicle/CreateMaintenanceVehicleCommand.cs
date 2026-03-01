using MediatR;
using CarRental.Application.Common;
using CarRental.Application.DTOs.MaintenanceVehicle;
using CarRental.Domain.Enums;

namespace CarRental.Application.Features.MaintenanceVehicles.Commands.CreateMaintenanceVehicle;

/// <summary>
/// Command to create a MaintenanceVehicle.
/// </summary>
public record CreateMaintenanceVehicleCommand : IRequest<Result<MaintenanceVehicleDto>>
{
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
