using MediatR;
using AutoMapper;
using CarRental.Application.Common;
using CarRental.Application.DTOs.BookingVehicle;
using CarRental.Application.Interfaces;

namespace CarRental.Application.Features.BookingVehicles.Queries.GetAllBookingVehicles;

/// <summary>
/// Handles retrieving all BookingVehicles.
/// </summary>
public class GetAllBookingVehiclesQueryHandler : IRequestHandler<GetAllBookingVehiclesQuery, Result<PaginatedList<BookingVehicleDto>>>
{
    private readonly IBookingVehicleService _service;
    private readonly IMapper _mapper;

    /// <summary>
    /// Initializes a new instance of the <see cref="GetAllBookingVehiclesQueryHandler"/> class.
    /// </summary>
    public GetAllBookingVehiclesQueryHandler(IBookingVehicleService service, IMapper mapper)
    {
        _service = service;
        _mapper = mapper;
    }

    /// <summary>
    /// Handles the query.
    /// </summary>
    public async Task<Result<PaginatedList<BookingVehicleDto>>> Handle(GetAllBookingVehiclesQuery request, CancellationToken cancellationToken)
    {
        var result = await _service.GetAllAsync(request.PageNumber, request.PageSize, cancellationToken);
        return result.MapPaginatedResult(value => _mapper.Map<BookingVehicleDto>(value));
    }
}
