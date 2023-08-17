using Fuel9_BE.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Fuel9_BE.Repository.Interfaces
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
