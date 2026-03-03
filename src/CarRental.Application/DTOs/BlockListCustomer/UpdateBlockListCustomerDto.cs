namespace CarRental.Application.DTOs.BlockListCustomer;

/// <summary>
/// Data transfer object used to update BlockListCustomer.
/// </summary>
public record UpdateBlockListCustomerDto
{
    /// <summary>Gets or sets the identifier.</summary>
    public long Id { get; init; }
    /// <summary>Gets or sets the DriverId.</summary>
    public long DriverId { get; init; }
    /// <summary>Gets or sets the IsBlock.</summary>
    public bool IsBlock { get; init; }
    /// <summary>Gets or sets the Description.</summary>
    public string Description { get; init; } = string.Empty;
}
