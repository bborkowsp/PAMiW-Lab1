using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.VisualBasic.FileIO;
using Newtonsoft.Json;
using P06Shop.Shared;
using P06Shop.Shared.Configuration;
using P06Shop.Shared.MessageBox;
using P06Shop.Shared.Services.AuthService;
using P06Shop.Shared.Services.VehicleDealershipService;
using P06Shop.Shared.Services.VehicleDealershipService;
using P06Shop.Shared.VehicleDealership;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http.Json;
using System.Windows;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using P04WeatherForecastAPI.Client.Models;
using P04WeatherForecastAPI.Client.Services.WeatherServices;
using P06Shop.Shared.MessageBox;
using P06Shop.Shared.Services.VehicleDealershipService;
using P06Shop.Shared.VehicleDealership;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Documents;

namespace P04WeatherForecastAPI.Client.Services.VehicleServices
{
    internal class VehicleService : IVehicleDealershipService
    {
        private readonly HttpClient _httpClient;
        private readonly AppSettings _appSettings;
        public VehicleService(HttpClient httpClient, IOptions<AppSettings> appSettings)
        {
            _httpClient = httpClient;
            _appSettings = appSettings.Value;
        }

        public async Task<ServiceResponse<Vehicle>> CreateVehicleAsync(Vehicle vehicle)
        {
            var url = _appSettings.BaseAPIUrl + "/" + _appSettings.VehicleDealershipEndpoints.GetVehiclesEndpoint;
            var response = await _httpClient.PostAsJsonAsync(url, vehicle);
            var result = await response.Content.ReadFromJsonAsync<ServiceResponse<Vehicle>>();
            return result;
        }

        public async Task<ServiceResponse<bool>> DeleteVehicleAsync(int id)
        {
            var url = $"{_appSettings.BaseAPIUrl}/{_appSettings.VehicleDealershipEndpoints.DeleteVehicleEndpoint.Replace("{vehicleId}", id.ToString())}";
            var response = await _httpClient.DeleteAsync(url);
            var result = await response.Content.ReadFromJsonAsync<ServiceResponse<bool>>();
            return result;
        }



        //// skopiowane z postmana 
        //public async Task<ServiceResponse<List<Vehicle>>> GetVehiclesAsync()
        //{
        //    //var client = new HttpClient();   
        //    var request = new HttpRequestMessage(HttpMethod.Get, _appSettings.BaseVehicleEndpoint.GetAllVehiclesEndpoint);
        //    var response = await _httpClient.SendAsync(request);
        //    response.EnsureSuccessStatusCode();        
        //    var json = await response.Content.ReadAsStringAsync();
        //    var result = JsonConvert.DeserializeObject<ServiceResponse<List<Vehicle>>>(json);
        //    return result;
        //}


        // alternatywny sposób pobierania danych 
        public async Task<ServiceResponse<List<Vehicle>>> GetVehiclesAsync()
        {
            var url = _appSettings.BaseAPIUrl + "/" + _appSettings.VehicleDealershipEndpoints.GetVehiclesEndpoint;
            var response = await _httpClient.GetAsync(url);
            if (!response.IsSuccessStatusCode)
                return new ServiceResponse<List<Vehicle>>
                {
                    Success = false,
                    Message = "HTTP request failed"
                };

            var json = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<ServiceResponse<List<Vehicle>>>(json)
                ?? new ServiceResponse<List<Vehicle>> { Success = false, Message = "Deserialization failed" };

            result.Success = result.Success && result.Data != null;

            return result;
        }

        // wersja 1 
        //public async Task<ServiceResponse<Vehicle>> UpdateVehicleAsync(Vehicle vehicle)
        //{
        //    var response = await _httpClient.PutAsJsonAsync(_appSettings.BaseVehicleEndpoint.GetAllVehiclesEndpoint, vehicle);
        //    var json = await response.Content.ReadAsStringAsync();
        //    var result = JsonConvert.DeserializeObject<ServiceResponse<Vehicle>>(json);
        //    return result;
        //}

        // wersja 2 
        public async Task<ServiceResponse<Vehicle>> UpdateVehicleAsync(Vehicle vehicle)
        {
            var url = _appSettings.BaseAPIUrl + "/" + _appSettings.VehicleDealershipEndpoints.GetVehiclesEndpoint;
            var content = new StringContent(JsonConvert.SerializeObject(vehicle), Encoding.UTF8, "application/json");
            var response = await _httpClient.PutAsync(url, content);

            var result = await response.Content.ReadFromJsonAsync<ServiceResponse<Vehicle>>();
            return result;
        }



        public async Task<ServiceResponse<Vehicle>> GetVehicleByIdAsync(int id)
        {


            return null;
        }
                public async Task<ServiceResponse<List<Vehicle>>> SearchVehiclesAsync(string text, int page, int pageSize)
        {
            return null;

        }
    }
}
