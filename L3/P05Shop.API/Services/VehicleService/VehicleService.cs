using P06Shop.Shared;
using P06Shop.Shared.Services.VehicleService;
using P06Shop.Shared.VehicleDealership;
using P07Shop.DataSeeder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace P05Shop.API.Services.VehicleService
{
    public class VehicleService : IVehicleService
    {
        private List<Vehicle> _vehicles; // Przechowuje listę pojazdów

        public VehicleService()
        {
            // Inicjalizacja listy pojazdów przy użyciu danych z seeder-a
            _vehicles = VehicleSeeder.GenerateVehicleData();
        }

        public async Task<ServiceResponse<List<Vehicle>>> GetVehiclesAsync()
        {
            try
            {
                var response = new ServiceResponse<List<Vehicle>>()
                {
                    Data = _vehicles,
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
                    Message = "Problem with dataseeder library",
                    Success = false
                };
            }
        }


        public ServiceResponse<Vehicle> CreateVehicle(Vehicle newVehicle)
        {
            try
            {
                var newId = _vehicles.Count > 0 ? _vehicles.Max(v => v.Id) + 1 : 1; // Sprawdza, czy istnieją już pojazdy w liście
                newVehicle.Id = newId; // Przypisuje nowy identyfikator nowemu pojazdowi
                _vehicles.Add(newVehicle); // Dodaje nowy pojazd do listy

                var response = new ServiceResponse<Vehicle>()
                {
                    Data = newVehicle,
                    Message = "New vehicle added",
                    Success = true
                };

                return response;
            }
            catch (Exception ex)
            {
                return new ServiceResponse<Vehicle>()
                {
                    Data = null,
                    Message = $"Failed to add new vehicle: {ex.Message}",
                    Success = false
                };
            }
        }



        public Vehicle GetVehicleById(int id)
        {
            // Zwraca pojazd na podstawie określonego identyfikatora (id)
            return _vehicles.FirstOrDefault(v => v.Id == id);
        }

        public void DeleteVehicle(int id)
        {
            var existingVehicle = _vehicles.FirstOrDefault(v => v.Id == id);
            if (existingVehicle != null)
            {
                _vehicles.Remove(existingVehicle);
            }
            else
            {
                throw new KeyNotFoundException($"Vehicle with ID {id} not found");
            }
        }


        public void UpdateVehicle(Vehicle updatedVehicle)
        {
            var existingVehicle = _vehicles.FirstOrDefault(v => v.Id == updatedVehicle.Id);
            if (existingVehicle != null)
            {
                existingVehicle.Name = updatedVehicle.Name;
                existingVehicle.Fuel = updatedVehicle.Fuel;
            }
            else
            {
                throw new KeyNotFoundException($"Vehicle with ID {updatedVehicle.Id} not found");
            }
        }
    }
}
