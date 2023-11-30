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
using System.Threading.Tasks;
using System.Windows.Input;

namespace P04WeatherForecastAPI.Client.ViewModels
{
    public partial class LoginViewModel : ObservableObject
    {
        private readonly IAuthService _authService;
        private readonly IMessageDialogService _wpfMesageDialogService;
        public static string Token { get; set; } = string.Empty;


        [ObservableProperty]
        private string password = string.Empty;

        public LoginViewModel(IAuthService authService, IMessageDialogService wpfMesageDialogService)
        {
            UserLoginDTO = new UserLoginDTO();
            _authService = authService;
            _wpfMesageDialogService = wpfMesageDialogService;
        }

        [ObservableProperty]
        private UserLoginDTO userLoginDTO;


        
        public async Task Login(string password)
        {
            Debug.WriteLine("Logging in... with email: " + UserLoginDTO.Email + " and password " + password);
            UserLoginDTO.Password = password;
            var response = await _authService.Login(UserLoginDTO);
            if (response.Success)
            {
                Debug.WriteLine("Logged in!");
                _wpfMesageDialogService.ShowMessage("Logged in successfully!");
                Token = response.Data;
            }
            else
            {
                Debug.WriteLine("Login failed: " + response.Message);
                _wpfMesageDialogService.ShowMessage("Login failed!\nMessage: " + response.Message);
                Token = string.Empty;
            }
            
        }

        [RelayCommand]
        public async Task MouseEnter()
        {
             
        }




    }

}
