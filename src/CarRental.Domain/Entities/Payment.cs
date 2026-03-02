using CarRental.Domain.Common;
using CarRental.Domain.Entities.Vehicles;
using CarRental.Domain.Enums;

namespace CarRental.Domain.Entities;

/// <summary>
/// Represents the Payment entity.
/// </summary>
public class Payment : BaseEntity
{
    /// <summary>Gets or sets the BookingId.</summary>
    public long BookingId { get; set; }
    /// <summary>Gets or sets the CurrencyId.</summary>
    public long CurrencyId { get; set; }
    /// <summary>Gets or sets the Amount.</summary>
    public decimal Amount { get; set; }
    /// <summary>Gets or sets the Type.</summary>
    public AmountType Type { get; set; }
    /// <summary>Gets or sets the Status.</summary>
    public PaymentStatus Status { get; set; }
    /// <summary>Gets or sets the related BookingVehicle.</summary>
    public virtual BookingVehicle BookingVehicle { get; set; }
    /// <summary>Gets or sets the related Currency.</summary>
    public virtual Currency Currency { get; set; }
}
