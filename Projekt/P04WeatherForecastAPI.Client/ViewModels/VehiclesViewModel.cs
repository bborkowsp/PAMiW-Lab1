using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using P04WeatherForecastAPI.Client.Models;
using P04WeatherForecastAPI.Client.Services.SpeechService;
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
using System.Windows;
using System.Windows.Documents;
using System.Diagnostics;

namespace P04WeatherForecastAPI.Client.ViewModels
{
    public partial class VehiclesViewModel : ObservableObject
    {
        private readonly IVehicleDealershipService _ivehicleDealershipService;
        private readonly VehicleDetailsView _vehicleDetailsView;
        private readonly IMessageDialogService _messageDialogService;

        private int _currentPage = 1;
        public List<Vehicle> vehicleList { get; set; }
        public ObservableCollection<Vehicle> PageVehicles { get; set; }

        [ObservableProperty]
        private Vehicle selectedVehicle;



        public VehiclesViewModel(IVehicleDealershipService ivehicleDealershipService, VehicleDetailsView vehicleDetailsView, IMessageDialogService messageDialogService)
        {
            _ivehicleDealershipService = ivehicleDealershipService;
            _messageDialogService = messageDialogService;
            _vehicleDetailsView = vehicleDetailsView;
            PageVehicles = new ObservableCollection<Vehicle>();
            vehicleList = new List<Vehicle>();
        }

        public async Task GetVehicles()
        {
            vehicleList.Clear();
            var vehiclesResult = await _ivehicleDealershipService.GetVehiclesAsync();
            if (vehiclesResult.Success)
            {
                foreach (var vehicle in vehiclesResult.Data)
                {
                    vehicleList.Add(vehicle);
                }
                LoadVehiclesOnPage();
            }
        }

        public void LoadVehiclesOnPage()
        {
            PageVehicles.Clear();
            
            int ItemsPerPage = 4;
     
            MaxPage = Convert.ToInt32(Math.Ceiling(Convert.ToDouble(vehicleList.Count) / Convert.ToDouble(ItemsPerPage)));
            if (MaxPage == 0)
            {
                MaxPage = 1;
            }
            
            for (int i = (CurrentPage - 1) * ItemsPerPage; i < (CurrentPage - 1) * ItemsPerPage + ItemsPerPage; i++)
            {
                if (i > vehicleList.Count - 1)
                {
                    break;
                }
                PageVehicles.Add(vehicleList[i]);
            }
        }

        [ObservableProperty]
        public int maxPage;

        public int CurrentPage
        {
            get => _currentPage;
            set
            {
                _currentPage = value;
                LoadVehiclesOnPage();
                OnPropertyChanged();
            }
        }

        [RelayCommand]
        public async void NextPage()
        {
            if (CurrentPage < MaxPage)
            {
                CurrentPage++;
                LoadVehiclesOnPage();
                OnPropertyChanged();
            }
        }

        [RelayCommand]
        public async void PreviousPage()
        {
            if (CurrentPage > 1)
            {
                CurrentPage--;
                LoadVehiclesOnPage();
                OnPropertyChanged();
            }
        }






        public async Task<bool> CreateVehicle()
        {
            var newVehicle  = new Vehicle()
            {
                Model = selectedVehicle.Model,
                Fuel = selectedVehicle.Fuel,

            };

            var result = await _ivehicleDealershipService.CreateVehicleAsync(newVehicle);
            if (result.Success)
            {
                await GetVehicles();
                return true;
            }
            else
            {
                _messageDialogService.ShowMessage(result.Message);
                return false;
            }
        }

        public async Task<bool> UpdateVehicle()
        {
            var vehicleToUpdate = new Vehicle()
            {
                Id = selectedVehicle.Id,
                Model = selectedVehicle.Model,
                Fuel = selectedVehicle.Fuel,

            };

            var res = await _ivehicleDealershipService.UpdateVehicleAsync(vehicleToUpdate);
            GetVehicles();

            if (!res.Success)
            {
                _messageDialogService.ShowMessage(res.Message);
            }

            return res.Success;
        }

        public async Task<bool> DeleteVehicle()
        {
            var res = await _ivehicleDealershipService.DeleteVehicleAsync(selectedVehicle.Id);
            await GetVehicles();

            if (!res.Success)
            {
                _messageDialogService.ShowMessage(res.Message);
            }

            return res.Success;
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
            bool success;
            if (selectedVehicle.Id == 0)
            {
                success = await CreateVehicle();
            }
            else
            {
                success = await UpdateVehicle();
            }
            if (success)
                _vehicleDetailsView.Hide();
        }

        [RelayCommand]
        public async Task Delete()
        {
            bool success = await DeleteVehicle();
            if (success)
            {
                _vehicleDetailsView.Hide();
            }
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

    }
}
