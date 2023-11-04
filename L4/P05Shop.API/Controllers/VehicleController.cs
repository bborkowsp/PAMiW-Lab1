using Microsoft.AspNetCore.Mvc;
using P06Shop.Shared;
using P06Shop.Shared.Services.VehicleService;
using P06Shop.Shared.VehicleDealership;

namespace P05Shop.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VehicleController : Controller
    {
        private readonly IVehicleService _VehicleService;
        private readonly ILogger<VehicleController> _logger;
        public VehicleController(IVehicleService VehicleService, ILogger<VehicleController> logger)
        {
            _VehicleService = VehicleService;
            _logger = logger;
        }

        [HttpGet("GetAllVehicles")]
        public async Task<ActionResult<ServiceResponse<List<Vehicle>>>> GetVehicles()
        {
            _logger.Log(LogLevel.Information, "Invoked GetVehicles Method in controller");

            var result = await _VehicleService.GetVehiclesAsync();

            if (result.Success)
                return Ok(result);
            else
                return StatusCode(500, $"Internal server error {result.Message}");
        }


        [HttpPut("UpdateVehicle/{id}")]
        public async Task<ActionResult<ServiceResponse<Vehicle>>> UpdateVehicle([FromBody] Vehicle vehicle)
        {

            var result = await _VehicleService.UpdateVehicleAsync(vehicle);

            if (result.Success)
                return Ok(result);
            else
                return StatusCode(500, $"Internal server error {result.Message}");
        }

        [HttpPost("NewVehicle")]
        public async Task<ActionResult<ServiceResponse<Vehicle>>> CreateVehicle([FromBody] Vehicle vehicle)
        {
            var result = await _VehicleService.CreateVehicleAsync(vehicle);

            if (result.Success)
                return Ok(result);
            else
                return StatusCode(500, $"Internal server error {result.Message}");
        }

        // http:localhost/api/Vehicle/delete?id=4
        // http:localhost/api/Vehicle/4 dla api REST
        [HttpDelete("DeleteVehicle/{id}")]
        public async Task<ActionResult<ServiceResponse<bool>>> DeleteVehicle([FromRoute] int id)
        {
            var result = await _VehicleService.DeleteVehicleAsync(id);

            if (result.Success)
                return Ok(result);
            else
                return StatusCode(500, $"Internal server error {result.Message}");
        }


    }
}
