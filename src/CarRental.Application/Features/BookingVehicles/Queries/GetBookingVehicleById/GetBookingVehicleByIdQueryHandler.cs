using MediatR;
using AutoMapper;
using CarRental.Application.Common;
using CarRental.Application.DTOs.BookingVehicle;
using CarRental.Application.Interfaces;

namespace CarRental.Application.Features.BookingVehicles.Queries.GetBookingVehicleById;

/// <summary>
/// Handles retrieving a BookingVehicle by id.
/// </summary>
public class GetBookingVehicleByIdQueryHandler : IRequestHandler<GetBookingVehicleByIdQuery, Result<BookingVehicleDto>>
{
    private readonly IBookingVehicleService _service;
    private readonly IMapper _mapper;

    /// <summary>
    /// Initializes a new instance of the <see cref="GetBookingVehicleByIdQueryHandler"/> class.
    /// </summary>
    public GetBookingVehicleByIdQueryHandler(IBookingVehicleService service, IMapper mapper)
    {
        _service = service;
        _mapper = mapper;
    }

    /// <summary>
    /// Handles the query.
    /// </summary>
    public async Task<Result<BookingVehicleDto>> Handle(GetBookingVehicleByIdQuery request, CancellationToken cancellationToken)
    {
        var result = await _service.GetByIdAsync(request.Id, cancellationToken);
        return result.MapResult(value => _mapper.Map<BookingVehicleDto>(value));
    }
}
