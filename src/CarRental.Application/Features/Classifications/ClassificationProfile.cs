using AutoMapper;
using CarRental.Application.DTOs.Classification;
using CarRental.Application.Features.Classifications.Commands.CreateClassification;
using CarRental.Application.Features.Classifications.Commands.UpdateClassification;
using CarRental.Domain.Entities.Vehicles;

namespace CarRental.Application.Features.Classifications;

/// <summary>
/// AutoMapper profile for Classification mappings.
/// </summary>
public class ClassificationProfile : Profile
{
    public ClassificationProfile()
    {
        CreateMap<CreateClassificationCommand, Classification>()
                .ForMember(dist => dist.Pricing, opt => opt.MapFrom(src => new Pricing
                {
                    PaymentPerDay = src.PaymentPerDay,
                    CostPerExKm = src.CostPerExKm,
                    CostPerLateDay = src.CostPerLateDay
                }));

        CreateMap<UpdateClassificationCommand, Classification>()
                .ForMember(dist => dist.Pricing, opt => opt.MapFrom(src => new Pricing
                {
                    PaymentPerDay = src.PaymentPerDay,
                    CostPerExKm = src.CostPerExKm,
                    CostPerLateDay = src.CostPerLateDay
                }));
        CreateMap<Classification, ClassificationDto>()
                .ForMember(dest => dest.PaymentPerDay, opt => opt.MapFrom(src => src.Pricing.PaymentPerDay))
                .ForMember(dest => dest.CostPerExKm, opt => opt.MapFrom(src => src.Pricing.CostPerExKm))
                .ForMember(dest => dest.CostPerLateDay, opt => opt.MapFrom(src => src.Pricing.CostPerLateDay));
    }
}
