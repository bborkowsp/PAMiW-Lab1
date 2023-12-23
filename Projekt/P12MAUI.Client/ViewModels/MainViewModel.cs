using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.AspNetCore.Components.Authorization;
using P06Shop.Shared.Auth;
using P06Shop.Shared.Services.AuthService;
using P06Shop.Shared.MessageBox;
using P06Shop.Shared.Services.VehicleDealershipService;
using System;
using P06Shop.Shared.Languages;

using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;
using System.Threading.Tasks;

namespace P12MAUI.Client.ViewModels
{
    public partial class MainViewModel : ObservableObject
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly IAuthService _authService;
        private readonly IMessageDialogService _messageDialogService;
        private readonly IVehicleDealershipService _vehicleDealershipService;
        private readonly ILanguageService _languageService;
        private readonly AuthenticationStateProvider _authenticationStateProvider;

        public AuthenticationState AuthenticationState;
        private bool IsLoadingWebView;
        private bool IsLogin = false;

        public MainViewModel(IServiceProvider serviceProvider, IAuthService authService,
                             IMessageDialogService messageDialogService,
                             AuthenticationStateProvider authenticationStateProvider,
                             IVehicleDealershipService vehicleDealershipService, ILanguageService languageService)
        {
            UserLoginDTO = new UserLoginDTO();
            _serviceProvider = serviceProvider;
            _authService = authService;
            _messageDialogService = messageDialogService;
            _authenticationStateProvider = authenticationStateProvider;
            _vehicleDealershipService = vehicleDealershipService;
            _languageService = languageService;
            TestViewModel.LanguageChanged += OnLanguageChanged;

        }

        [ObservableProperty]
        private UserLoginDTO userLoginDTO;

        [ObservableProperty]
        private string errorMessage = "";

        [ObservableProperty]
        private bool isErrorVisible;

        [ObservableProperty]
        private bool isLoggingIn;

        public void SetIsLogin(bool isLogin)
        {
            IsLogin = isLogin;
            RefreshAllProperties();
        }

        [RelayCommand]
        public async Task Login()
        {
            IsErrorVisible = false;
            IsLoggingIn = true;

            if (string.IsNullOrEmpty(UserLoginDTO.Email) || string.IsNullOrEmpty(UserLoginDTO.Password))
            {
                ErrorMessage = GetErrorMessage();
                IsErrorVisible = true;
                IsLoggingIn = false;

                return;
            }

            var userLoginDTO = new UserLoginDTO
            {
                Email = UserLoginDTO.Email,
                Password = UserLoginDTO.Password
            };

            try
            {
                IsLoggingIn = true;
                IsErrorVisible = false;


                var response = await _authService.Login(userLoginDTO);

                if (response.Success)
                {
                    LoggedIn(response.Data);
                }
                else
                {
                    ErrorMessage = GetErrorMessage();
                    IsErrorVisible = true;
                }
            }
            finally
            {
                IsLoggingIn = false;
            }
        }

        [RelayCommand]
        public async Task Register()
        {
            RegisterPage loginView = _serviceProvider.GetService<RegisterPage>();
            RegisterViewModel loginViewModel = _serviceProvider.GetService<RegisterViewModel>();

            loginViewModel.SetIsLogin(false);

            await Shell.Current.GoToAsync(nameof(RegisterPage), true, new Dictionary<string, object>
            {
                {nameof(MainViewModel), this }
            });
        }

        public async Task LoggedIn(string token)
        {
            AppCurrentResources.SetToken(token);

            var loginView = _serviceProvider.GetService<VehiclesPage>();
            var loginViewModel = _serviceProvider.GetService<VehiclesViewModel>();

            await Shell.Current.GoToAsync(nameof(VehiclesPage), true, new Dictionary<string, object>
            {
                { nameof(MainViewModel), this }
            });

            var mainViewModel = _serviceProvider.GetService<MainViewModel>();
            mainViewModel.GetAuthenticationState();
        }

        public async Task LoggedOut()
        {
            AppCurrentResources.SetToken("");
            var mainViewModel = _serviceProvider.GetService<MainViewModel>();
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
        private void OnLanguageChanged(object sender, string newLanguage)
        {
            RefreshAllProperties();
        }
        public string LoginText
        {
            get { return _languageService.GetLanguage(TestViewModel.Language.ToLower(), "LoginTitle"); }
        }

        public string PasswordText
        {
            get { return _languageService.GetLanguage(TestViewModel.Language.ToLower(), "PasswordLabel"); }
        }
        public string CreateAccountText
        {
            get { return _languageService.GetLanguage(TestViewModel.Language.ToLower(), "CreateAccountLabel"); }
        }

        public string GetErrorMessage()
        {
            return _languageService.GetLanguage(TestViewModel.Language.ToLower(), "ErrorMessageLabel");
        }
        public string LoginButtonText
        {
            get { return _languageService.GetLanguage(TestViewModel.Language.ToLower(), "LoginButton"); }
        }

        public string NotRegisteredText
        {
            get { return _languageService.GetLanguage(TestViewModel.Language.ToLower(), "NotRegisteredLabel"); }
        }
    }
}
