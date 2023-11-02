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

        public VehicleController(IVehicleService VehicleService)
        {
            _VehicleService = VehicleService;
        }

        [HttpGet("GetAllVehicles")]
        public async Task<ActionResult<ServiceResponse<List<Vehicle>>>> GetVehicles()
        {
            var result = await _VehicleService.GetVehiclesAsync();

            if (result.Success)
                return Ok(result);
            else
                return StatusCode(500, $"Internal server error {result.Message}");
        }

        [HttpPost("newVehicle")]
        public IActionResult createVehicle([FromBody] Vehicle vehicle)
        {
            var serviceResponse = _VehicleService.CreateVehicle(vehicle);

            if (serviceResponse.Success)
            {
                return Ok(serviceResponse);
            }
            else
            {
                return StatusCode(500, $"Internal server error {serviceResponse.Message}");
            }
        }

        [HttpGet("GetVehicle/{id}")]
        public IActionResult GetVehicle([FromRoute] int id)
        {
            return Ok(new Vehicle() { Name = "Fiat" });
        }

        [HttpPut("UpdateVehicle/{id}")]
        public IActionResult UpdateVehicle(int id, [FromBody] Vehicle updatedVehicle)
        {
            if (string.IsNullOrEmpty(updatedVehicle.Name) || string.IsNullOrEmpty(updatedVehicle.Fuel))
            {
                return BadRequest("Invalid vehicle data");
            }

            try
            {
                var existingVehicle = _VehicleService.GetVehicleById(id);
                if (existingVehicle != null)
                {
                    existingVehicle.Name = updatedVehicle.Name;
                    existingVehicle.Fuel = updatedVehicle.Fuel;
                    _VehicleService.UpdateVehicle(existingVehicle);
                    return Ok($"Vehicle with ID {id} has been successfully updated");
                }
                else
                {
                    return NotFound($"Vehicle with ID {id} not found");
                }
            }
            catch (Exception ex)
            {
                // Zwróć kod błędu serwera w przypadku wystąpienia wyjątku
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }


        [HttpDelete("DeleteVehicle/{id}")]
        public IActionResult DeleteVehicle(int id)
        {
            try
            {
                // Sprawdź, czy istnieje pojazd o podanym identyfikatorze (id)
                var existingVehicle = _VehicleService.GetVehicleById(id);
                if (existingVehicle == null)
                {
                    return NotFound($"Vehicle with ID {id} not found");
                }

                // Wywołaj metodę usuwania pojazdu
                _VehicleService.DeleteVehicle(id);

                // Zwróć odpowiednią odpowiedź HTTP, jeśli usunięcie zakończy się sukcesem
                return Ok($"Vehicle with ID {id} has been successfully deleted");
            }
            catch (Exception ex)
            {
                // Zwróć kod błędu serwera w przypadku wystąpienia wyjątku
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }


    }
}
