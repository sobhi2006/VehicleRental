using MediatR;
using AutoMapper;
using CarRental.Application.Common;
using CarRental.Application.DTOs.Classification;
using CarRental.Application.Interfaces;
using CarRental.Domain.Entities.Vehicles;

namespace CarRental.Application.Features.Classifications.Commands.UpdateClassification;

/// <summary>
/// Handles updates of Classification.
/// </summary>
public class UpdateClassificationCommandHandler : IRequestHandler<UpdateClassificationCommand, Result<ClassificationDto>>
{
    private readonly IClassificationService _service;
    private readonly IMapper _mapper;

    /// <summary>
    /// Initializes a new instance of the <see cref="UpdateClassificationCommandHandler"/> class.
    /// </summary>
    public UpdateClassificationCommandHandler(IClassificationService service, IMapper mapper)
    {
        _service = service;
        _mapper = mapper;
    }

    /// <summary>
    /// Handles the command.
    /// </summary>
    public async Task<Result<ClassificationDto>> Handle(UpdateClassificationCommand request, CancellationToken cancellationToken)
    {
        var entity = _mapper.Map<Classification>(request);

        var result = await _service.UpdateAsync(entity, cancellationToken);
        return result.MapResult(value => _mapper.Map<ClassificationDto>(value));
    }
}
