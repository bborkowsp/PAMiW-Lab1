using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.AspNetCore.Components.Authorization;
using P06VehicleDealership.Shared.Auth;
using P06VehicleDealership.Shared.Services.AuthService;
using P06VehicleDealership.Shared.MessageBox;
using P06VehicleDealership.Shared.Services.VehicleDealershipService;
using P06VehicleDealership.Shared.Languages;
using System.Collections.Generic;
using System.Reflection;
using System.Threading.Tasks;

namespace P12MAUI.Client.ViewModels
{
    public partial class MainViewModel : ObservableObject
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly IAuthService _authService;
        private readonly IVehicleDealershipService _vehicleDealershipService;
        private readonly ILanguageService _languageService;
        private readonly AuthenticationStateProvider _authenticationStateProvider;

        public AuthenticationState AuthenticationState
        {
            get;
            private set;
        }

        public MainViewModel(
            IServiceProvider serviceProvider, IAuthService authService,
            AuthenticationStateProvider authenticationStateProvider,
            IVehicleDealershipService vehicleDealershipService, ILanguageService languageService)
        {
            UserLoginDTO = new UserLoginDTO();
            _serviceProvider = serviceProvider;
            _authService = authService;
            _authenticationStateProvider = authenticationStateProvider;
            _vehicleDealershipService = vehicleDealershipService;
            _languageService = languageService;
            SettingsViewModel.LanguageChanged += OnLanguageChanged;
        }

        [ObservableProperty] private UserLoginDTO userLoginDTO;
        [ObservableProperty] private string errorMessage = "";
        [ObservableProperty] private bool isErrorVisible;
        [ObservableProperty] private bool isLoggingIn;

        [RelayCommand]
        public async Task Login()
        {
            isErrorVisible = false;
            isLoggingIn = true;
            RefreshAllProperties();

            if (string.IsNullOrEmpty(UserLoginDTO.Email) || string.IsNullOrEmpty(UserLoginDTO.Password))
            {
                errorMessage = GetErrorMessage();
                isErrorVisible = true;
                isLoggingIn = false;
                RefreshAllProperties();
                return;
            }

            var userLoginDTO = new UserLoginDTO
            {
                Email = UserLoginDTO.Email,
                Password = UserLoginDTO.Password
            };

            try
            {
                var response = await _authService.Login(userLoginDTO);

                if (response.Success)
                {
                    await LoggedIn(response.Data);
                    isLoggingIn = false;
                    RefreshAllProperties();
                }
                else
                {
                    errorMessage = GetWrongCredentialsMessage();
                    isErrorVisible = true;
                    isLoggingIn = false;
                    RefreshAllProperties();
                    return;
                }
            }
            finally
            {
                isLoggingIn = false;
            }
        }

        [RelayCommand]
        public async Task Register()
        {
            RegisterPage loginView = _serviceProvider.GetService<RegisterPage>();
            RegisterViewModel loginViewModel = _serviceProvider.GetService<RegisterViewModel>();
            await Shell.Current.GoToAsync(nameof(RegisterPage), true, new Dictionary<string, object> {
                {
                    nameof(MainViewModel), this
                }
            });
        }

        private async Task LoggedIn(string token)
        {
            AppCurrentResources.SetToken(token);

            var loginView = _serviceProvider.GetService<VehiclesPage>();
            var loginViewModel = _serviceProvider.GetService<VehiclesViewModel>();

            await Shell.Current.GoToAsync(nameof(VehiclesPage), true, new Dictionary<string, object> {
                {
                    nameof(MainViewModel), this
                }
            });

            var mainViewModel = _serviceProvider.GetService<MainViewModel>();
            await mainViewModel.GetAuthenticationState();
        }

        public async Task LoggedOut()
        {
            AppCurrentResources.SetToken("");
            var mainViewModel = _serviceProvider.GetService<MainViewModel>();
            await mainViewModel.GetAuthenticationState();
        }

        public async Task GetAuthenticationState()
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

        private void OnLanguageChanged(object sender, string newLanguage)
        {
            RefreshAllProperties();
        }

        public string LoginText => _languageService.GetLanguage(SettingsViewModel.Language.ToLower(), "LoginTitle");
        public string PasswordText => _languageService.GetLanguage(SettingsViewModel.Language.ToLower(), "PasswordLabel");
        public string CreateAccountText => _languageService.GetLanguage(SettingsViewModel.Language.ToLower(), "CreateAccountLabel");
        public string GetErrorMessage() => _languageService.GetLanguage(SettingsViewModel.Language.ToLower(), "NonNullFieldErrorMessage");
        public string GetWrongCredentialsMessage() => _languageService.GetLanguage(SettingsViewModel.Language.ToLower(), "WrongCredentialsMessage");
        public string LoginButtonText => _languageService.GetLanguage(SettingsViewModel.Language.ToLower(), "LoginButton");
        public string NotRegisteredText => _languageService.GetLanguage(SettingsViewModel.Language.ToLower(), "NotRegisteredLabel");
    }
}