using CarRental.Domain.Common;

namespace CarRental.Domain.Entities;

/// <summary>
/// Represents the Currency entity.
/// </summary>
public class Currency : BaseEntity
{
    /// <summary>Gets or sets the Name.</summary>
    public string Name { get; set; } = string.Empty;
    /// <summary>Gets or sets the ValueVsOneDollar.</summary>
    public decimal ValueVsOneDollar { get; set; }
}
