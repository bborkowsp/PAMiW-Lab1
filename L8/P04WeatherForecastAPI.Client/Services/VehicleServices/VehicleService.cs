using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.VisualBasic.FileIO;
using Newtonsoft.Json;
using P04WeatherForecastAPI.Client.Configuration;
using P06Shop.Shared;
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
            var baseEndpoint = _appSettings.BaseAPIUrl + _appSettings.BaseVehicleEndpoint.Base_url;
            var fullEndpoint = baseEndpoint + _appSettings.BaseVehicleEndpoint.NewVehicleEndpoint;

            var response = await _httpClient.PostAsJsonAsync(fullEndpoint, vehicle);
            var result = await response.Content.ReadFromJsonAsync<ServiceResponse<Vehicle>>();
            return result;
        }

        public async Task<ServiceResponse<bool>> DeleteVehicleAsync(int id)
        {
            var baseEndpoint = _appSettings.BaseAPIUrl + _appSettings.BaseVehicleEndpoint.Base_url;
            var fullEndpoint = baseEndpoint + _appSettings.BaseVehicleEndpoint.DeleteVehicleEndpoint + $"/{id}";

            var response = await _httpClient.DeleteAsync(fullEndpoint);
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
            var baseEndpoint = _appSettings.BaseAPIUrl + _appSettings.BaseVehicleEndpoint.Base_url;
            var fullEndpoint = baseEndpoint + _appSettings.BaseVehicleEndpoint.GetAllVehiclesEndpoint;

            var response = await _httpClient.GetAsync(fullEndpoint);
            var json = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<ServiceResponse<List<Vehicle>>>(json);
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
            var baseEndpoint = _appSettings.BaseAPIUrl + _appSettings.BaseVehicleEndpoint.Base_url;
            var fullEndpoint = baseEndpoint + _appSettings.BaseVehicleEndpoint.UpdateVehicleEndpoint;

            var response = await _httpClient.PutAsJsonAsync(fullEndpoint, vehicle);
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
