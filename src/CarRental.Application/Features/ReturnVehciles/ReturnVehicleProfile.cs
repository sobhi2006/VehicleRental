using AutoMapper;
using CarRental.Application.DTOs.ReturnVehicle;
using CarRental.Application.Features.ReturnVehicles.Commands.CreateReturnVehicle;
using CarRental.Application.Features.ReturnVehicles.Commands.UpdateReturnVehicle;
using CarRental.Domain.Entities.Vehicles;

namespace CarRental.Application.Features.ReturnVehicles;

/// <summary>
/// AutoMapper profile for ReturnVehicle mappings.
/// </summary>
public class ReturnVehicleProfile : Profile
{
    public ReturnVehicleProfile()
    {
        CreateMap<CreateReturnVehicleCommand, ReturnVehicle>();
        CreateMap<UpdateReturnVehicleCommand, ReturnVehicle>();
        CreateMap<ReturnVehicle, ReturnVehicleDto>();
    }
}
