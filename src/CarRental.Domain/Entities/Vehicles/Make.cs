using CarRental.Domain.Common;

namespace CarRental.Domain.Entities.Vehicles;

/// <summary>
/// Represents the Make entity.
/// </summary>
public class Make : BaseEntity
{
    /// <summary>Gets or sets the Name.</summary>
    public string Name { get; set; } = string.Empty;
}
