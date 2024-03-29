﻿using Fuel9_BE.DTO;
using Fuel9_BE.ExceptionFilter;
using Fuel9_BE.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Threading.Tasks;
using System;

namespace Fuel9_BE.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VehiclesController : ControllerBase
    {
        private readonly IVehicleService _vehicleService;

        public VehiclesController(IVehicleService vehicleService)
        {
            _vehicleService = vehicleService;
        }

        [HttpPost]
        public async Task<IActionResult> AddVehicle([FromBody] VehicleDTO vehicle)
        {
            await _vehicleService.AddVehicle(vehicle);
            return Ok();
        }

        [HttpGet]
        public async Task<IActionResult> GetVehicles()
        {
            var result = await _vehicleService.GetAll();

            return Ok(result);
        }

        [HttpGet("{id}")]
        [BusinessExceptionFilter(typeof(Exception), HttpStatusCode.BadRequest)]
        public async Task<IActionResult> GetVehicleById(int id)
        {
            var result = await _vehicleService.GetById(id);

            return Ok(result);
        }

        [HttpDelete("{id}")]
        [BusinessExceptionFilter(typeof(Exception), HttpStatusCode.BadRequest)]
        public async Task<IActionResult> RemoveVehicle(int id)
        {
            await _vehicleService.RemoveVehicle(id);

            return Ok();
        }

        [HttpPatch]
        [BusinessExceptionFilter(typeof(Exception), HttpStatusCode.BadRequest)]
        public async Task<IActionResult> UpdateVehicle(VehicleDTO vehicle)
        {
            var result = await _vehicleService.UpdateVehicle(vehicle);

            return Ok(result);
        }
    }
}
