using MediatR;
using AutoMapper;
using CarRental.Application.Common;
using CarRental.Application.DTOs.DamageVehicle;
using CarRental.Application.Interfaces;

namespace CarRental.Application.Features.DamageVehicles.Queries.GetAllDamageVehicles;

/// <summary>
/// Handles retrieving all DamageVehicles.
/// </summary>
public class GetAllDamageVehiclesQueryHandler : IRequestHandler<GetAllDamageVehiclesQuery, Result<PaginatedList<DamageVehicleDto>>>
{
    private readonly IDamageVehicleService _service;
    private readonly IMapper _mapper;

    /// <summary>
    /// Initializes a new instance of the <see cref="GetAllDamageVehiclesQueryHandler"/> class.
    /// </summary>
    public GetAllDamageVehiclesQueryHandler(IDamageVehicleService service, IMapper mapper)
    {
        _service = service;
        _mapper = mapper;
    }

    /// <summary>
    /// Handles the query.
    /// </summary>
    public async Task<Result<PaginatedList<DamageVehicleDto>>> Handle(GetAllDamageVehiclesQuery request, CancellationToken cancellationToken)
    {
        var result = await _service.GetAllAsync(request.PageNumber, request.PageSize, cancellationToken);
        return result.MapPaginatedResult(value => _mapper.Map<DamageVehicleDto>(value));
    }
}
