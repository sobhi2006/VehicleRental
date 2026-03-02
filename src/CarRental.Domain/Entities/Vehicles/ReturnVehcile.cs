using CarRental.Domain.Common;

namespace CarRental.Domain.Entities.Vehicles;

/// <summary>
/// Represents the ReturnVehicle entity.
/// </summary>
public class ReturnVehicle : BaseEntity
{
    /// <summary>Gets or sets the BookingId.</summary>
    public long BookingId { get; set; }
    /// <summary>Gets or sets the ConditionNotes.</summary>
    public string? ConditionNotes { get; set; }
    /// <summary>Gets or sets the ActualReturnDate.</summary>
    public DateTime ActualReturnDate { get; set; }
    /// <summary>Gets or sets the MileageAfter.</summary>
    public decimal MileageAfter { get; set; }
    /// <summary>Gets or sets the ExcessMileageFess.</summary>
    public long? ExcessMileageFess { get; set; }
    /// <summary>Gets or sets the DamageId.</summary>
    public long? DamageId{ get; set; }
    /// <summary>Gets or sets the related BookingVehicle.</summary>
    public virtual BookingVehicle? BookingVehicle { get; set; }
    /// <summary>Gets or sets the related FeesBank.</summary>
    public virtual FeesBank? FeesBank { get; set; }
    /// <summary>Gets or sets the related DamageVehicle.</summary>
    public virtual DamageVehicle? DamageVehicle { get; set; }
}
