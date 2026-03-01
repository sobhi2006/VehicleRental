using MediatR;
using AutoMapper;
using CarRental.Application.Common;
using CarRental.Application.DTOs.Person;
using CarRental.Application.Interfaces;
using CarRental.Domain.Entities;

namespace CarRental.Application.Features.Persons.Commands.CreatePerson;

/// <summary>
/// Handles creation of Person.
/// </summary>
public class CreatePersonCommandHandler : IRequestHandler<CreatePersonCommand, Result<PersonDto>>
{
    private readonly IPersonService _service;
    private readonly IMapper _mapper;

    /// <summary>
    /// Initializes a new instance of the <see cref="CreatePersonCommandHandler"/> class.
    /// </summary>
    public CreatePersonCommandHandler(IPersonService service, IMapper mapper)
    {
        _service = service;
        _mapper = mapper;
    }

    /// <summary>
    /// Handles the command.
    /// </summary>
    public async Task<Result<PersonDto>> Handle(CreatePersonCommand request, CancellationToken cancellationToken)
    {
        var entity = _mapper.Map<Person>(request);

        var result = await _service.CreateAsync(entity, cancellationToken);
        return result.MapResult(value => _mapper.Map<PersonDto>(value));
    }
}
