using P06Shop.Shared;
using P06Shop.Shared.VehicleDealership;

namespace P06Shop.Shared.Services.VehicleService
{
    public interface IVehicleService
    {
        Task<ServiceResponse<List<Vehicle>>> GetVehiclesAsync();
        Vehicle GetVehicleById(int id);
        void DeleteVehicle(int id);
        void UpdateVehicle(Vehicle updatedVehicle);
        ServiceResponse<Vehicle> CreateVehicle(Vehicle newVehicle);


    }
}
