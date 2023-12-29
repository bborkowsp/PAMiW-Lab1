using P05VehicleDealership.API.VehicleDealership;
using P05VehicleDealership.API;

namespace P05VehicleDealership.API.Services.VehicleDealershipService
{
    public interface IVehicleDealershipService
    {
        Task<ServiceResponse<List<Vehicle>>> GetVehiclesAsync();

        Task<ServiceResponse<Vehicle>> UpdateVehicleAsync(Vehicle product);

        Task<ServiceResponse<bool>> DeleteVehicleAsync(int id);

        Task<ServiceResponse<Vehicle>> CreateVehicleAsync(Vehicle product);

        Task<ServiceResponse<Vehicle>> GetVehicleByIdAsync(int id);
        Task<ServiceResponse<List<Vehicle>>> SearchVehiclesAsync(string text, int page, int pageSize);

        void SetAuthToken(string authToken);

    }
}
