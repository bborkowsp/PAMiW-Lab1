using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using P06Shop.Shared;
using P06Shop.Shared.Services.VehicleDealershipService;
using P06Shop.Shared.VehicleDealership;

namespace P05Shop.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VehicleController : Controller
    {
        private readonly IVehicleDealershipService _iVehicleDealershipService;
        private readonly ILogger<VehicleController> _logger;
        public VehicleController(IVehicleDealershipService iVehicleDealershipService, ILogger<VehicleController> logger)
        {
            _iVehicleDealershipService = iVehicleDealershipService;
            _logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult<ServiceResponse<List<Vehicle>>>> GetVehicles()
        {

            var result = await _iVehicleDealershipService.GetVehiclesAsync();

            if (result.Success)
                return Ok(result);
            else
                return  StatusCode(500, $"Internal server error {result.Message}");
        }


        // http:localhost/api/product/4 dla api REST
        [HttpGet("{id}")]
        public async Task<ActionResult<ServiceResponse<Vehicle>>> GetVehicle(int id)
        {
          
            var result = await _iVehicleDealershipService.GetVehicleByIdAsync(id);

            if (result.Success)
                return Ok(result);
            else
                return StatusCode(500, $"Internal server error {result.Message}");
        }


        [HttpPut]
        public async Task<ActionResult<ServiceResponse<Vehicle>>> UpdateVehicle([FromBody] Vehicle product)
        {
            
            var result = await _iVehicleDealershipService.UpdateVehicleAsync(product);

            if (result.Success)
                return Ok(result);
            else
                return StatusCode(500, $"Internal server error {result.Message}");
        }

        [HttpPost]
        public async Task<ActionResult<ServiceResponse<Vehicle>>> CreateVehicle([FromBody] Vehicle product)
        {
            var result = await _iVehicleDealershipService.CreateVehicleAsync(product);

            if (result.Success)
                return Ok(result);
            else
                return StatusCode(500, $"Internal server error {result.Message}");
        }

        // http:localhost/api/product/delete?id=4
        // http:localhost/api/product/4 dla api REST
        [HttpDelete("{id}")]
        public async Task<ActionResult<ServiceResponse<bool>>> DeleteVehicle([FromRoute] int id)
        {
            var result = await _iVehicleDealershipService.DeleteVehicleAsync(id);

            if (result.Success)
                return Ok(result);
            else
                return StatusCode(500, $"Internal server error {result.Message}");
        }


        [HttpGet("search/{text}/{page}/{pageSize}")]
        [HttpGet("search/{page}/{pageSize}")]
        public async Task<ActionResult<ServiceResponse<List<Vehicle>>>> SearchVehicles(string? text = null, int page = 1, int pageSize = 10)
        {

            var result = await _iVehicleDealershipService.SearchVehiclesAsync(text, page, pageSize);

            if (result.Success)
                return Ok(result);
            else
                return StatusCode(500, $"Internal server error {result.Message}");
        }


    }
}
