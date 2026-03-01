using MediatR;
using AutoMapper;
using CarRental.Application.Common;
using CarRental.Application.DTOs.Make;
using CarRental.Application.Interfaces;

namespace CarRental.Application.Features.Makes.Queries.GetMakeById;

/// <summary>
/// Handles retrieving a Make by id.
/// </summary>
public class GetMakeByIdQueryHandler : IRequestHandler<GetMakeByIdQuery, Result<MakeDto>>
{
    private readonly IMakeService _service;
    private readonly IMapper _mapper;

    /// <summary>
    /// Initializes a new instance of the <see cref="GetMakeByIdQueryHandler"/> class.
    /// </summary>
    public GetMakeByIdQueryHandler(IMakeService service, IMapper mapper)
    {
        _service = service;
        _mapper = mapper;
    }

    /// <summary>
    /// Handles the query.
    /// </summary>
    public async Task<Result<MakeDto>> Handle(GetMakeByIdQuery request, CancellationToken cancellationToken)
    {
        var result = await _service.GetByIdAsync(request.Id, cancellationToken);
        return result.MapResult(_mapper.Map<MakeDto>);
    }
}
