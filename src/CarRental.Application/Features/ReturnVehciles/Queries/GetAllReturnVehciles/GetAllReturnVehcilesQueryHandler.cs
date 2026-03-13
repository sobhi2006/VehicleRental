using MediatR;
using AutoMapper;
using CarRental.Application.Common;
using CarRental.Application.DTOs.ReturnVehicle;
using CarRental.Application.Interfaces;

namespace CarRental.Application.Features.ReturnVehicles.Queries.GetAllReturnVehicles;

/// <summary>
/// Handles retrieving all ReturnVehicles.
/// </summary>
public class GetAllReturnVehiclesQueryHandler : IRequestHandler<GetAllReturnVehiclesQuery, Result<PaginatedList<ReturnVehicleDto>>>
{
    private readonly IReturnVehicleService _service;
    private readonly IMapper _mapper;

    /// <summary>
    /// Initializes a new instance of the <see cref="GetAllReturnVehiclesQueryHandler"/> class.
    /// </summary>
    public GetAllReturnVehiclesQueryHandler(IReturnVehicleService service, IMapper mapper)
    {
        _service = service;
        _mapper = mapper;
    }

    /// <summary>
    /// Handles the query.
    /// </summary>
    public async Task<Result<PaginatedList<ReturnVehicleDto>>> Handle(GetAllReturnVehiclesQuery request, CancellationToken cancellationToken)
    {
        var result = await _service.GetAllAsync(request.PageNumber, request.PageSize, cancellationToken);
        return result.MapPaginatedResult(value => _mapper.Map<ReturnVehicleDto>(value));
    }
}
