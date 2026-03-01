using MediatR;
using AutoMapper;
using CarRental.Application.Common;
using CarRental.Application.DTOs.BookingVehicle;
using CarRental.Application.Interfaces;
using CarRental.Domain.Entities.Vehicles;

namespace CarRental.Application.Features.BookingVehicles.Commands.CreateBookingVehicle;

/// <summary>
/// Handles creation of BookingVehicle.
/// </summary>
public class CreateBookingVehicleCommandHandler : IRequestHandler<CreateBookingVehicleCommand, Result<BookingVehicleDto>>
{
    private readonly IBookingVehicleService _service;
    private readonly IMapper _mapper;

    /// <summary>
    /// Initializes a new instance of the <see cref="CreateBookingVehicleCommandHandler"/> class.
    /// </summary>
    public CreateBookingVehicleCommandHandler(IBookingVehicleService service, IMapper mapper)
    {
        _service = service;
        _mapper = mapper;
    }

    /// <summary>
    /// Handles the command.
    /// </summary>
    public async Task<Result<BookingVehicleDto>> Handle(CreateBookingVehicleCommand request, CancellationToken cancellationToken)
    {
        var entity = _mapper.Map<BookingVehicle>(request);

        var result = await _service.CreateAsync(entity, cancellationToken);
        return result.MapResult(value => _mapper.Map<BookingVehicleDto>(value));
    }
}
