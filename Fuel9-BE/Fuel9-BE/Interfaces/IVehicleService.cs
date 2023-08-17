using Fuel9_BE.DTO;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Fuel9_BE.Interfaces
{
    public interface IVehicleService
    {
        Task<List<VehicleDTO>> GetAll();
        Task<VehicleDTO> GetById(int id);
        Task<bool> RemoveVehicle(int id);
        Task<VehicleDTO> UpdateVehicle(VehicleDTO vehicle);
        Task AddVehicle(VehicleDTO vehicle);
    }
}
