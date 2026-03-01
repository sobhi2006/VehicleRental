using MediatR;
using AutoMapper;
using CarRental.Application.Common;
using CarRental.Application.DTOs.Person;
using CarRental.Application.Interfaces;

namespace CarRental.Application.Features.Persons.Queries.GetAllPersons;

/// <summary>
/// Handles retrieving all Persons.
/// </summary>
public class GetAllPersonsQueryHandler : IRequestHandler<GetAllPersonsQuery, Result<PaginatedList<PersonDto>>>
{
    private readonly IPersonService _service;
    private readonly IMapper _mapper;

    /// <summary>
    /// Initializes a new instance of the <see cref="GetAllPersonsQueryHandler"/> class.
    /// </summary>
    public GetAllPersonsQueryHandler(IPersonService service, IMapper mapper)
    {
        _service = service;
        _mapper = mapper;
    }

    /// <summary>
    /// Handles the query.
    /// </summary>
    public async Task<Result<PaginatedList<PersonDto>>> Handle(GetAllPersonsQuery request, CancellationToken cancellationToken)
    {
        var result = await _service.GetAllAsync(request.PageNumber, request.PageSize, cancellationToken);
        return result.MapPaginatedResult(value => _mapper.Map<PersonDto>(value));
    }
}
