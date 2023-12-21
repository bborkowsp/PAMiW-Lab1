using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Maui.ApplicationModel;
using P12MAUI.Client.ViewModels;
using P06Shop.Shared.MessageBox;
using P06Shop.Shared.Services.VehicleDealershipService;
using P06Shop.Shared.VehicleDealership;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P12MAUI.Client.ViewModels
{
    [QueryProperty(nameof(Vehicle), nameof(Vehicle))]
    [QueryProperty(nameof(VehiclesViewModel), nameof(VehiclesViewModel))]
    public partial class VehicleDetailsViewModel : ObservableObject
    {
        private readonly IVehicleDealershipService _vehicleDealershipService;
        private readonly IMessageDialogService _messageDialogService;
        private readonly IGeolocation _geolocation;
        private readonly IMap _map;
        private VehiclesViewModel _vehicleViewModel;

        public VehicleDetailsViewModel(IVehicleDealershipService vehicleDealership, IMessageDialogService messageDialogService, IGeolocation geolocation, IMap map)
        {
            _map = map;
            _vehicleDealershipService = vehicleDealership;
            _messageDialogService = messageDialogService;
            _geolocation = geolocation;
        }

        public VehiclesViewModel VehiclesViewModel
        {
            get
            {
                return _vehicleViewModel;
            }
            set
            {
                _vehicleViewModel = value;
            }
        }


        [ObservableProperty]
        Vehicle vehicle;

        public async Task DeleteVehicle()
        {
            await _vehicleDealershipService.DeleteVehicleAsync(vehicle.Id);
            await _vehicleViewModel.GetVehicles();
        }

        public async Task CreateVehicle()
        {
            var newVehicle = new Vehicle()
            {
                Model = vehicle.Model,
                Fuel = vehicle.Fuel,

            };

            var result = await _vehicleDealershipService.CreateVehicleAsync(newVehicle);
            if (result.Success)
                await _vehicleViewModel.GetVehicles();
            else
                _messageDialogService.ShowMessage(result.Message);
        }

        public async Task UpdateVehicle()
        {
            var vehicleToUpdate = new Vehicle()
            {
                Id = vehicle.Id,
                Model = vehicle.Model,
                Fuel = vehicle.Fuel,
            };

            await _vehicleDealershipService.UpdateVehicleAsync(vehicleToUpdate);
            await _vehicleViewModel.GetVehicles();
        }


        [RelayCommand]
        public async Task Save()
        {
            if (vehicle.Id == 0)
            {
                CreateVehicle();
                await Shell.Current.GoToAsync("../", true);

            }
            else
            {
                UpdateVehicle();
                await Shell.Current.GoToAsync("../", true);
            }

        }

        [RelayCommand]
        public async Task Delete()
        {
            DeleteVehicle();
            await Shell.Current.GoToAsync("../", true);
        }
    }
}
