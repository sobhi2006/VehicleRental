using MediatR;
using CarRental.Application.Common;
using CarRental.Application.DTOs.BlockListCustomer;

namespace CarRental.Application.Features.BlockListCustomers.Commands.CreateBlockListCustomer;

/// <summary>
/// Command to create a BlockListCustomer.
/// </summary>
public record CreateBlockListCustomerCommand : IRequest<Result<BlockListCustomerDto>>
{
    /// <summary>Gets or sets the DriverId.</summary>
    public long DriverId { get; init; }
    /// <summary>Gets or sets the IsBlock.</summary>
    public bool IsBlock { get; init; }
    /// <summary>Gets or sets the Description.</summary>
    public string Description { get; init; } = string.Empty;
}
