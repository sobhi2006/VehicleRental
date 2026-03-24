using MediatR;
using CarRental.Application.Common;
using CarRental.Application.DTOs.ReturnVehicle;

namespace CarRental.Application.Features.ReturnVehicles.Commands.UpdateReturnVehicle;

/// <summary>
/// Command to update a ReturnVehicle.
/// </summary>
public record UpdateReturnVehicleCommand : IRequest<Result<ReturnVehicleDto>>
{
    /// <summary>Gets or sets the identifier.</summary>
    public long Id { get; init; }
    /// <summary>Gets or sets the BookingId.</summary>
    public long BookingId { get; init; }
    /// <summary>Gets or sets the ConditionNotes.</summary>
    public string? ConditionNotes { get; init; }
    /// <summary>Gets or sets the ActualReturnDate.</summary>
    public DateTime ActualReturnDate { get; init; }
    /// <summary>Gets or sets the MileageAfter.</summary>
    public decimal MileageAfter { get; init; }
    /// <summary>Gets or sets the FeesBankIds to apply.</summary>
    public List<long> FeesBankIds { get; init; } = [];
    /// <summary>Gets or sets the DamageId.</summary>
    public long? DamageId{ get; init; }
}
