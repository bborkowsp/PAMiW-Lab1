using CommunityToolkit.Mvvm.ComponentModel;
using P04WeatherForecastAPI.Client.Models;
using P04WeatherForecastAPI.Client.Services.WeatherServices;
using P06Shop.Shared.Services.VehicleService;
using P06Shop.Shared.VehicleDealership;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Documents;


namespace P04WeatherForecastAPI.Client.ViewModels
{
    public partial class VehiclesViewModel : ObservableObject
    {
        private int _currentPage = 0;
        private const int PageSize = 5;
        private readonly IVehicleService _VehicleService;

        public ObservableCollection<Vehicle> Vehicles { get; set; }
        public ObservableCollection<Vehicle> DisplayedVehicles { get; set; }


        public VehiclesViewModel(IVehicleService VehicleService)
        {
            _VehicleService = VehicleService;
            Vehicles = new ObservableCollection<Vehicle>();
            DisplayedVehicles = new ObservableCollection<Vehicle>();

        }

        public async void GetVehicles()
        {
            var VehiclesResult = await _VehicleService.GetVehiclesAsync();
            if (VehiclesResult.Success)
            {
                foreach (var p in VehiclesResult.Data)
                {
                    Vehicles.Add(p);
                }
            }
            var allVehicles = Vehicles.ToList();
            DisplayedVehicles.Clear();

            // Oblicz bieżącą stronę
            var startIndex = _currentPage * PageSize;
            var endIndex = startIndex + PageSize;
            if (endIndex > allVehicles.Count)
            {
                endIndex = allVehicles.Count;
            }

            // Dodaj produkty do wyświetlenia na bieżącej stronie
            for (int i = startIndex; i < endIndex; i++)
            {
                DisplayedVehicles.Add(allVehicles[i]);
            }
        }

        public ICommand NextPageCommand => new RelayCommand(NextPage);

        private void NextPage()
        {
            _currentPage++;
            GetVehicles();
        }

        public ICommand PreviousPageCommand => new RelayCommand(PreviousPage);

        private void PreviousPage()
        {
            if (_currentPage > 0)
            {
                _currentPage--;
                GetVehicles();
            }
        }

        internal class RelayCommand : ICommand
        {
            private readonly Action _execute;
            private readonly Func<bool> _canExecute;

            public RelayCommand(Action execute, Func<bool> canExecute = null)
            {
                _execute = execute ?? throw new ArgumentNullException(nameof(execute));
                _canExecute = canExecute;
            }

            public bool CanExecute(object parameter)
            {
                return _canExecute?.Invoke() ?? true;
            }

            public void Execute(object parameter)
            {
                _execute();
            }

            // ICommand implementation
            public event EventHandler CanExecuteChanged
            {
                add { CommandManager.RequerySuggested += value; }
                remove { CommandManager.RequerySuggested -= value; }
            }
        }
    }
}

