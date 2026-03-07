using CarRental.Domain.Entities.Vehicles;

namespace CarRental.Domain.Entities.ImageEntities;

public class DamageVehicleImage : Image
{
    public long DamageVehicleId { get; set; }
    public DamageVehicle? DamageVehicle { get; set; }
}
