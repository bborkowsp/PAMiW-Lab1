using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.AspNetCore.Components.Authorization;
using System.Diagnostics;
using P06Shop.Shared.Auth;
using P06Shop.Shared.Services.AuthService;
using P06Shop.Shared.MessageBox;
using P06Shop.Shared.Services.VehicleDealershipService;
using System.Reflection;
namespace P12MAUI.Client.ViewModels
{

    public partial class MainViewModel : ObservableObject
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly IAuthService _authService;
        private readonly IMessageDialogService _mesageDialogService;

        private readonly IVehicleDealershipService _vehicleDealershipService;

        public AuthenticationState AuthenticationState;
        private readonly AuthenticationStateProvider _authenticationStateProvider;



        private bool IsLoadingWebView;
        private bool IsLogin = false;

        public MainViewModel(IServiceProvider serviceProvider, IAuthService authService,
            IMessageDialogService wpfMesageDialogService, AuthenticationStateProvider authenticationStateProvider, IVehicleDealershipService vehicleDealershipService)
        {
            UserLoginDTO = new UserLoginDTO();
            _serviceProvider = serviceProvider;
            _authService = authService;
            _mesageDialogService = wpfMesageDialogService;
            _authenticationStateProvider = authenticationStateProvider;

            // Initialize _vehicleDealershipService
            _vehicleDealershipService = vehicleDealershipService;
        }


        [ObservableProperty]
        private UserLoginDTO userLoginDTO;


        public void SetIsLogin(bool _isLogin)
        {
            Debug.WriteLine("SetIsLogin: " + _isLogin);
            IsLogin = _isLogin;
            RefreshAllProperties();
        }

        [RelayCommand]
        public async Task Login()
        {
            Trace.WriteLine("Logging in... with email: " + UserLoginDTO.Email + " and password " + UserLoginDTO.Password);

            if (string.IsNullOrEmpty(UserLoginDTO.Email) || string.IsNullOrEmpty(UserLoginDTO.Password))
            {
            }


                UserLoginDTO userLoginDTO = new UserLoginDTO();
                userLoginDTO.Email = UserLoginDTO.Email;
                userLoginDTO.Password = UserLoginDTO.Password;
                Trace.WriteLine("Loguje si�");

                var response = await _authService.Login(userLoginDTO);

                if (response.Success)
                {
                    Trace.WriteLine("Sukces");

                    LoggedIn(response.Data);

                }
                else
                {
                    Trace.WriteLine("porazka");

                    LoggedOut();
                }

        }

        public async Task LoggedIn(string token)
        {
            Debug.WriteLine("Logged in!");

            AppCurrentResources.SetToken(token);
            Trace.WriteLine("token,:  ",token);
            


                                VehiclesPage loginView = _serviceProvider.GetService<VehiclesPage>();
            VehiclesViewModel loginViewModel = _serviceProvider.GetService<VehiclesViewModel>();


            await Shell.Current.GoToAsync(nameof(VehiclesPage), true, new Dictionary<string, object>
            {
                {nameof(MainViewModel), this }
            });

            MainViewModel mainViewModel = _serviceProvider.GetService<MainViewModel>();




            mainViewModel.GetAuthenticationState();
        }

        public async Task LoggedOut()
        {
            AppCurrentResources.SetToken("");
            MainViewModel mainViewModel = _serviceProvider.GetService<MainViewModel>();
            mainViewModel.GetAuthenticationState();
        }

        public async void GetAuthenticationState()
        {
            AuthenticationState = await _authenticationStateProvider.GetAuthenticationStateAsync();
            _vehicleDealershipService.SetAuthToken(AppCurrentResources.Token);
            _authService.SetAuthToken(AppCurrentResources.Token);
            RefreshAllProperties();
        }

        public void RefreshAllProperties()
        {
            OnPropertyChanged();
            var properties = GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance);
            foreach (var property in properties)
            {
                OnPropertyChanged(property.Name);
            }
        }

    }
}