using AutoMapper;
using CarRental.Application.DTOs.Person;
using CarRental.Application.Features.Persons.Commands.CreatePerson;
using CarRental.Application.Features.Persons.Commands.UpdatePerson;
using CarRental.Domain.Entities;

namespace CarRental.Application.Features.Persons;

/// <summary>
/// AutoMapper profile for Person mappings.
/// </summary>
public class PersonProfile : Profile
{
    public PersonProfile()
    {
        CreateMap<CreatePersonCommand, Person>();
        CreateMap<UpdatePersonCommand, Person>();
        CreateMap<Person, PersonDto>();

        CreateMap<Person, UpdateNationalNoDto>();
    }
}
