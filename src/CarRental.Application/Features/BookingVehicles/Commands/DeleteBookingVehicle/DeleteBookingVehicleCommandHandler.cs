using MediatR;
using CarRental.Application.Common;
using CarRental.Application.Interfaces;

namespace CarRental.Application.Features.BookingVehicles.Commands.DeleteBookingVehicle;

/// <summary>
/// Handles deletion of BookingVehicle.
/// </summary>
public class DeleteBookingVehicleCommandHandler : IRequestHandler<DeleteBookingVehicleCommand, Result>
{
    private readonly IBookingVehicleService _service;

    /// <summary>
    /// Initializes a new instance of the <see cref="DeleteBookingVehicleCommandHandler"/> class.
    /// </summary>
    public DeleteBookingVehicleCommandHandler(IBookingVehicleService service)
    {
        _service = service;
    }

    /// <summary>
    /// Handles the command.
    /// </summary>
    public async Task<Result> Handle(DeleteBookingVehicleCommand request, CancellationToken cancellationToken)
    {
        return await _service.DeleteAsync(request.Id, cancellationToken);
    }
}
