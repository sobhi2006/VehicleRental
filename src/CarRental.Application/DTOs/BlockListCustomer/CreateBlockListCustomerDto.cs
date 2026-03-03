namespace CarRental.Application.DTOs.BlockListCustomer;

/// <summary>
/// Data transfer object used to create BlockListCustomer.
/// </summary>
public record CreateBlockListCustomerDto
{
    /// <summary>Gets or sets the DriverId.</summary>
    public long DriverId { get; init; }
    /// <summary>Gets or sets the IsBlock.</summary>
    public bool IsBlock { get; init; }
    /// <summary>Gets or sets the Description.</summary>
    public string Description { get; init; } = string.Empty;
}
