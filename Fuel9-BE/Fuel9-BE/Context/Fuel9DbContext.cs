using Fuel9_BE.Models;
using Microsoft.EntityFrameworkCore;

namespace Fuel9_BE.Context
{
    public class Fuel9DbContext : DbContext
    {
        public DbSet<FuelLog> FuelLogs { get; set; }
        public DbSet<Vehicle> Vehicles { get; set; }

        public Fuel9DbContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(Fuel9DbContext).Assembly);
        }
    }
}
