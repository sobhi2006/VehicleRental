namespace CarRental.Domain.Common;

/// <summary>
/// Base auditable entity with created/updated by tracking.
/// </summary>
public abstract class BaseAuditableEntity : BaseEntity
{
    /// <summary>
    /// Gets or sets the user who created the entity.
    /// </summary>
    public string? CreatedBy { get; set; }
    /// <summary>
    /// Gets or sets the user who last updated the entity.
    /// </summary>
    public string? UpdatedBy { get; set; }
}
