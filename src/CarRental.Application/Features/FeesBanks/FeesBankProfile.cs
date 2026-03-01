using AutoMapper;
using CarRental.Application.DTOs.FeesBank;
using CarRental.Application.Features.FeesBanks.Commands.CreateFeesBank;
using CarRental.Application.Features.FeesBanks.Commands.UpdateFeesBank;
using CarRental.Domain.Entities;

namespace CarRental.Application.Features.FeesBanks;

/// <summary>
/// AutoMapper profile for FeesBank mappings.
/// </summary>
public class FeesBankProfile : Profile
{
    public FeesBankProfile()
    {
        CreateMap<CreateFeesBankCommand, FeesBank>();
        CreateMap<UpdateFeesBankCommand, FeesBank>();
        CreateMap<FeesBank, FeesBankDto>();
    }
}
