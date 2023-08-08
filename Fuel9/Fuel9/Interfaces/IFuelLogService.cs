using Fuel9.DTO;

namespace Fuel9.Interfaces
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
