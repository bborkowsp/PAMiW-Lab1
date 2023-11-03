using P06Shop.Shared;
using P06Shop.Shared.VehicleDealership;

namespace P06Shop.Shared.Services.VehicleService
{
    public interface IVehicleService
    {
        Task<ServiceResponse<List<Vehicle>>> GetVehiclesAsync();

        Task<ServiceResponse<Vehicle>> UpdateVehicleAsync(Vehicle vehicle);

        Task<ServiceResponse<bool>> DeleteVehicleAsync(int id);

        Task<ServiceResponse<Vehicle>> CreateVehicleAsync(Vehicle vehicle);


    }
}
