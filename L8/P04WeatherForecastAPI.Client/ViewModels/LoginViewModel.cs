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
            UserLoginDTO.Password = password;
            var response = await _authService.Login(UserLoginDTO);
            if (response.Success)
            {
                _wpfMesageDialogService.ShowMessage("Zalogowano");
                Token = response.Data;
            }
            else
            {
                _wpfMesageDialogService.ShowMessage("Błąd logowania: " + response.Message);
                Token = string.Empty;
            }
            
        }

        [RelayCommand]
        public async Task MouseEnter()
        {
             
        }




    }

}
