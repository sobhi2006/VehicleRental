namespace CarRental.Application.DTOs.ReturnVehicle;

/// <summary>
/// Data transfer object for ReturnVehicle.
/// </summary>
public record ReturnVehicleDto
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
    /// <summary>Gets or sets the applied FeesBank identifiers.</summary>
    public List<long> FeesBankIds { get; init; } = [];
    /// <summary>Gets or sets the DamageId.</summary>
    public long? DamageId{ get; init; }
    /// <summary>Gets or sets the creation timestamp.</summary>
    public DateTime CreatedAt { get; init; }
    /// <summary>Gets or sets the last update timestamp.</summary>
    public DateTime? UpdatedAt { get; init; }
    /// <summary>Gets or sets the associated InvoiceId.</summary>
    public long InvoiceId { get; init; }
}
