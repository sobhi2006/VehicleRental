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
        CreateMap<CreateClassificationCommand, Classification>();
        CreateMap<UpdateClassificationCommand, Classification>();
        CreateMap<Classification, ClassificationDto>();
    }
}
