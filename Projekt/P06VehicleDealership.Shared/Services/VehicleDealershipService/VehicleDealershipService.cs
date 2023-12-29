
using Microsoft.Extensions.Options;
using Microsoft.VisualBasic.FileIO;
using Newtonsoft.Json;
using P06VehicleDealership.Shared;
using P06VehicleDealership.Shared.Configuration;
using P06VehicleDealership.Shared.VehicleDealership;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace P06VehicleDealership.Shared.Services.VehicleDealershipService
{
    public class VehicleDealershipService : IVehicleDealershipService
    {

        private readonly HttpClient _httpClient;
        private readonly AppSettings _appSettings;
        public VehicleDealershipService(HttpClient httpClient, AppSettings appSettings)
        {
            _httpClient = httpClient;
            _appSettings = appSettings;
        }

        public async Task<ServiceResponse<Vehicle>> CreateVehicleAsync(Vehicle vehicle)
        {
            //var response = await _httpClient.PostAsJsonAsync(_appSettings.VehicleDealershipEndpoints.GetVehiclesEndpoint, vehicle);
            var url = _appSettings.BaseAPIUrl + "/" + _appSettings.VehicleDealershipEndpoints.GetVehiclesEndpoint;
            var response = await _httpClient.PostAsJsonAsync(url, vehicle);
            var result = await response.Content.ReadFromJsonAsync<ServiceResponse<Vehicle>>();
            return result;
        }

        public async Task<ServiceResponse<bool>> DeleteVehicleAsync(int id)
        {
            // jesli uzyjemy / na poczatku to wtedy sciezka trakktowana jest od root czyli pomija czesc środkową adresu 
            // zazwyczaj unikamy stosowania / na poczatku 
            //var response = await _httpClient.DeleteAsync($"{id}");
            var url = _appSettings.BaseAPIUrl + "/" + String.Format(_appSettings.VehicleDealershipEndpoints.DeleteVehicleEndpoint, id);
            var response = await _httpClient.DeleteAsync(url);
            var result = await response.Content.ReadFromJsonAsync<ServiceResponse<bool>>();
            return result;
        }






        // alternatywny sposób pobierania danych 
        public async Task<ServiceResponse<List<Vehicle>>> GetVehiclesAsync()
        {
            // "https://localhost:7230/api/Vehicle"
            var url = _appSettings.BaseAPIUrl + "/" + _appSettings.VehicleDealershipEndpoints.GetVehiclesEndpoint;
            try
            {
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
            catch (JsonException)
            {
                return new ServiceResponse<List<Vehicle>>
                {
                    Success = false,
                    Message = "Cannot deserialize data"
                };
            }
            catch (Exception)
            {
                return new ServiceResponse<List<Vehicle>>
                {
                    Success = false,
                    Message = "Network error"
                };
            }
        }

        public async Task<ServiceResponse<Vehicle>> GetVehicleByIdAsync(int id)
        {
            var url = _appSettings.BaseAPIUrl + "/" + String.Format(_appSettings.VehicleDealershipEndpoints.GetVehicleEndpoint, id);
            var response = await _httpClient.GetAsync(url);




            //var response = await _httpClient.GetAsync(id.ToString());
            if (!response.IsSuccessStatusCode)
                return new ServiceResponse<Vehicle>
                {
                    Success = false,
                    Message = "HTTP request failed"
                };

            var json = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<ServiceResponse<Vehicle>>(json)
                ?? new ServiceResponse<Vehicle> { Success = false, Message = "Deserialization failed" };

            result.Success = result.Success && result.Data != null;

            return result;
        }



        // wersja 2 
        public async Task<ServiceResponse<Vehicle>> UpdateVehicleAsync(Vehicle vehicle)
        {
            //var response = await _httpClient.PutAsJsonAsync(_appSettings.VehicleDealershipEndpoints.GetVehiclesEndpoint, vehicle);
            var url = _appSettings.BaseAPIUrl + "/" + _appSettings.VehicleDealershipEndpoints.GetVehiclesEndpoint;
            var content = new StringContent(JsonConvert.SerializeObject(vehicle), Encoding.UTF8, "application/json");
            var response = await _httpClient.PutAsync(url, content);

            var result = await response.Content.ReadFromJsonAsync<ServiceResponse<Vehicle>>();
            return result;
        }

        public async Task<ServiceResponse<List<Vehicle>>> SearchVehiclesAsync(string text, int page, int pageSize)
        {
            Console.WriteLine("Service -> sending request");
            try
            {
                Console.WriteLine("Check 1");
                string searchUrl = string.IsNullOrWhiteSpace(text) ? "" : $"/{text}";
                Console.WriteLine("Check 2");
                var url = _appSettings.BaseAPIUrl + "/" + _appSettings.VehicleDealershipEndpoints.SearchVehiclesEndpoint + searchUrl + $"/{page}/{pageSize}";
                Console.WriteLine("Sending request to " + url);
                var response = await _httpClient.GetAsync(url);
                Console.WriteLine("Result: " + response.IsSuccessStatusCode + " -> " + response.RequestMessage);
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
            catch (JsonException)
            {
                return new ServiceResponse<List<Vehicle>>
                {
                    Success = false,
                    Message = "Cannot deserialize data"
                };
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

                return new ServiceResponse<List<Vehicle>>
                {
                    Success = false,
                    Message = "Network error"
                };
            }
        }

        public void SetAuthToken(string authToken)
        {
            Debug.WriteLine("Setting auth token...");
            if (authToken == null || authToken == "")
            {
                _httpClient.DefaultRequestHeaders.Authorization = null;
                return;
            }
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", authToken);
        }
    }
}
