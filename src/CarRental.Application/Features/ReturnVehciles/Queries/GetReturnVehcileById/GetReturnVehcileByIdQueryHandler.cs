using MediatR;
using AutoMapper;
using CarRental.Application.Common;
using CarRental.Application.DTOs.ReturnVehicle;
using CarRental.Application.Interfaces;

namespace CarRental.Application.Features.ReturnVehicles.Queries.GetReturnVehicleById;

/// <summary>
/// Handles retrieving a ReturnVehicle by id.
/// </summary>
public class GetReturnVehicleByIdQueryHandler : IRequestHandler<GetReturnVehicleByIdQuery, Result<ReturnVehicleDto>>
{
    private readonly IReturnVehicleService _service;
    private readonly IMapper _mapper;

    /// <summary>
    /// Initializes a new instance of the <see cref="GetReturnVehicleByIdQueryHandler"/> class.
    /// </summary>
    public GetReturnVehicleByIdQueryHandler(IReturnVehicleService service, IMapper mapper)
    {
        _service = service;
        _mapper = mapper;
    }

    /// <summary>
    /// Handles the query.
    /// </summary>
    public async Task<Result<ReturnVehicleDto>> Handle(GetReturnVehicleByIdQuery request, CancellationToken cancellationToken)
    {
        var result = await _service.GetByIdAsync(request.Id, cancellationToken);
        return result.MapResult(value => _mapper.Map<ReturnVehicleDto>(value));
    }
}
