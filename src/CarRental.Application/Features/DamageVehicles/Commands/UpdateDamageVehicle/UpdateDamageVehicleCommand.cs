using MediatR;
using CarRental.Application.Common;
using CarRental.Application.DTOs.DamageVehicle;
using CarRental.Domain.Enums;
using Microsoft.AspNetCore.Http;

namespace CarRental.Application.Features.DamageVehicles.Commands.UpdateDamageVehicle;

/// <summary>
/// Command to update a DamageVehicle.
/// </summary>
public record UpdateDamageVehicleCommand : IRequest<Result<DamageVehicleDto>>
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
    /// <summary>Gets or sets the Photos.</summary>
    public List<IFormFile> Photos { get; init; } = [];
    /// <summary>Gets or sets the RepairCost.</summary>
    public decimal RepairCost { get; init; }
    /// <summary>Gets or sets the DamageDate.</summary>
    public DateTime DamageDate { get; init; }
}
