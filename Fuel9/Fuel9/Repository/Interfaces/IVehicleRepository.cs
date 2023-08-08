using Fuel9.Models;

namespace Fuel9.Repository.Interfaces
{
    public interface IVehicleRepository
    {
        Task<List<Vehicle>> GetAll();
        Task AddVehicle(Vehicle vehicle);
        Task<Vehicle> GetById(int? id);
        Task RemoveVehicle(Vehicle vehicle);
        Task<Vehicle> UpdateVehicle(Vehicle vehicle);
    }
}
