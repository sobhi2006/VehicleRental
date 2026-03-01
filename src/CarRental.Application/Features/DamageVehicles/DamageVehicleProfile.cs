using AutoMapper;
using CarRental.Application.DTOs.DamageVehicle;
using CarRental.Application.Features.DamageVehicles.Commands.CreateDamageVehicle;
using CarRental.Application.Features.DamageVehicles.Commands.UpdateDamageVehicle;
using CarRental.Domain.Entities.Vehicles;

namespace CarRental.Application.Features.DamageVehicles;

/// <summary>
/// AutoMapper profile for DamageVehicle mappings.
/// </summary>
public class DamageVehicleProfile : Profile
{
    public DamageVehicleProfile()
    {
        CreateMap<CreateDamageVehicleCommand, DamageVehicle>()
            .ForMember(d => d.Photos, opt => opt.MapFrom(s => s.Photos.Select(i => i.FileName).ToList()));

        CreateMap<UpdateDamageVehicleCommand, DamageVehicle>()
            .ForMember(d => d.Photos, opt => opt.MapFrom(s => s.Photos.Select(i => i.FileName).ToList()));

        CreateMap<DamageVehicle, DamageVehicleDto>();
    }
}
