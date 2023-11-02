using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.VisualBasic.FileIO;
using Newtonsoft.Json;
using P04WeatherForecastAPI.Client.Configuration;
using P06Shop.Shared;
using P06Shop.Shared.Services.VehicleService;
using P06Shop.Shared.VehicleDealership;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace P04WeatherForecastAPI.Client.Services.VehicleServices
{
    internal class VehicleService : IVehicleService
    {
        private readonly HttpClient _httpClient;
        private readonly AppSettings _appSettings;
        public VehicleService(HttpClient httpClient, IOptions<AppSettings> appSettings)
        {
            _httpClient = httpClient;
            _appSettings = appSettings.Value;
        }

        public async Task<ServiceResponse<List<Vehicle>>> GetVehiclesAsync()
        {
            var baseEndpoint = _appSettings.BaseAPIUrl + "/" + _appSettings.BaseVehicleEndpoint.Base_url;
            var fullEndpoint = baseEndpoint + _appSettings.BaseVehicleEndpoint.GetAllVehiclesEndpoint;

            var response = await _httpClient.GetAsync(fullEndpoint);
            var json = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<ServiceResponse<List<Vehicle>>>(json);
            return result;
        }

        public Vehicle GetVehicleById(int id)
        {
            throw new NotImplementedException("This method is not implemented.");
        }

        public void DeleteVehicle(int id)
        {
            throw new NotImplementedException("This method is not implemented.");

        }

        public void UpdateVehicle(Vehicle updatedVehicle)
        {
            throw new NotImplementedException("This method is not implemented.");

        }

        public ServiceResponse<Vehicle> CreateVehicle(Vehicle newVehicle)
        {
            throw new NotImplementedException("This method is not implemented.");
        }

    }
}
