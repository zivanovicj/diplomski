using Fuel9_BE.Enums;

namespace Fuel9_BE.Models
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
