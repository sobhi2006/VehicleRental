using MediatR;
using CarRental.Application.Common;
using CarRental.Application.DTOs.Driver;

namespace CarRental.Application.Features.Drivers.Commands.UpdateDriver;

/// <summary>
/// Command to update a Driver.
/// </summary>
public record UpdateDriverCommand : IRequest<Result<DriverDto>>
{
    /// <summary>Gets or sets the identifier.</summary>
    public long Id { get; init; }
    /// <summary>Gets or sets the PersonId.</summary>
    public long PersonId { get; init; }
    /// <summary>Gets or sets the DriverLicenseNumber.</summary>
    public string DriverLicenseNumber { get; init; } = string.Empty;
    /// <summary>Gets or sets the DriverLicenseExpiryDate.</summary>
    public DateOnly DriverLicenseExpiryDate { get; init; }
}
