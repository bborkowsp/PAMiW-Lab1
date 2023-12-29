using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using P06Shop.Shared.Auth;
using P06Shop.Shared.MessageBox;
using P06Shop.Shared.Services.AuthService;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Windows;

using System.Threading.Tasks;
using System.Windows.Input;
using P04WeatherForecastAPI.Client.ViewModels;

namespace P04WeatherForecastAPI.Client.ViewModels
{
    public partial class LoginViewModel : ObservableObject
    {
        private readonly IAuthService _authService;
        private readonly IMessageDialogService _wpfMesageDialogService;
        private readonly IServiceProvider _serviceProvider;

        public static string Token { get; set; } = string.Empty;


        [ObservableProperty]
        private string password = string.Empty;

        public LoginViewModel(IServiceProvider serviceProvider, IAuthService authService, IMessageDialogService wpfMesageDialogService)
        {
            _serviceProvider = serviceProvider;

            UserLoginDTO = new UserLoginDTO();
            _authService = authService;
            _wpfMesageDialogService = wpfMesageDialogService;
        }

        [ObservableProperty]
        private UserLoginDTO userLoginDTO;



        public async Task Login(string password)
        {
            Trace.WriteLine(LanguageManager.GetActualLanguage());
            UserLoginDTO.Password = password;
            var response = await _authService.Login(UserLoginDTO);
            Application.Current.Dispatcher.Invoke(() =>
    {
        if (response.Success)
        {
            LoggedInViewModel loggedInViewModel = (LoggedInViewModel)_serviceProvider.GetService(typeof(LoggedInViewModel));

            if (LanguageManager.GetActualLanguage() == "en-US")
            {
                loggedInViewModel.setMessage("Logged in successfully!");
            }
            else
            {
                loggedInViewModel.setMessage("Zalogowano pomyślnie!");
            }
            LoggedInView vehicleDealershipView = (LoggedInView)_serviceProvider.GetService(typeof(LoggedInView));



            vehicleDealershipView.Show();
            Token = response.Data;
        }
        else
        {
            LoggedInViewModel loggedInViewModel = (LoggedInViewModel)_serviceProvider.GetService(typeof(LoggedInViewModel));

            if (LanguageManager.GetActualLanguage() == "en-US")
            {
                loggedInViewModel.setMessage("Error, wrong login credentials!");
            }
            else
            {
                loggedInViewModel.setMessage("Błąd, błędne dane logowania!");
            }
            LoggedInView vehicleDealershipView = (LoggedInView)_serviceProvider.GetService(typeof(LoggedInView));



            vehicleDealershipView.Show();
            Token = string.Empty;
        }
    });
        }

        [RelayCommand]
        public async Task MouseEnter()
        {

        }




    }

}
