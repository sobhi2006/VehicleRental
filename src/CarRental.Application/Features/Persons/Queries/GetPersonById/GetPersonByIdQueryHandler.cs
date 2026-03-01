using MediatR;
using AutoMapper;
using CarRental.Application.Common;
using CarRental.Application.DTOs.Person;
using CarRental.Application.Interfaces;

namespace CarRental.Application.Features.Persons.Queries.GetPersonById;

/// <summary>
/// Handles retrieving a Person by id.
/// </summary>
public class GetPersonByIdQueryHandler : IRequestHandler<GetPersonByIdQuery, Result<PersonDto>>
{
    private readonly IPersonService _service;
    private readonly IMapper _mapper;

    /// <summary>
    /// Initializes a new instance of the <see cref="GetPersonByIdQueryHandler"/> class.
    /// </summary>
    public GetPersonByIdQueryHandler(IPersonService service, IMapper mapper)
    {
        _service = service;
        _mapper = mapper;
    }

    /// <summary>
    /// Handles the query.
    /// </summary>
    public async Task<Result<PersonDto>> Handle(GetPersonByIdQuery request, CancellationToken cancellationToken)
    {
        var result = await _service.GetByIdAsync(request.Id, cancellationToken);
        return result.MapResult(value => _mapper.Map<PersonDto>(value));
    }
}
