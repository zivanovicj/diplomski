using Fuel9.DTO;
using Fuel9.ExceptionFilter;
using Fuel9.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Fuel9.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FuelLogsController : ControllerBase
    {
        private readonly IFuelLogService _fuelLogService;

        public FuelLogsController(IFuelLogService fuelLogService)
        {
            _fuelLogService = fuelLogService;
        }

        [HttpGet]
        [Route("{vehicleId}")]
        [BusinessExceptionFilter(typeof(Exception), HttpStatusCode.BadRequest)]
        public async Task<IActionResult> GetVehicleFuelLogs(int vehicleId)
        {
            var result = await _fuelLogService.GetVehicleFuelLogs(vehicleId);

            return Ok(result);
        }

        [HttpGet]
        [Route("details/{id}")]
        [BusinessExceptionFilter(typeof(Exception), HttpStatusCode.BadRequest)]
        public async Task<IActionResult> GetFuelLogByID(int id)
        {
            var result = await _fuelLogService.GetById(id);

            return Ok(result);
        }

        [HttpGet]
        [Route("statistics/{vehicleID}")]
        [BusinessExceptionFilter(typeof(Exception), HttpStatusCode.BadRequest)]
        public async Task<IActionResult> GetStatistics(int vehicleID)
        {
            var result = await _fuelLogService.GetVehicleStatistics(vehicleID);

            return Ok(result);
        }

        [HttpPost]
        [BusinessExceptionFilter(typeof(Exception), HttpStatusCode.BadRequest)]
        public async Task<IActionResult> CreateFuelLog([FromBody] FuelLogDTO fuelLog)
        {
            await _fuelLogService.AddFuelLog(fuelLog);
            return Ok();
        }

        [HttpDelete("{id}")]
        [BusinessExceptionFilter(typeof(Exception), HttpStatusCode.BadRequest)]
        public async Task<IActionResult> RemoveFuelLog(int id)
        {
            await _fuelLogService.RemoveFuelLog(id);

            return Ok();
        }

        [HttpPatch("{id}")]
        [BusinessExceptionFilter(typeof(Exception), HttpStatusCode.BadRequest)]
        public async Task<IActionResult> UpdateFuelLog(FuelLogDTO fuelLog)
        {
            var result = await _fuelLogService.UpdateFuelLog(fuelLog);

            return Ok(result);
        }
    }
}
