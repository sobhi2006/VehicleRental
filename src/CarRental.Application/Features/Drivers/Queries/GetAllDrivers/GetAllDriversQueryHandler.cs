using MediatR;
using AutoMapper;
using CarRental.Application.Common;
using CarRental.Application.DTOs.Driver;
using CarRental.Application.Interfaces;

namespace CarRental.Application.Features.Drivers.Queries.GetAllDrivers;

/// <summary>
/// Handles retrieving all Drivers.
/// </summary>
public class GetAllDriversQueryHandler : IRequestHandler<GetAllDriversQuery, Result<PaginatedList<DriverDto>>>
{
    private readonly IDriverService _service;
    private readonly IMapper _mapper;

    /// <summary>
    /// Initializes a new instance of the <see cref="GetAllDriversQueryHandler"/> class.
    /// </summary>
    public GetAllDriversQueryHandler(IDriverService service, IMapper mapper)
    {
        _service = service;
        _mapper = mapper;
    }

    /// <summary>
    /// Handles the query.
    /// </summary>
    public async Task<Result<PaginatedList<DriverDto>>> Handle(GetAllDriversQuery request, CancellationToken cancellationToken)
    {
        var result = await _service.GetAllAsync(request.PageNumber, request.PageSize, cancellationToken);
        return result.MapPaginatedResult(value => _mapper.Map<DriverDto>(value));
    }
}
