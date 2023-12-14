using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using P06Shop.Shared.MessageBox;
using P06Shop.Shared.Services.VehicleDealershipService;
using P06Shop.Shared.VehicleDealership;
using P12MAUI.Client;
using P12MAUI.Client.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using static System.Reflection.Metadata.BlobBuilder;


namespace P04WeatherForecastAPI.Client.ViewModels
{
   
 public partial class VehiclesViewModel : ObservableObject
    {
        private readonly IVehicleDealershipService _vehicleDealershipService;
        private readonly VehicleDetailsView _vehicleDetailsView;
        private readonly IMessageDialogService _messageDialogService;
        private readonly IConnectivity _connectivity;

        private int _currentPage = 1;
        public List<Vehicle> AllVehicles { get; set; }
        public ObservableCollection<Vehicle> PageVehicles { get; set; }

        [ObservableProperty]
        private Vehicle selectedVehicle;

        public VehiclesViewModel(IVehicleDealershipService vehicleDealership, VehicleDetailsView vehicleDetailsView, IMessageDialogService messageDialogService,
            IConnectivity connectivity)
        {
            _messageDialogService = messageDialogService;
            _vehicleDetailsView = vehicleDetailsView;
            _vehicleDealershipService = vehicleDealership;
            _connectivity = connectivity; // set the _connectivity field
            PageVehicles = new ObservableCollection<Vehicle>();
            AllVehicles = new List<Vehicle>();
            GetVehicles();
        }

        public async Task GetVehicles()
        {
            AllVehicles.Clear();
            
            if (_connectivity.NetworkAccess != NetworkAccess.Internet)
            {
                _messageDialogService.ShowMessage("Internet not available!");
                return;
            }
            
            var vehiclesResult = await _vehicleDealershipService.GetVehiclesAsync();
            if (vehiclesResult.Success)
            {
                foreach (var p in vehiclesResult.Data)
                {
                    AllVehicles.Add(p);
                }
                LoadVehiclesOnPage();
            }
            else
            {
                _messageDialogService.ShowMessage(vehiclesResult.Message);
            }
        }

        public void LoadVehiclesOnPage()
        {
            PageVehicles.Clear();

            int ItemsPerPage = 700;

            MaxPage = Convert.ToInt32(Math.Ceiling(Convert.ToDouble(AllVehicles.Count) / Convert.ToDouble(ItemsPerPage)));
            if (MaxPage == 0)
            {
                MaxPage = 1;
            }

            for (int i = (CurrentPage - 1) * ItemsPerPage; i < (CurrentPage - 1) * ItemsPerPage + ItemsPerPage; i++)
            {
                if (i > AllVehicles.Count - 1)
                {
                    break;
                }
                PageVehicles.Add(AllVehicles[i]);
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


        [RelayCommand]
        public async Task ShowDetails(Vehicle vehicle)
        {
            if (_connectivity.NetworkAccess != NetworkAccess.Internet)
            {
                _messageDialogService.ShowMessage("Internet not available!");
                return;
            }

            await Shell.Current.GoToAsync(nameof(VehicleDetailsView), true, new Dictionary<string, object>
            {
                {"Vehicle", vehicle },
                {nameof(VehiclesViewModel), this }
            });
            SelectedVehicle = vehicle;
        }

        [RelayCommand]
        public async Task New()
        {
            if (_connectivity.NetworkAccess != NetworkAccess.Internet)
            {
                _messageDialogService.ShowMessage("Internet not available!");
                return;
            }

            SelectedVehicle = new Vehicle();
            await Shell.Current.GoToAsync(nameof(VehicleDetailsView), true, new Dictionary<string, object>
            {
                {"Vehicle", SelectedVehicle },
                {nameof(VehiclesViewModel), this }
            });
        }

    }
}
