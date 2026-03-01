using MediatR;
using AutoMapper;
using CarRental.Application.Common;
using CarRental.Application.DTOs.Classification;
using CarRental.Application.Interfaces;

namespace CarRental.Application.Features.Classifications.Queries.GetAllClassifications;

/// <summary>
/// Handles retrieving all Classifications.
/// </summary>
public class GetAllClassificationsQueryHandler : IRequestHandler<GetAllClassificationsQuery, Result<PaginatedList<ClassificationDto>>>
{
    private readonly IClassificationService _service;
    private readonly IMapper _mapper;

    /// <summary>
    /// Initializes a new instance of the <see cref="GetAllClassificationsQueryHandler"/> class.
    /// </summary>
    public GetAllClassificationsQueryHandler(IClassificationService service, IMapper mapper)
    {
        _service = service;
        _mapper = mapper;
    }

    /// <summary>
    /// Handles the query.
    /// </summary>
    public async Task<Result<PaginatedList<ClassificationDto>>> Handle(GetAllClassificationsQuery request, CancellationToken cancellationToken)
    {
        var result = await _service.GetAllAsync(request.PageNumber, request.PageSize, cancellationToken);
        return result.MapPaginatedResult(value => _mapper.Map<ClassificationDto>(value));
    }
}
