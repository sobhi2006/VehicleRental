using AutoMapper;
using CarRental.Application.DTOs.Make;
using CarRental.Application.Features.Makes.Commands.CreateMake;
using CarRental.Application.Features.Makes.Commands.UpdateMake;
using CarRental.Domain.Entities.Vehicles;

namespace CarRental.Application.Features.Makes;

/// <summary>
/// AutoMapper profile for Make mappings.
/// </summary>
public class MakeProfile : Profile
{
    public MakeProfile()
    {
        CreateMap<CreateMakeCommand, Make>();
        CreateMap<UpdateMakeCommand, Make>();
        CreateMap<Make, MakeDto>();
    }
}
