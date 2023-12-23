using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.AspNetCore.Components.Authorization;
using P06Shop.Shared.Auth;
using P06Shop.Shared.Services.AuthService;
using P06Shop.Shared.MessageBox;
using P06Shop.Shared.Languages;

using P06Shop.Shared.Services.VehicleDealershipService;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;
using System.Threading.Tasks;

using System.Reflection;
using Microsoft.AspNetCore.Components.Authorization;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace P12MAUI.Client.ViewModels
{

    public partial class RegisterViewModel : ObservableObject
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly IAuthService _authService;
        private readonly IMessageDialogService _mesageDialogService;

        private readonly ILanguageService _languageService;

        private bool IsLogin = false;

        public RegisterViewModel(IServiceProvider serviceProvider, IAuthService authService,
            IMessageDialogService wpfMesageDialogService, AuthenticationStateProvider authenticationStateProvider,
             ILanguageService languageService)
        {
            UserRegisterDTO = new UserRegisterDTO();
            _serviceProvider = serviceProvider;
            _authService = authService;
            _mesageDialogService = wpfMesageDialogService;
            _languageService = languageService;
        }

        [ObservableProperty]
        private UserRegisterDTO userRegisterDTO;

        public void SetIsLogin(bool _isLogin)
        {
            Debug.WriteLine("SetIsLogin: " + _isLogin);
            IsLogin = _isLogin;
            RefreshAllProperties();
        }

        [RelayCommand]
        public async Task Register()
        {
            Debug.WriteLine("Logging in... with email: " + UserRegisterDTO.Email + " and password " + UserRegisterDTO.Password);

            if (string.IsNullOrEmpty(UserRegisterDTO.Email) || string.IsNullOrEmpty(UserRegisterDTO.Password))
            {
                return;
            }

            if (string.IsNullOrEmpty(UserRegisterDTO.Username) || string.IsNullOrEmpty(UserRegisterDTO.ConfirmPassword))
            {
                return;
            }

            if (!UserRegisterDTO.Email.Contains("@") || UserRegisterDTO.Password.Length < 6)
            {
                return;
            }

            if (UserRegisterDTO.Password != UserRegisterDTO.ConfirmPassword)
            {
                return;
            }

            var response = await _authService.Register(UserRegisterDTO);

            if (response.Success)
            {
                SetIsLogin(true);
            }
            else
            {
            }
        }

        public async Task LoggedIn(string token)
        {
            Debug.WriteLine("Logged in!");

            AppCurrentResources.SetToken(token);
            MainViewModel mainViewModel = _serviceProvider.GetService<MainViewModel>();
            mainViewModel.GetAuthenticationState();
            CloseLoginWindow();
        }

        public async Task LoggedOut()
        {
            AppCurrentResources.SetToken("");
            MainViewModel mainViewModel = _serviceProvider.GetService<MainViewModel>();
            mainViewModel.GetAuthenticationState();
        }

        public void OnPageLoaded()
        {
            SetLoading(false);
        }

        public void SetLoading(bool loading)
        {
        }

        public async void CloseLoginWindow()
        {
            var navigation = Application.Current.MainPage.Navigation;
            await navigation.PopAsync();
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

        public string UsernameText
        {
            get { return _languageService.GetLanguage(TestViewModel.Language.ToLower(), "UsernameLabel"); }
        }
        public string PasswordText
        {
            get { return _languageService.GetLanguage(TestViewModel.Language.ToLower(), "PasswordLabel"); }
        }
        public string ConfirmPasswordText
        {
            get { return _languageService.GetLanguage(TestViewModel.Language.ToLower(), "ConfirmPasswordLabel"); }
        }
                public string CreateAccountText
        {
            get { return _languageService.GetLanguage(TestViewModel.Language.ToLower(), "CreateAccountLabel2"); }
        }
    }
}
