using P06Shop.Shared;
using P06Shop.Shared.Services.VehicleService;
using P06Shop.Shared.VehicleDealership;
using Microsoft.EntityFrameworkCore;

using P05Shop.API.Models;

namespace P05Shop.API.Services.VehicleService
{
    public class VehicleService : IVehicleService
    {
        private readonly DataContext _dataContext;
        public VehicleService(DataContext context)
        {
            _dataContext = context;
        }

        public async Task<ServiceResponse<Vehicle>> CreateVehicleAsync(Vehicle vehicle)
        {
            try
            {
                _dataContext.Vehicles.Add(vehicle);
                await _dataContext.SaveChangesAsync();
                return new ServiceResponse<Vehicle>() { Data = vehicle, Success = true };
            }
            catch (Exception)
            {
                return new ServiceResponse<Vehicle>()
                {
                    Data = null,
                    Success = false,
                    Message = "Cannot create vehicle"
                };
            }

        }

        public async Task<ServiceResponse<bool>> DeleteVehicleAsync(int id)
        {
            // sposób 1 (najpierw znajdujemy potem go usuwamy): 
            //var vehicleToDelete = _dataContext.Vehicles.FirstOrDefault(x => x.Id == id);
            //_dataContext.Vehicles.Remove(vehicleToDelete);  

            // sposób 2: (uzywamy attach : tylko jedno zapytanie do bazy)
            var vehicle = new Vehicle() { Id = id };
            _dataContext.Vehicles.Attach(vehicle);
            _dataContext.Vehicles.Remove(vehicle);
            await _dataContext.SaveChangesAsync();

            return new ServiceResponse<bool>() { Data = true, Success = true };
        }

        public async Task<ServiceResponse<List<Vehicle>>> GetVehiclesAsync()
        {
            var vehicles = await _dataContext.Vehicles.ToListAsync();

            try
            {
                var response = new ServiceResponse<List<Vehicle>>()
                {
                    Data = vehicles,
                    Message = "Ok",
                    Success = true
                };

                return response;
            }
            catch (Exception)
            {
                return new ServiceResponse<List<Vehicle>>()
                {
                    Data = null,
                    Message = "Problem with database",
                    Success = false
                };
            }

        }

        public async Task<ServiceResponse<Vehicle>> UpdateVehicleAsync(Vehicle vehicle)
        {
            try
            {
                var vehicleToEdit = new Vehicle() { Id = vehicle.Id };
                _dataContext.Vehicles.Attach(vehicleToEdit);

                vehicleToEdit.Model = vehicle.Model;
                vehicleToEdit.Fuel = vehicle.Fuel;


                await _dataContext.SaveChangesAsync();
                return new ServiceResponse<Vehicle> { Data = vehicleToEdit, Success = true };
            }
            catch (Exception)
            {
                return new ServiceResponse<Vehicle>
                {
                    Data = null,
                    Success = false,
                    Message = "An error occured while updating vehicle"
                };
            }



        }
    }
}
