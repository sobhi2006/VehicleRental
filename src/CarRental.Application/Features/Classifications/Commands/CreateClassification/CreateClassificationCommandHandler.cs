using MediatR;
using AutoMapper;
using CarRental.Application.Common;
using CarRental.Application.DTOs.Classification;
using CarRental.Application.Interfaces;
using CarRental.Domain.Entities.Vehicles;

namespace CarRental.Application.Features.Classifications.Commands.CreateClassification;

/// <summary>
/// Handles creation of Classification.
/// </summary>
public class CreateClassificationCommandHandler : IRequestHandler<CreateClassificationCommand, Result<ClassificationDto>>
{
    private readonly IClassificationService _service;
    private readonly IMapper _mapper;

    /// <summary>
    /// Initializes a new instance of the <see cref="CreateClassificationCommandHandler"/> class.
    /// </summary>
    public CreateClassificationCommandHandler(IClassificationService service, IMapper mapper)
    {
        _service = service;
        _mapper = mapper;
    }

    /// <summary>
    /// Handles the command.
    /// </summary>
    public async Task<Result<ClassificationDto>> Handle(CreateClassificationCommand request, CancellationToken cancellationToken)
    {
        var entity = _mapper.Map<Classification>(request);

        var result = await _service.CreateAsync(entity, cancellationToken);
        return result.MapResult(value => _mapper.Map<ClassificationDto>(value));
    }
}
