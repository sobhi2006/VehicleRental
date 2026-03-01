using AutoMapper;
using CarRental.Application.Common;
using CarRental.Application.DTOs.Person;
using CarRental.Application.Interfaces;
using MediatR;

namespace CarRental.Application.Features.Persons.Commands.UpdateNationalNo;

public class UpdateNationalNoHandler : IRequestHandler<UpdateNationalNoCommand, Result<UpdateNationalNoDto>>
{
    private readonly IPersonService _personService;
    private readonly IMapper _mapper;

    public UpdateNationalNoHandler(IPersonService personService, IMapper mapper)
    {
        this._personService = personService;
        _mapper = mapper;
    }
    public async Task<Result<UpdateNationalNoDto>> Handle(UpdateNationalNoCommand request, CancellationToken cancellationToken)
    {
        var result = await _personService.UpdateNationalNoAsync(request.Id, request.NationalNo, cancellationToken);
        return result.MapResult(value => _mapper.Map<UpdateNationalNoDto>(value));
    }
}