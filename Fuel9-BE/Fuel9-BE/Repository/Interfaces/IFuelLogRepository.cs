using Fuel9_BE.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Fuel9_BE.Repository.Interfaces
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
