using AutoMapper;
using CarRental.Application.DTOs.Driver;
using CarRental.Application.Features.Drivers.Commands.CreateDriver;
using CarRental.Application.Features.Drivers.Commands.UpdateDriver;
using CarRental.Domain.Entities.Vehicles;

namespace CarRental.Application.Features.Drivers;

/// <summary>
/// AutoMapper profile for Driver mappings.
/// </summary>
public class DriverProfile : Profile
{
    public DriverProfile()
    {
        CreateMap<CreateDriverCommand, Driver>();
        CreateMap<UpdateDriverCommand, Driver>();
        CreateMap<Driver, DriverDto>();
    }
}
