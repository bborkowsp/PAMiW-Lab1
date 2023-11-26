using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using P04WeatherForecastAPI.Client.Models;
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
        private readonly IVehicleDealershipService _productService;
        private readonly VehicleDetailsView _productDetailsView;
        private readonly IMessageDialogService _messageDialogService;
        private readonly IConnectivity _connectivity;
        public ObservableCollection<Vehicle> Vehicles { get; set; }

        [ObservableProperty]
        private Vehicle selectedProduct;

        public VehiclesViewModel(IVehicleDealershipService productService, VehicleDetailsView productDetailsView, IMessageDialogService messageDialogService,
            IConnectivity connectivity)
        {
            _messageDialogService = messageDialogService;
            _productDetailsView = productDetailsView;
            _productService = productService;
            _connectivity = connectivity; // set the _connectivity field
            Vehicles = new ObservableCollection<Vehicle>();
            GetVehicles();
        }

        public async Task GetVehicles()
        {
            Vehicles.Clear();
            if (_connectivity.NetworkAccess != NetworkAccess.Internet)
            {
                _messageDialogService.ShowMessage("Internet not available!");
                return;
            }

            var productsResult = await _productService.GetVehiclesAsync();
            if (productsResult.Success)
            {
                foreach (var p in productsResult.Data)
                {
                    Vehicles.Add(p);
                }
            }
            else
            {
<<<<<<< HEAD
                _messageDialogService.ShowMessage(productsResult.Message);
=======
                _messageDialogService.ShowMessage(vehiclesResult.Message);
            }
        }

        public void LoadVehiclesOnPage()
        {
            PageVehicles.Clear();

            int ItemsPerPage = 7;

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
>>>>>>> parent of 5598479 (changes)
            }
        }

        [RelayCommand]
        public async Task ShowDetails(Vehicle product)
        {
            if (_connectivity.NetworkAccess != NetworkAccess.Internet)
            {
                _messageDialogService.ShowMessage("Internet not available!");
                return;
            }

            await Shell.Current.GoToAsync(nameof(VehicleDetailsView), true, new Dictionary<string, object>
            {
                {"Product", product },
                {nameof(VehicleDetailsView), this }
            });
            SelectedProduct = product;
        }

        [RelayCommand]
        public async Task New()
        {
            if (_connectivity.NetworkAccess != NetworkAccess.Internet)
            {
                _messageDialogService.ShowMessage("Internet not available!");
                return;
            }

            SelectedProduct = new Vehicle();
            await Shell.Current.GoToAsync(nameof(VehicleDetailsView), true, new Dictionary<string, object>
            {
                {"Product", SelectedProduct },
                {nameof(VehicleDetailsView), this }
            });
        }

    }
}
