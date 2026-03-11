using AutoMapper;
using CarRental.Application.DTOs.DamageVehicle;
using CarRental.Application.DTOs.ImagesDto;
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
        CreateMap<CreateDamageVehicleCommand, DamageVehicle>();
        CreateMap<UpdateDamageVehicleCommand, DamageVehicle>();
        CreateMap<DamageVehicle, DamageVehicleDto>()
            .ForMember(dest => dest.Images, opt => opt.MapFrom(src => src.Images.Select(i => new ImageDto {Id = i.Id, ImageUrl = i.Url }).ToList()));
    }
}