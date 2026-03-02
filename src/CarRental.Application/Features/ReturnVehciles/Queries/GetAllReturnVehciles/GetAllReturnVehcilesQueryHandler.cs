using MediatR;
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

    /// <summary>
    /// Initializes a new instance of the <see cref="GetAllReturnVehiclesQueryHandler"/> class.
    /// </summary>
    public GetAllReturnVehiclesQueryHandler(IReturnVehicleService service)
    {
        _service = service;
    }

    /// <summary>
    /// Handles the query.
    /// </summary>
    public async Task<Result<PaginatedList<ReturnVehicleDto>>> Handle(GetAllReturnVehiclesQuery request, CancellationToken cancellationToken)
    {
        return await _service.GetAllAsync(request.PageNumber, request.PageSize, cancellationToken);
    }
}
