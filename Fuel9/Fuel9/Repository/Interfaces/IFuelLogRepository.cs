using Fuel9.Models;

namespace Fuel9.Repository.Interfaces
{
    public interface IFuelLogRepository
    {
        Task<List<FuelLog>> GetVehiclesFuelLogs(int? vehicleId);
        Task<FuelLog> GetById(int id);
        Task RemoveFuelLog(FuelLog fuelLog);
        Task<FuelLog> UpdateFuelLog(FuelLog fuelLog);
        Task AddFuelLog(FuelLog fuelLog);
    }
}
