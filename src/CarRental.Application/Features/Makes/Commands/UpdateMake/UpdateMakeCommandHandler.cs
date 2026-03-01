using MediatR;
using AutoMapper;
using CarRental.Application.Common;
using CarRental.Application.DTOs.Make;
using CarRental.Application.Interfaces;
using CarRental.Domain.Entities.Vehicles;

namespace CarRental.Application.Features.Makes.Commands.UpdateMake;

/// <summary>
/// Handles updates of Make.
/// </summary>
public class UpdateMakeCommandHandler : IRequestHandler<UpdateMakeCommand, Result<MakeDto>>
{
    private readonly IMakeService _service;
    private readonly IMapper _mapper;

    /// <summary>
    /// Initializes a new instance of the <see cref="UpdateMakeCommandHandler"/> class.
    /// </summary>
    public UpdateMakeCommandHandler(IMakeService service, IMapper mapper)
    {
        _service = service;
        _mapper = mapper;
    }

    /// <summary>
    /// Handles the command.
    /// </summary>
    public async Task<Result<MakeDto>> Handle(UpdateMakeCommand request, CancellationToken cancellationToken)
    {
        var entity = _mapper.Map<Make>(request);

        var result = await _service.UpdateAsync(entity, cancellationToken);
        return result.MapResult(_mapper.Map<MakeDto>);
    }
}
