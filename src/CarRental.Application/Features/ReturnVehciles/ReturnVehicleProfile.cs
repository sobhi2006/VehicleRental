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
        CreateMap<CreateReturnVehicleCommand, ReturnVehicle>()
            .ForMember(dest => dest.ReturnVehicleFeesBanks, opt => opt.Ignore());

        CreateMap<UpdateReturnVehicleCommand, ReturnVehicle>()
            .ForMember(dest => dest.ReturnVehicleFeesBanks, opt => opt.Ignore());

        CreateMap<ReturnVehicle, ReturnVehicleDto>()
            .ForMember(dest => dest.FeesBankIds,
                opt => opt.MapFrom(src => src.ReturnVehicleFeesBanks.Select(x => x.FeesBankId).ToList()));
    }
}
