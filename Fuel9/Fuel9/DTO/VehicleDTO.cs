namespace Fuel9.DTO
{
    public class VehicleDTO
    {
        public int? Id { get; set; }
        public string Manufacturer { get; set; }
        public int YearOfProduction { get; set; }
        public string Model { get; set; }
        public double Odometer { get; set; }
        public string OdometerType { get; set; }
        public int EnginePower { get; set; }
        public string VehicleType { get; set; }
        public string FuelType { get; set; }
        public string Transmission { get; set; }
    }
}
