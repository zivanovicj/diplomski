using AutoMapper;
using Fuel9_BE.DTO;
using Fuel9_BE.Interfaces;
using Fuel9_BE.Models;
using Fuel9_BE.Repository.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;

namespace Fuel9_BE.Services
{
    public class VehicleService : IVehicleService
    {
        private readonly IMapper _mapper;
        private readonly IVehicleRepository _vehicleRepository;
        private readonly IFuelLogRepository _fuelLogRepository;
        private double conversionConstant = 0.62137;

        public VehicleService(IMapper mapper, IVehicleRepository vehiclesRepository, IFuelLogRepository fuelLogRepository)
        {
            _mapper = mapper;
            _vehicleRepository = vehiclesRepository;
            _fuelLogRepository = fuelLogRepository;
        }

        public async Task AddVehicle(VehicleDTO vehicle)
        {
            var vehicleToAdd = _mapper.Map<Vehicle>(vehicle);
            await _vehicleRepository.AddVehicle(vehicleToAdd);
        }

        public async Task<List<VehicleDTO>> GetAll()
        {
            var vehicles = await _vehicleRepository.GetAll();
            return _mapper.Map<List<VehicleDTO>>(vehicles);
        }

        public async Task<VehicleDTO> GetById(int id)
        {
            if (id <= 0) //should be refactored to validator
            {
                throw new Exception("The given ID is not valid");
            }

            var vehicle = await _vehicleRepository.GetById(id);

            if (vehicle == null)
            {
                throw new Exception($"Vehicle with given ID was not found. ID: {id}");
            }
            return _mapper.Map<VehicleDTO>(vehicle);
        }

        public async Task<bool> RemoveVehicle(int id)
        {
            if (id <= 0)  // TODO: Should be refactored to validator
            {
                throw new Exception("The given ID is not valid. Should be greater then 0.");
            }
            var vehicleToRemove = await _vehicleRepository.GetById(id);
            if (vehicleToRemove == null)
            {
                return false;
            }
            await _vehicleRepository.RemoveVehicle(vehicleToRemove);
            return true;
        }

        public async Task<VehicleDTO> UpdateVehicle(VehicleDTO vehicle)
        {
            var divide = true;
            if (vehicle.Id <= 0)
            {
                throw new Exception("Invalid ID provided. ID should be greater then 0.");
            }
            else
            {
                var oldVehicle = await _vehicleRepository.GetById(vehicle.Id);
                if (!oldVehicle.OdometerType.Equals(vehicle.OdometerType))
                {
                    if (vehicle.OdometerType.Equals("metric"))
                    {
                        vehicle.Odometer /= conversionConstant;
                    }
                    else
                    {
                        vehicle.Odometer *= conversionConstant;
                        divide = false;
                    }
                    var fuelLogs = await _fuelLogRepository.GetVehiclesFuelLogs(vehicle.Id);
                    foreach (var fuelLog in fuelLogs)
                    {
                        if (divide)
                        {
                            fuelLog.Distance /= conversionConstant;
                        }
                        else
                        {
                            fuelLog.Distance *= conversionConstant;
                        }
                        await _fuelLogRepository.UpdateFuelLog(fuelLog);
                    }
                }
                var updatedVehicle = await _vehicleRepository.UpdateVehicle(_mapper.Map<Vehicle>(vehicle));

                return _mapper.Map<VehicleDTO>(updatedVehicle);
            }
        }
    }
}
