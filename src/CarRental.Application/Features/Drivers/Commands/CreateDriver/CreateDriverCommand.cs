using MediatR;
using CarRental.Application.Common;
using CarRental.Application.DTOs.Driver;

namespace CarRental.Application.Features.Drivers.Commands.CreateDriver;

/// <summary>
/// Command to create a Driver.
/// </summary>
public record CreateDriverCommand : IRequest<Result<DriverDto>>
{
    /// <summary>Gets or sets the PersonId.</summary>
    public long PersonId { get; init; }
    /// <summary>Gets or sets the DriverLicenseNumber.</summary>
    public string DriverLicenseNumber { get; init; } = string.Empty;
    /// <summary>Gets or sets the DriverLicenseExpiryDate.</summary>
    public DateOnly DriverLicenseExpiryDate { get; init; }
}
