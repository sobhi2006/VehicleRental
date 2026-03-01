using MediatR;
using AutoMapper;
using CarRental.Application.Common;
using CarRental.Application.DTOs.Person;
using CarRental.Application.Interfaces;
using CarRental.Domain.Entities;

namespace CarRental.Application.Features.Persons.Commands.UpdatePerson;

/// <summary>
/// Handles updates of Person.
/// </summary>
public class UpdatePersonCommandHandler : IRequestHandler<UpdatePersonCommand, Result<PersonDto>>
{
    private readonly IPersonService _service;
    private readonly IMapper _mapper;

    /// <summary>
    /// Initializes a new instance of the <see cref="UpdatePersonCommandHandler"/> class.
    /// </summary>
    public UpdatePersonCommandHandler(IPersonService service, IMapper mapper)
    {
        _service = service;
        _mapper = mapper;
    }

    /// <summary>
    /// Handles the command.
    /// </summary>
    public async Task<Result<PersonDto>> Handle(UpdatePersonCommand request, CancellationToken cancellationToken)
    {
        var entity = _mapper.Map<Person>(request);

        var result = await _service.UpdateAsync(entity, cancellationToken);
        return result.MapResult(value => _mapper.Map<PersonDto>(value));
    }
}
