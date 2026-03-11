using AutoMapper;
using CarRental.Application.DTOs.ImagesDto;
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
            .ForMember(dest => dest.Images, opt => opt.Ignore());
        CreateMap<UpdateVehicleCommand, Vehicle>()
            .ForMember(dest => dest.Images, opt => opt.Ignore());
        CreateMap<Vehicle, VehicleDto>()
            .ForMember(dest => dest.Images, 
            opt => opt.MapFrom(src => src.Images.Select(i => 
                                                                                    new ImageDto 
                                                                                    {
                                                                                        Id = i.Id,
                                                                                        ImageUrl = i.Url 
                                                                                    }).ToList()));
    }
}
