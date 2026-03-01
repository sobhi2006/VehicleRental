using MediatR;
using AutoMapper;
using CarRental.Application.Common;
using CarRental.Application.DTOs.Driver;
using CarRental.Application.Interfaces;
using CarRental.Domain.Entities.Vehicles;

namespace CarRental.Application.Features.Drivers.Commands.UpdateDriver;

/// <summary>
/// Handles updates of Driver.
/// </summary>
public class UpdateDriverCommandHandler : IRequestHandler<UpdateDriverCommand, Result<DriverDto>>
{
    private readonly IDriverService _service;
    private readonly IMapper _mapper;

    /// <summary>
    /// Initializes a new instance of the <see cref="UpdateDriverCommandHandler"/> class.
    /// </summary>
    public UpdateDriverCommandHandler(IDriverService service, IMapper mapper)
    {
        _service = service;
        _mapper = mapper;
    }

    /// <summary>
    /// Handles the command.
    /// </summary>
    public async Task<Result<DriverDto>> Handle(UpdateDriverCommand request, CancellationToken cancellationToken)
    {
        var entity = _mapper.Map<Driver>(request);

        var result = await _service.UpdateAsync(entity, cancellationToken);
        return result.MapResult(value => _mapper.Map<DriverDto>(value));
    }
}
