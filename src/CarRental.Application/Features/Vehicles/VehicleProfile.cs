using AutoMapper;
using CarRental.Application.DTOs.Vehicle;
using CarRental.Application.Features.Vehicles.Commands.CreateVehicle;
using CarRental.Application.Features.Vehicles.Commands.UpdateVehicle;
using CarRental.Domain.Entities.Vehicles;

namespace CarRental.Application.Features.Vehicles;

/// <summary>
/// AutoMapper profile for Vehicle mappings.
/// </summary>
public class VehicleProfile : Profile
{
    public VehicleProfile()
    {
        CreateMap<CreateVehicleCommand, Vehicle>();
        CreateMap<UpdateVehicleCommand, Vehicle>();
        CreateMap<Vehicle, VehicleDto>()
            .ForMember(dest => dest.ImageUrl, opt => opt.MapFrom(src => src.ImageUrl.Select(i => i.Url).ToList()));
    }
}
