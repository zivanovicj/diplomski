using FluentValidation;
using Fuel9_BE.DTO;
using Fuel9_BE.Enums;

namespace Fuel9_BE.Validators
{
    public class FuelLogDTOValidator : AbstractValidator<FuelLogDTO>
    {
        public FuelLogDTOValidator()
        {
            RuleFor(fuelLog => fuelLog.Distance).NotEmpty();
            RuleFor(fuelLog => fuelLog.RefuelTime).NotEmpty();
            RuleFor(fuelLog => fuelLog.VehicleID).NotEmpty();
            RuleFor(fuelLog => fuelLog.Amount).NotEmpty();
            RuleFor(fuelLog => fuelLog.FuelType).NotEmpty().IsEnumName(typeof(EFuelType), caseSensitive: false).WithMessage("Entered fuel type isn't valid");
            RuleFor(fuelLog => fuelLog.Price).NotEmpty();
            RuleFor(fuelLog => fuelLog.TotalOdometer).NotEmpty();
        }
    }
}
