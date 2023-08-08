using Fuel9.Context;
using Fuel9.Models;
using Fuel9.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Fuel9.Repository
{
    public class VehicleRepository : IVehicleRepository
    {
        private readonly Fuel9DbContext _dbContext;

        public VehicleRepository(Fuel9DbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task AddVehicle(Vehicle vehicle)
        {
            await _dbContext.Vehicles.AddAsync(vehicle);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<List<Vehicle>> GetAll()
        {
            return await _dbContext.Vehicles.ToListAsync();
        }

        public async Task<Vehicle> GetById(int? id)
        {
            return await _dbContext.Vehicles.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task RemoveVehicle(Vehicle vehicle)
        {
            _dbContext.Vehicles.Remove(vehicle);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<Vehicle> UpdateVehicle(Vehicle vehicle)
        {
            _dbContext.Entry(vehicle).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync();
            return vehicle;
        }
    }
}
