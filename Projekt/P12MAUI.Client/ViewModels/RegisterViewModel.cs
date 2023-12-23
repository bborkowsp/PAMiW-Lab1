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

        [ObservableProperty] private bool isUsernameErrorMessageVisible;
        [ObservableProperty] private bool isEmailErrorMessageVisible;
        [ObservableProperty] private bool isPasswordErrorMessageVisible;
        [ObservableProperty] private bool isConfirmationPasswordErrorMessageVisible;

        [ObservableProperty] private bool isGlobalErrorMessageVisible;
        [ObservableProperty] private bool isLoading;

        [ObservableProperty] private bool isAccountCreatedMessageVisible;

        [ObservableProperty] private string emailErrorMessage = "";
        [ObservableProperty] private string usernameErrorMessage = "";
        [ObservableProperty] private string passwordErrorMessage = "";
        [ObservableProperty] private string confirmationPasswordErrorMessage = "";
        [ObservableProperty] private string accountCreatedMessage = "";

        [ObservableProperty] private string globalErrorMessage = "";

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

        [RelayCommand]
        public async Task Register()
        {
            isUsernameErrorMessageVisible = false;
            isConfirmationPasswordErrorMessageVisible = false;
            isEmailErrorMessageVisible = false;
            isPasswordErrorMessageVisible = false;
            isGlobalErrorMessageVisible = false;
            RefreshAllPropertiesA();
            if (string.IsNullOrEmpty(UserRegisterDTO.Email) || string.IsNullOrEmpty(UserRegisterDTO.Password) || string.IsNullOrEmpty(UserRegisterDTO.Username) || string.IsNullOrEmpty(UserRegisterDTO.ConfirmPassword))
            {
                globalErrorMessage = _languageService.GetLanguage(TestViewModel.Language.ToLower(), "NonNullFieldErrorMessage");
                isGlobalErrorMessageVisible = true;
                RefreshAllPropertiesA();
                return;
            }
            if (!UserRegisterDTO.Email.Contains("@"))
            {
                emailErrorMessage = _languageService.GetLanguage(TestViewModel.Language.ToLower(), "WrongEmailMessageError");
                isEmailErrorMessageVisible = true;
                RefreshAllPropertiesA();
                return;
            }
            if (UserRegisterDTO.Password.Length < 8)
            {
                passwordErrorMessage = _languageService.GetLanguage(TestViewModel.Language.ToLower(), "WrongPasswordMessageError");
                isPasswordErrorMessageVisible = true;
                RefreshAllPropertiesA();
                return;
            }

            if (UserRegisterDTO.Password != UserRegisterDTO.ConfirmPassword)
            {
                confirmationPasswordErrorMessage = _languageService.GetLanguage(TestViewModel.Language.ToLower(), "WrongConfPasswordMessageError");
                isConfirmationPasswordErrorMessageVisible = true;
                RefreshAllPropertiesA();
                return;
            }
            try
            {
                isLoading = true;
                RefreshAllPropertiesA();
                var response = await _authService.Register(UserRegisterDTO);
                isLoading = false;
                RefreshAllPropertiesA();

                if (response.Success)
                {
                    accountCreatedMessage = _languageService.GetLanguage(TestViewModel.Language.ToLower(), "AccountCreatedMessage");
                    isAccountCreatedMessageVisible = true;
                    RefreshAllPropertiesA();
                    isLoading = false;
                    RefreshAllPropertiesA();

                    Thread.Sleep(3000);

                    return;

                }
                else
                {
                    globalErrorMessage = _languageService.GetLanguage(TestViewModel.Language.ToLower(), "CreateAccountErrorMessage");
                    isGlobalErrorMessageVisible = true;
                    RefreshAllPropertiesA();
                    isLoading = false;
                    RefreshAllPropertiesA();



                    return;
                }
            }
            finally
            {
                isLoading = false;
                           //         CloseLoginWindow();

            }
        }

        public void SetLoading(bool loading) { }

        public async void CloseLoginWindow()
        {
            var navigation = Application.Current.MainPage.Navigation;
            await navigation.PopAsync();
            return;

        }

        public string UsernameText
        {
            get
            {
                return _languageService.GetLanguage(TestViewModel.Language.ToLower(), "UsernameLabel");
            }
        }
        public string PasswordText
        {
            get
            {
                return _languageService.GetLanguage(TestViewModel.Language.ToLower(), "PasswordLabel");
            }
        }
        public string ConfirmPasswordText
        {
            get
            {
                return _languageService.GetLanguage(TestViewModel.Language.ToLower(), "ConfirmPasswordLabel");
            }
        }
        public string CreateAccountText
        {
            get
            {
                return _languageService.GetLanguage(TestViewModel.Language.ToLower(), "CreateAccountLabel2");
            }
        }

        private void RefreshAllPropertiesA()
        {
            OnPropertyChanged();
            var properties = GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance);
            foreach (var property in properties)
            {
                OnPropertyChanged(property.Name);
            }
            OnPropertyChanged(nameof(IsUsernameErrorMessageVisible));
            OnPropertyChanged(nameof(IsConfirmationPasswordErrorMessageVisible));
            OnPropertyChanged(nameof(IsEmailErrorMessageVisible));
            OnPropertyChanged(nameof(IsPasswordErrorMessageVisible));
            OnPropertyChanged(nameof(IsGlobalErrorMessageVisible));
            OnPropertyChanged(nameof(IsAccountCreatedMessageVisible));
            OnPropertyChanged(nameof(EmailErrorMessage));
            OnPropertyChanged(nameof(UsernameErrorMessage));
            OnPropertyChanged(nameof(PasswordErrorMessage));
            OnPropertyChanged(nameof(ConfirmationPasswordErrorMessage));
            OnPropertyChanged(nameof(AccountCreatedMessage));
            OnPropertyChanged(nameof(GlobalErrorMessage));
        }
    }
}