using AutoMapper;
using CarRental.Application.DTOs.BookingVehicle;
using CarRental.Application.Features.BookingVehicles.Commands.CreateBookingVehicle;
using CarRental.Application.Features.BookingVehicles.Commands.UpdateBookingVehicle;
using CarRental.Domain.Entities.Vehicles;

namespace CarRental.Application.Features.BookingVehicles;

/// <summary>
/// AutoMapper profile for BookingVehicle mappings.
/// </summary>
public class BookingVehicleProfile : Profile
{
    public BookingVehicleProfile()
    {
        CreateMap<CreateBookingVehicleCommand, BookingVehicle>();
        CreateMap<UpdateBookingVehicleCommand, BookingVehicle>();
        CreateMap<BookingVehicle, BookingVehicleDto>();
    }
}
