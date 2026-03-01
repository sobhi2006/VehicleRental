using MediatR;
using AutoMapper;
using CarRental.Application.Common;
using CarRental.Application.DTOs.BookingVehicle;
using CarRental.Application.Interfaces;
using CarRental.Domain.Entities.Vehicles;

namespace CarRental.Application.Features.BookingVehicles.Commands.UpdateBookingVehicle;

/// <summary>
/// Handles updates of BookingVehicle.
/// </summary>
public class UpdateBookingVehicleCommandHandler : IRequestHandler<UpdateBookingVehicleCommand, Result<BookingVehicleDto>>
{
    private readonly IBookingVehicleService _service;
    private readonly IMapper _mapper;

    /// <summary>
    /// Initializes a new instance of the <see cref="UpdateBookingVehicleCommandHandler"/> class.
    /// </summary>
    public UpdateBookingVehicleCommandHandler(IBookingVehicleService service, IMapper mapper)
    {
        _service = service;
        _mapper = mapper;
    }

    /// <summary>
    /// Handles the command.
    /// </summary>
    public async Task<Result<BookingVehicleDto>> Handle(UpdateBookingVehicleCommand request, CancellationToken cancellationToken)
    {
        var entity = _mapper.Map<BookingVehicle>(request);

        var result = await _service.UpdateAsync(entity, cancellationToken);
        return result.MapResult(value => _mapper.Map<BookingVehicleDto>(value));
    }
}
