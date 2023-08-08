using Fuel9.DTO;

namespace Fuel9.Interfaces
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
