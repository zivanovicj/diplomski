using Fuel9.Context;
using Fuel9.Models;
using Fuel9.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Fuel9.Repository
{
    public class FuelLogRepository : IFuelLogRepository
    {
        private readonly Fuel9DbContext _dbContext;

        public FuelLogRepository(Fuel9DbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task AddFuelLog(FuelLog fuelLog)
        {
            await _dbContext.FuelLogs.AddAsync(fuelLog);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<FuelLog> GetById(int id)
        {
            return await _dbContext.FuelLogs.AsNoTracking().SingleOrDefaultAsync(x => x.Id == id);
        }

        public async Task<List<FuelLog>> GetVehiclesFuelLogs(int? vehicleId)
        {
            return await _dbContext.FuelLogs.AsNoTracking().Where(x => x.VehicleID == vehicleId).ToListAsync();
        }

        public async Task RemoveFuelLog(FuelLog fuelLog)
        {
            _dbContext.FuelLogs.Remove(fuelLog);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<FuelLog> UpdateFuelLog(FuelLog fuelLog)
        {
            _dbContext.Entry(fuelLog).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync();
            return fuelLog;
        }
    }
}
