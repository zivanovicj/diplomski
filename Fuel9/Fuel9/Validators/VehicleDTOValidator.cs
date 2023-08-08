using FluentValidation;
using Fuel9.DTO;
using Fuel9.Enums;

namespace Fuel9.Validators
{
    public class VehicleDTOValidator : AbstractValidator<VehicleDTO>
    {
        public VehicleDTOValidator()
        {
            RuleFor(vehicle => vehicle.Model).NotNull().Length(2, 100);
            RuleFor(vehicle => vehicle.Manufacturer).NotEmpty();
            RuleFor(vehicle => vehicle.YearOfProduction).NotEmpty();
            RuleFor(vehicle => vehicle.Odometer).InclusiveBetween(0, double.MaxValue);
            RuleFor(vehicle => vehicle.OdometerType).NotEmpty().IsEnumName(typeof(EOdometerUnit), caseSensitive: false).WithMessage("Entered odometer unit isn't valid");
            RuleFor(vehicle => vehicle.VehicleType).NotEmpty().IsEnumName(typeof(EVehicleType), caseSensitive: false).WithMessage("Entered vehicle type isn't valid");
            RuleFor(vehicle => vehicle.FuelType).NotEmpty().IsEnumName(typeof(EFuelTypeVehicle), caseSensitive: false).WithMessage("Entered fuel type isn't valid");
            RuleFor(vehicle => vehicle.Transmission).NotEmpty().IsEnumName(typeof(ETransmission), caseSensitive: false).WithMessage("Entered transmission isn't valid");
            RuleFor(vehicle => vehicle.EnginePower).NotEmpty().InclusiveBetween(1, 1000);
        }
    }
}
