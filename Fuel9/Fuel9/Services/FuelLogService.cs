using AutoMapper;
using Fuel9.DTO;
using Fuel9.Interfaces;
using Fuel9.Models;
using Fuel9.Repository.Interfaces;
using Microsoft.Data.SqlClient.Server;

namespace Fuel9.Services
{
    public class FuelLogService : IFuelLogService
    {
        private readonly IMapper _mapper;
        private readonly IFuelLogRepository _fuelLogsRepository;
        private readonly IVehicleRepository _vehicleRepository;
        private int DieselCO2 = 2640;
        private int PetrolCO2 = 2392;
        private int GasCO2 = 2252;

        public FuelLogService(IFuelLogRepository fuelLogsRepository, IMapper mapper, IVehicleRepository vehicleRepository)
        {
            _fuelLogsRepository = fuelLogsRepository;
            _mapper = mapper;
            _vehicleRepository = vehicleRepository;
        }

        public async Task AddFuelLog(FuelLogDTO fuelLog)
        {
            var vehicle1 = await _vehicleRepository.GetById(fuelLog.VehicleID);
            if (vehicle1 == null)
            {
                throw new Exception($"Vehicle with the given ID was not found. ID: {fuelLog.VehicleID}");
            }
            var vehicle = _mapper.Map<VehicleDTO>(vehicle1);
            if (fuelLog.FuelType == null)
            {
                fuelLog.FuelType = vehicle.FuelType;
            }

            var fuelLogs = await GetVehicleFuelLogs(vehicle.Id);
            if (fuelLog.RefuelTime == null)
            {
                fuelLog.RefuelTime = DateTime.Now;
            }
            else if (fuelLogs.Count != 0)
            {
                var latestFuelLogDate = fuelLogs.Max(x => x.RefuelTime);
                if (latestFuelLogDate > fuelLog.RefuelTime)
                {
                    throw new Exception($"Please enter valid refuel time. Can not be before {latestFuelLogDate.Date}" +
                        $", which is the date when latest refueling was submitted.");
                }
            }

            if (fuelLog.TotalOdometer == 0 && fuelLog.Distance != 0)
            {
                vehicle.Odometer += fuelLog.Distance;
                fuelLog.TotalOdometer = vehicle.Odometer;
                _ = await _vehicleRepository.UpdateVehicle(_mapper.Map<Vehicle>(vehicle));
            }
            else if (fuelLog.TotalOdometer != 0 && fuelLog.Distance == 0)
            {
                if (fuelLog.TotalOdometer < vehicle.Odometer)
                {
                    throw new Exception("Provided total odometer must not be lower then vehicles odometer.");
                }
                fuelLog.Distance = fuelLog.TotalOdometer - vehicle.Odometer;
                vehicle.Odometer = fuelLog.TotalOdometer;
                _ = await _vehicleRepository.UpdateVehicle(_mapper.Map<Vehicle>(vehicle));
            }
            else if (fuelLog.TotalOdometer != 0 && fuelLog.Distance != 0)
            {
                if (fuelLog.TotalOdometer < vehicle.Odometer)
                {
                    throw new Exception("Provided total odometer must not be lower then vehicles odometer.");
                }
                fuelLog.Distance = fuelLog.TotalOdometer - vehicle.Odometer;
                vehicle.Odometer = fuelLog.TotalOdometer;
                _ = await _vehicleRepository.UpdateVehicle(_mapper.Map<Vehicle>(vehicle));
            }

            int endId = -1;
            if (!fuelLog.IsFull || fuelLogs.Count == 0)
            {
                fuelLog.StatisticsFL = 0;
                await _fuelLogsRepository.AddFuelLog(_mapper.Map<FuelLog>(fuelLog));
                return;
            }
            else
            {
                var fuelLogsStatistics = new FuelLogStatisticsDTO();
                for (int i = fuelLogs.Count - 1; i >= 0; i--)
                {
                    if (fuelLogs[i].IsFull)
                    {
                        endId = i;
                        break;
                    }
                }
            }

            var fuelLogsList = new List<FuelLogDTO>();
            if (endId == -1)
            {
                fuelLog.StatisticsFL = 0;
                await _fuelLogsRepository.AddFuelLog(_mapper.Map<FuelLog>(fuelLog));
                return;
            }
            else
            {
                fuelLogs.Add(fuelLog);
                for (int i = fuelLogs.Count - 1; i >= endId; i--)
                {
                    fuelLogsList.Add(fuelLogs[i]);
                }
            }

            fuelLog.StatisticsFL = fuelLogsList.Sum(x => x.Amount) / fuelLogsList.Count;
            var fuelLogToAdd = _mapper.Map<FuelLog>(fuelLog);
            await _fuelLogsRepository.AddFuelLog(fuelLogToAdd);
        }

