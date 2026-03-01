using MediatR;
using AutoMapper;
using CarRental.Application.Common;
using CarRental.Application.DTOs.Classification;
using CarRental.Application.Interfaces;

namespace CarRental.Application.Features.Classifications.Queries.GetClassificationById;

/// <summary>
/// Handles retrieving a Classification by id.
/// </summary>
public class GetClassificationByIdQueryHandler : IRequestHandler<GetClassificationByIdQuery, Result<ClassificationDto>>
{
    private readonly IClassificationService _service;
    private readonly IMapper _mapper;

    /// <summary>
    /// Initializes a new instance of the <see cref="GetClassificationByIdQueryHandler"/> class.
    /// </summary>
    public GetClassificationByIdQueryHandler(IClassificationService service, IMapper mapper)
    {
        _service = service;
        _mapper = mapper;
    }

    /// <summary>
    /// Handles the query.
    /// </summary>
    public async Task<Result<ClassificationDto>> Handle(GetClassificationByIdQuery request, CancellationToken cancellationToken)
    {
        var result = await _service.GetByIdAsync(request.Id, cancellationToken);
        return result.MapResult(value => _mapper.Map<ClassificationDto>(value));
    }
}
