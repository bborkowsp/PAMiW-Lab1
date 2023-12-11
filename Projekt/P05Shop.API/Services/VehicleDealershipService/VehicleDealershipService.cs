using Microsoft.EntityFrameworkCore;
using P05Shop.API.Models;
using P06Shop.Shared;
using P06Shop.Shared.Services.VehicleDealershipService;
using P06Shop.Shared.VehicleDealership;
using P07Shop.DataSeeder;

namespace P05Shop.API.Services.VehicleDealershipService
{
    public class VehicleDealershipService : IVehicleDealershipService
    {
        private readonly DataContext _dataContext;
        public VehicleDealershipService(DataContext context)
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
            //var bookToDelete = _dataContext.Books.FirstOrDefault(x => x.Id == id);
            //_dataContext.Books.Remove(bookToDelete);  

            // sposób 2: (uzywamy attach : tylko jedno zapytanie do bazy)
            var vehicle = new Vehicle() { Id = id };
            _dataContext.Vehicles.Attach(vehicle);
            _dataContext.Vehicles.Remove(vehicle);
            await _dataContext.SaveChangesAsync();

            return new ServiceResponse<bool>() { Data = true, Success = true };
        }

        public async Task<ServiceResponse<Vehicle>> GetVehicleByIdAsync(int id)
        {
            try
            {
                var vehicle = await _dataContext.Vehicles.FindAsync(id);
                var response = new ServiceResponse<Vehicle>()
                {
                    Data = vehicle,
                    Message = "Ok",
                    Success = true
                };

                return response;
            }
            catch (Exception)
            {
                return new ServiceResponse<Vehicle>()
                {
                    Data = null,
                    Message = "Problem with database",
                    Success = false
                };
            }
        }

        public async Task<ServiceResponse<List<Vehicle>>> GetVehiclesAsync()
        {
            var books = await _dataContext.Vehicles.ToListAsync();

            try
            {
                var response = new ServiceResponse<List<Vehicle>>()
                {
                    Data = books,
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

        public async Task<ServiceResponse<List<Vehicle>>> SearchVehiclesAsync(string text, int page, int pageSize)
        {
            IQueryable<Vehicle> query = _dataContext.Vehicles;

            if (!string.IsNullOrEmpty(text))
                query = query.Where(x => x.Model.Contains(text) || x.Fuel.Contains(text));

            var books = await query
                .Skip(pageSize * (page - 1))
                .Take(pageSize)
                .ToListAsync();

            try
            {
                var response = new ServiceResponse<List<Vehicle>>()
                {
                    Data = books,
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
                var bookToEdit = new Vehicle() { Id = vehicle.Id };
                _dataContext.Vehicles.Attach(bookToEdit);

                bookToEdit.Model = vehicle.Model;
                bookToEdit.Fuel = vehicle.Fuel;


                await _dataContext.SaveChangesAsync();
                return new ServiceResponse<Vehicle> { Data = bookToEdit, Success = true };
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
