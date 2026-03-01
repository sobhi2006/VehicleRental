using MediatR;
using AutoMapper;
using CarRental.Application.Common;
using CarRental.Application.DTOs.Make;
using CarRental.Application.Interfaces;

namespace CarRental.Application.Features.Makes.Queries.GetAllMakes;

/// <summary>
/// Handles retrieving all Makes.
/// </summary>
public class GetAllMakesQueryHandler : IRequestHandler<GetAllMakesQuery, Result<PaginatedList<MakeDto>>>
{
    private readonly IMakeService _service;
    private readonly IMapper _mapper;

    /// <summary>
    /// Initializes a new instance of the <see cref="GetAllMakesQueryHandler"/> class.
    /// </summary>
    public GetAllMakesQueryHandler(IMakeService service, IMapper mapper)
    {
        _service = service;
        _mapper = mapper;
    }

    /// <summary>
    /// Handles the query.
    /// </summary>
    public async Task<Result<PaginatedList<MakeDto>>> Handle(GetAllMakesQuery request, CancellationToken cancellationToken)
    {
        var result = await _service.GetAllAsync(request.PageNumber, request.PageSize, cancellationToken);
        return result.MapPaginatedResult(_mapper.Map<MakeDto>);
    }
}
