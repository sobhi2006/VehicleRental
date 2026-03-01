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
        CreateMap<CreateVehicleCommand, Vehicle>()
            .ForMember(d => d.ImageUrl, opt => opt.MapFrom(s => s.Images.Select(i => i.FileName).ToList()));

        CreateMap<UpdateVehicleCommand, Vehicle>()
            .ForMember(d => d.ImageUrl, opt => opt.MapFrom(s => s.Images.Select(i => i.FileName).ToList()));

        CreateMap<Vehicle, VehicleDto>();
    }
}
