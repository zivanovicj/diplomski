using Fuel9.Enums;

namespace Fuel9.Models
{
    public class FuelLog
    {
        public int Id { get; set; }
        public int VehicleID { get; set; }
        public DateTime RefuelTime { get; set; }
        public double Price { get; set; }
        public double Amount { get; set; }
        public EFuelType FuelType { get; set; }
        public double Distance { get; set; }
        public double TotalOdometer { get; set; }
        public bool IsFull { get; set; }
        public double StatisticsFL { get; set; }
    }
}
