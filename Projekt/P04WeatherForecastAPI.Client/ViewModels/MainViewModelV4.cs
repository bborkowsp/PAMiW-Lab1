using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Extensions.DependencyInjection;
using P04WeatherForecastAPI.Client.Commands;
using P06Shop.Shared.MessageBox;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace P04WeatherForecastAPI.Client.ViewModels
{
    // przekazywanie wartosci do innego formularza 
    public partial class MainViewModelV4 : ObservableObject
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly IMessageDialogService _messageDialogService;


        public MainViewModelV4(

            IServiceProvider serviceProvider, IMessageDialogService messageDialogService)
        {

            _serviceProvider = serviceProvider;



            _messageDialogService = messageDialogService;

        }




       [RelayCommand]
        public void OpenVehicleDealershipWindow()
        {
            if (!string.IsNullOrEmpty(LoginViewModel.Token))
            {
                VehicleDealershipView vehicleDealershipView = _serviceProvider.GetService<VehicleDealershipView>();
                VehiclesViewModel vehiclesViewModel = _serviceProvider.GetService<VehiclesViewModel>();

                vehicleDealershipView.Show();
                vehiclesViewModel.GetVehicles();
            }
            else
            {
                _messageDialogService.ShowMessage("Access denied! Log in first!");
            }
        }

        [RelayCommand]
        public void OpenLoginWindow()
        {
            LoginView loginView = _serviceProvider.GetService<LoginView>();
            LoginViewModel loginViewModel = _serviceProvider.GetService<LoginViewModel>();

            loginView.Show();

        }
    }
}
