using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using P04WeatherForecastAPI.Client.Models;
using P04WeatherForecastAPI.Client.Services.WeatherServices;
using P06Shop.Shared.MessageBox;
using P06Shop.Shared.Services.VehicleService;
using P06Shop.Shared.VehicleDealership;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Documents;



namespace P04WeatherForecastAPI.Client.ViewModels
{
    public partial class VehiclesViewModel : ObservableObject
    {
        private int _currentPage = 0;
        private const int PageSize = 5;
        private readonly IVehicleService _vehicleService;
        private readonly VehicleDetailsView _vehicleDetailsView;
        private readonly IMessageDialogService _messageDialogService;

        public ObservableCollection<Vehicle> Vehicles { get; set; }



        [ObservableProperty]
        private Vehicle selectedVehicle;


        public VehiclesViewModel(IVehicleService vehicleService, VehicleDetailsView vehicleDetailsView, IMessageDialogService messageDialogService)
        {
            _messageDialogService = messageDialogService;
            _vehicleDetailsView = vehicleDetailsView;
            _vehicleService = vehicleService;
            Vehicles = new ObservableCollection<Vehicle>();
        }

        public async Task GetVehicles()
        {
            Vehicles.Clear();
            var VehiclesResult = await _vehicleService.GetVehiclesAsync();
            if (VehiclesResult.Success)
            {
                foreach (var v in VehiclesResult.Data)
                {
                    Vehicles.Add(v);
                }
            }
            // var allVehicles = Vehicles.ToList();
            // DisplayedVehicles.Clear();

            // // Oblicz bieżącą stronę
            // var startIndex = _currentPage * PageSize;
            // var endIndex = startIndex + PageSize;
            // if (endIndex > allVehicles.Count)
            // {
            //     endIndex = allVehicles.Count;
            // }

            // // Dodaj produkty do wyświetlenia na bieżącej stronie
            // for (int i = startIndex; i < endIndex; i++)
            // {
            //     DisplayedVehicles.Add(allVehicles[i]);
            // }
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




        //     public ICommand NextPageCommand => new RelayCommand(NextPage);

        //     private void NextPage()
        //     {
        //         _currentPage++;
        //         GetVehicles();
        //     }

        //     public ICommand PreviousPageCommand => new RelayCommand(PreviousPage);

        //     private void PreviousPage()
        //     {
        //         if (_currentPage > 0)
        //         {
        //             _currentPage--;
        //             GetVehicles();
        //         }
        //     }

        //     internal class RelayCommand : ICommand
        //     {
        //         private readonly Action _execute;
        //         private readonly Func<bool> _canExecute;

        //         public RelayCommand(Action execute, Func<bool> canExecute = null)
        //         {
        //             _execute = execute ?? throw new ArgumentNullException(nameof(execute));
        //             _canExecute = canExecute;
        //         }

        //         public bool CanExecute(object parameter)
        //         {
        //             return _canExecute?.Invoke() ?? true;
        //         }

        //         public void Execute(object parameter)
        //         {
        //             _execute();
        //         }

        //         // ICommand implementation
        //         public event EventHandler CanExecuteChanged
        //         {
        //             add { CommandManager.RequerySuggested += value; }
        //             remove { CommandManager.RequerySuggested -= value; }
        //         }
        //     }
    }
}

