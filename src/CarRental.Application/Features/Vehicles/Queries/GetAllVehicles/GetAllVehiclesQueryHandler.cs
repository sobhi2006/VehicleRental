using MediatR;
using AutoMapper;
using CarRental.Application.Common;
using CarRental.Application.DTOs.Vehicle;
using CarRental.Application.Interfaces;

namespace CarRental.Application.Features.Vehicles.Queries.GetAllVehicles;

/// <summary>
/// Handles retrieving all Vehicles.
/// </summary>
public class GetAllVehiclesQueryHandler : IRequestHandler<GetAllVehiclesQuery, Result<PaginatedList<VehicleDto>>>
{
    private readonly IVehicleService _service;
    private readonly IMapper _mapper;

    /// <summary>
    /// Initializes a new instance of the <see cref="GetAllVehiclesQueryHandler"/> class.
    /// </summary>
    public GetAllVehiclesQueryHandler(IVehicleService service, IMapper mapper)
    {
        _service = service;
        _mapper = mapper;
    }

    /// <summary>
    /// Handles the query.
    /// </summary>
    public async Task<Result<PaginatedList<VehicleDto>>> Handle(GetAllVehiclesQuery request, CancellationToken cancellationToken)
    {
        var result = await _service.GetAllAsync(request.PageNumber, request.PageSize, cancellationToken);
        return result.MapPaginatedResult(value => _mapper.Map<VehicleDto>(value));
    }
}
