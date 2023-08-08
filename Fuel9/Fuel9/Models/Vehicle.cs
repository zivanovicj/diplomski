using Fuel9.Enums;

namespace Fuel9.Models
{
    public class Vehicle
    {
        public int Id { get; set; }
        public string Manufacturer { get; set; }
        public int YearOfProduction { get; set; }
        public string Model { get; set; }
        public double Odometer { get; set; }
        public EOdometerUnit OdometerType { get; set; }
        public int EnginePower { get; set; }
        public EVehicleType VehicleType { get; set; }
        public EFuelTypeVehicle FuelType { get; set; }
        public ETransmission Transmission { get; set; }
    }
}
