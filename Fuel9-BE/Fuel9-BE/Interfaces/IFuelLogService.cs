using Fuel9_BE.DTO;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Fuel9_BE.Interfaces
{
    public interface IFuelLogService
    {
        Task<List<FuelLogDTO>> GetVehicleFuelLogs(int? vehicleId);
        Task<FuelLogDTO> GetById(int id);
        Task<bool> RemoveFuelLog(int id);
        Task<FuelLogDTO> UpdateFuelLog(FuelLogDTO fuelLog);
        Task AddFuelLog(FuelLogDTO fuelLog);
        Task<VehicleStatisticsDTO> GetVehicleStatistics(int vehicleID);
    }
}
