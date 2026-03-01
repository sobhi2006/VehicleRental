using MediatR;
using AutoMapper;
using CarRental.Application.Common;
using CarRental.Application.DTOs.Driver;
using CarRental.Application.Interfaces;

namespace CarRental.Application.Features.Drivers.Queries.GetDriverById;

/// <summary>
/// Handles retrieving a Driver by id.
/// </summary>
public class GetDriverByIdQueryHandler : IRequestHandler<GetDriverByIdQuery, Result<DriverDto>>
{
    private readonly IDriverService _service;
    private readonly IMapper _mapper;

    /// <summary>
    /// Initializes a new instance of the <see cref="GetDriverByIdQueryHandler"/> class.
    /// </summary>
    public GetDriverByIdQueryHandler(IDriverService service, IMapper mapper)
    {
        _service = service;
        _mapper = mapper;
    }

    /// <summary>
    /// Handles the query.
    /// </summary>
    public async Task<Result<DriverDto>> Handle(GetDriverByIdQuery request, CancellationToken cancellationToken)
    {
        var result = await _service.GetByIdAsync(request.Id, cancellationToken);
        return result.MapResult(value => _mapper.Map<DriverDto>(value));
    }
}
