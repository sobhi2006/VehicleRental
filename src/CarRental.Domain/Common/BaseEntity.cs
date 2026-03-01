namespace CarRental.Domain.Common;

/// <summary>
/// Base entity with common auditing fields.
/// </summary>
public abstract class BaseEntity
{
    /// <summary>
    /// Gets or sets the primary key.
    /// </summary>
    public long Id { get; set; }
    /// <summary>
    /// Gets or sets the creation timestamp in UTC.
    /// </summary>
    public DateTime CreatedAt { get; set; }
    /// <summary>
    /// Gets or sets the last update timestamp in UTC.
    /// </summary>
    public DateTime? UpdatedAt { get; set; }
}
