using CarRental.Domain.Common;

namespace CarRental.Domain.Entities;

/// <summary>
/// Represents the FeesBank entity.
/// </summary>
public class FeesBank : BaseEntity
{
    /// <summary>Gets or sets the Name.</summary>
    public string Name { get; set; } = string.Empty;
    /// <summary>Gets or sets the Amount.</summary>
    public decimal Amount { get; set; }
}
