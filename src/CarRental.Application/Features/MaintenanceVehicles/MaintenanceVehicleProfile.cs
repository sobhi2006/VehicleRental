using AutoMapper;
using CarRental.Application.DTOs.MaintenanceVehicle;
using CarRental.Application.Features.MaintenanceVehicles.Commands.CreateMaintenanceVehicle;
using CarRental.Application.Features.MaintenanceVehicles.Commands.UpdateMaintenanceVehicle;
using CarRental.Domain.Entities.Vehicles;

namespace CarRental.Application.Features.MaintenanceVehicles;

/// <summary>
/// AutoMapper profile for MaintenanceVehicle mappings.
/// </summary>
public class MaintenanceVehicleProfile : Profile
{
    public MaintenanceVehicleProfile()
    {
        CreateMap<CreateMaintenanceVehicleCommand, MaintenanceVehicle>();
        CreateMap<UpdateMaintenanceVehicleCommand, MaintenanceVehicle>();
        CreateMap<MaintenanceVehicle, MaintenanceVehicleDto>();
    }
}
