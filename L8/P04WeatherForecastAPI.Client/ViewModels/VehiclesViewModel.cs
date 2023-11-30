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



namespace P04WeatherForecastAPI.Client.ViewModels
{
    public partial class VehiclesViewModel : ObservableObject
    {
        private int _currentPage = 0;
        private const int PageSize = 2;
        private readonly IVehicleDealershipService _vehicleService;
        private readonly VehicleDetailsView _vehicleDetailsView;
        private readonly IMessageDialogService _messageDialogService;

        public ObservableCollection<Vehicle> Vehicles { get; set; }
        private List<Vehicle> _allVehicles;



        [ObservableProperty]
        private Vehicle selectedVehicle;


        public VehiclesViewModel(IVehicleDealershipService vehicleService, VehicleDetailsView vehicleDetailsView, IMessageDialogService messageDialogService)
        {
            _messageDialogService = messageDialogService;
            _vehicleDetailsView = vehicleDetailsView;
            _vehicleService = vehicleService;
            Vehicles = new ObservableCollection<Vehicle>();
        }

        public async Task GetVehicles()
        {
            var vehiclesResult = await _vehicleService.GetVehiclesAsync();
            if (vehiclesResult.Success)
            {
                _allVehicles = vehiclesResult.Data;
                _currentPage = 0;
                LoadCurrentPage();
            }
            else
            {
                // Handle error
                _messageDialogService.ShowMessage(vehiclesResult.Message);
            }
        }

        public async Task CreateVehicle()
        {
            var newVehicle = new Vehicle()
            {
                Model = selectedVehicle.Model,
                Fuel = selectedVehicle.Fuel,

            };

            var result = await _vehicleService.CreateVehicleAsync(newVehicle);
            if (result.Success)
                await GetVehicles();
            else
                _messageDialogService.ShowMessage(result.Message);
        }

        public async Task UpdateVehicle()
        {
            var vehicleToUpdate = new Vehicle()
            {
                Id = selectedVehicle.Id,
                Model = selectedVehicle.Model,
                Fuel = selectedVehicle.Fuel,

            };

            await _vehicleService.UpdateVehicleAsync(vehicleToUpdate);
            GetVehicles();
        }

        public async Task DeleteVehicle()
        {
            await _vehicleService.DeleteVehicleAsync(selectedVehicle.Id);
            await GetVehicles();
        }

        [RelayCommand]
        public async Task ShowDetails(Vehicle vehicle)
        {
            _vehicleDetailsView.Show();
            _vehicleDetailsView.DataContext = this;
            //selectedVehicle = vehicle;
            //OnPropertyChanged("SelectedVehicle");
            SelectedVehicle = vehicle;
        }


        [RelayCommand]
        public async Task Save()
        {
            if (selectedVehicle.Id == 0)
            {
                CreateVehicle();
            }
            else
            {
                UpdateVehicle();
            }

        }

        [RelayCommand]
        public async Task Delete()
        {
            DeleteVehicle();
        }

        [RelayCommand]
        public async Task New()
        {
            _vehicleDetailsView.Show();
            _vehicleDetailsView.DataContext = this;
            //selectedVehicle = new Vehicle();
            //OnPropertyChanged("SelectedVehicle");
            SelectedVehicle = new Vehicle();
        }









        private void LoadCurrentPage()
        {
            Vehicles.Clear();
            int startIndex = _currentPage * PageSize;
            int endIndex = Math.Min(startIndex + PageSize, _allVehicles.Count);

            for (int i = startIndex; i < endIndex; i++)
            {
                Vehicles.Add(_allVehicles[i]);
            }
        }


        public RelayCommand NextPageCommand => new RelayCommand(() =>
        {
            if ((_currentPage + 1) * PageSize < _allVehicles.Count)
            {
                _currentPage++;
                LoadCurrentPage();
            }
        });

        public RelayCommand PreviousPageCommand => new RelayCommand(() =>
        {
            if (_currentPage > 0)
            {
                _currentPage--;
                LoadCurrentPage();
            }
        });

    }
}

