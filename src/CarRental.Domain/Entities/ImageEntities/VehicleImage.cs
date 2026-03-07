using CarRental.Domain.Entities.Vehicles;

namespace CarRental.Domain.Entities.ImageEntities;

public class VehicleImage : Image
{
    public long VehicleId { get; set; }
    public Vehicle? Vehicle { get; set; }
}