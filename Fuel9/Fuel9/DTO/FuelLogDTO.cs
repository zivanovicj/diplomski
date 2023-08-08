namespace Fuel9.DTO
{
    public class FuelLogDTO
    {
        public int? Id { get; set; }
        public int VehicleID { get; set; }
        public DateTime RefuelTime { get; set; }
        public double Price { get; set; }
        public double Amount { get; set; }
        public string FuelType { get; set; }
        public double Distance { get; set; }
        public double TotalOdometer { get; set; }
        public bool IsFull { get; set; }
        public double? StatisticsFL { get; set; }
    }
}
