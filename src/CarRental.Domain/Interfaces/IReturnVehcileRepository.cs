using CarRental.Domain.Entities;
using CarRental.Domain.Entities.Vehicles;

namespace CarRental.Domain.Interfaces;

/// <summary>
/// Repository contract for ReturnVehicle.
/// </summary>
public interface IReturnVehicleRepository : IRepository<ReturnVehicle>
{
}
