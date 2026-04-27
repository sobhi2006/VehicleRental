using CarRental.Domain.Common;

namespace CarRental.Domain.Entities.Vehicles;

/// <summary>
/// Join entity between ReturnVehicle and FeesBank.
/// </summary>
public class ReturnVehicleFeesBank : BaseEntity
{
    /// <summary>Gets or sets the ReturnVehicle identifier.</summary>
    public long ReturnVehicleId { get; set; }
    /// <summary>Gets or sets the FeesBank identifier.</summary>
    public long FeesBankId { get; set; }
    /// <summary>Gets or sets the related ReturnVehicle.</summary>
    public virtual ReturnVehicle? ReturnVehicle { get; set; }
    /// <summary>Gets or sets the related FeesBank.</summary>
    public virtual FeesBank? FeesBank { get; set; }
}