        public async Task<List<FuelLogDTO>> GetVehicleFuelLogs(int? vehicleId)
        {
            if (vehicleId <= 0)
            {
                throw new Exception("Provided vehicle ID is not valid. Must be greater then 0.");
            }
            var fuelLogs = await _fuelLogsRepository.GetVehiclesFuelLogs(vehicleId);
            var fuelLogsDTO = _mapper.Map<List<FuelLogDTO>>(fuelLogs);

            return fuelLogsDTO;
        }

        public async Task<FuelLogDTO> GetById(int id)
        {
            if (id <= 0)
            {
                throw new Exception("Provided ID is invalid. Should be greater then 0.");
            }

            var fuelLog = await _fuelLogsRepository.GetById(id);
            if (fuelLog == null)
            {
                throw new Exception("Fuel log with the provided ID was not found.");
            }

            return _mapper.Map<FuelLogDTO>(fuelLog);
        }

        public async Task<bool> RemoveFuelLog(int id)
        {
            if (id <= 0)
            {
                throw new Exception("Invalid FuelLog ID");
            }

            var fuelLog = await _fuelLogsRepository.GetById(id) ?? throw new Exception();
            await _fuelLogsRepository.RemoveFuelLog(fuelLog);
            return true;
        }

        public async Task<FuelLogDTO> UpdateFuelLog(FuelLogDTO fuelLog)
        {
            var fuelLogToUpdate = _mapper.Map<FuelLog>(fuelLog);
            var updatedFuelLog = await _fuelLogsRepository.UpdateFuelLog(fuelLogToUpdate);
            return _mapper.Map<FuelLogDTO>(updatedFuelLog);
        }

        public async Task<VehicleStatisticsDTO> GetVehicleStatistics(int vehicleID)
        {
            if (vehicleID <= 0)
            {
                throw new Exception("Provided vehicle ID is not valid. Must be greater then 0.");
            }
            var vehicle1 = await _vehicleRepository.GetById(vehicleID);
            if (vehicle1 == null)
            {
                throw new Exception($"Vehicle with given ID was not found. ID: {vehicleID}");
            }

            var vehicle = _mapper.Map<VehicleDTO>(vehicle1);
            var fuelLogs = await GetVehicleFuelLogs(vehicleID);
            var vehicleFuelLogs = _mapper.Map<List<FuelLogDTO>>(fuelLogs);
            var vehicleStatistics = new VehicleStatisticsDTO();

            if (vehicleFuelLogs.Count == 0)
            {
                return vehicleStatistics;
            }

            vehicleStatistics.AverageFuelPrice = vehicleFuelLogs.Sum(x => x.Price) / vehicleFuelLogs.Sum(x => x.Amount);
            vehicleStatistics.AverageFuelConsumption = (100 * vehicleFuelLogs.Sum(x => x.Amount)) / vehicleFuelLogs.Sum(x => x.Distance);
            vehicleStatistics.CO2Emission = vehicleStatistics.AverageFuelConsumption / 100;

            switch (vehicle.FuelType.ToUpper())
            {
                case ("DIESEL"):
                    vehicleStatistics.CO2Emission *= DieselCO2;
                    break;
                case ("PETROL"):
                    vehicleStatistics.CO2Emission *= PetrolCO2;
                    break;
                default:
                    vehicleStatistics.CO2Emission *= GasCO2;
                    break;
            }

            return vehicleStatistics;
        }
    }
}
