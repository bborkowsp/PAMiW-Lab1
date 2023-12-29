using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.AspNetCore.Components.Authorization;
using P06Shop.Shared.Auth;
using P06Shop.Shared.Services.AuthService;
using P06Shop.Shared.MessageBox;
using P06Shop.Shared.Languages;
using P06Shop.Shared.Services.VehicleDealershipService;
using System;
using System.Reflection;
using System.Threading.Tasks;

namespace P12MAUI.Client.ViewModels
{
    public partial class RegisterViewModel : ObservableObject
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly IAuthService _authService;
        private readonly IMessageDialogService _messageDialogService;
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

        public RegisterViewModel(IServiceProvider serviceProvider, IAuthService authService,
            IMessageDialogService wpfMesageDialogService, AuthenticationStateProvider authenticationStateProvider,
            ILanguageService languageService)
        {
            UserRegisterDTO = new UserRegisterDTO();
            _serviceProvider = serviceProvider;
            _authService = authService;
            _languageService = languageService;
            SettingsViewModel.LanguageChanged += OnLanguageChanged;
        }

        [ObservableProperty] private UserRegisterDTO userRegisterDTO;

        [RelayCommand]
        public async Task Register()
        {
            ResetErrorMessages();
            RefreshAllProperties();

            if (string.IsNullOrEmpty(UserRegisterDTO.Email) || string.IsNullOrEmpty(UserRegisterDTO.Password) ||
                string.IsNullOrEmpty(UserRegisterDTO.Username) || string.IsNullOrEmpty(UserRegisterDTO.ConfirmPassword))
            {
                globalErrorMessage = _languageService.GetLanguage(SettingsViewModel.Language.ToLower(), "NonNullFieldErrorMessage");
                isGlobalErrorMessageVisible = true;
                RefreshAllProperties();
                return;
            }

            if (!UserRegisterDTO.Email.Contains("@"))
            {
                emailErrorMessage = _languageService.GetLanguage(SettingsViewModel.Language.ToLower(), "WrongEmailMessageError");
                isEmailErrorMessageVisible = true;
                RefreshAllProperties();
                return;
            }

            if (UserRegisterDTO.Password.Length < 8)
            {
                passwordErrorMessage = _languageService.GetLanguage(SettingsViewModel.Language.ToLower(), "WrongPasswordMessageError");
                isPasswordErrorMessageVisible = true;
                RefreshAllProperties();
                return;
            }

            if (UserRegisterDTO.Password != UserRegisterDTO.ConfirmPassword)
            {
                confirmationPasswordErrorMessage = _languageService.GetLanguage(SettingsViewModel.Language.ToLower(), "WrongConfPasswordMessageError");
                isConfirmationPasswordErrorMessageVisible = true;
                RefreshAllProperties();
                return;
            }

            try
            {
                isLoading = true;
                RefreshAllProperties();

                var response = await _authService.Register(UserRegisterDTO);

                if (response.Success)
                {
                    accountCreatedMessage = _languageService.GetLanguage(SettingsViewModel.Language.ToLower(), "AccountCreatedMessage");
                    isAccountCreatedMessageVisible = true;
                    RefreshAllProperties();
                    isLoading = false;
                    RefreshAllProperties();
                    return;
                }
                else
                {
                    globalErrorMessage = _languageService.GetLanguage(SettingsViewModel.Language.ToLower(), "CreateAccountErrorMessage");
                    isGlobalErrorMessageVisible = true;
                    RefreshAllProperties();
                    isLoading = false;
                    RefreshAllProperties();
                    return;
                }
            }
            finally
            {
                isLoading = false;
            }
        }

        private void ResetErrorMessages()
        {
            isUsernameErrorMessageVisible = false;
            isConfirmationPasswordErrorMessageVisible = false;
            isEmailErrorMessageVisible = false;
            isPasswordErrorMessageVisible = false;
            isGlobalErrorMessageVisible = false;
            isAccountCreatedMessageVisible = false;
        }

        public void SetLoading(bool loading) { }

        public async void CloseLoginWindow()
        {
            var navigation = Application.Current.MainPage.Navigation;
            await navigation.PopAsync();
        }

        public string UsernameText => _languageService.GetLanguage(SettingsViewModel.Language.ToLower(), "UsernameLabel");

        public string PasswordText => _languageService.GetLanguage(SettingsViewModel.Language.ToLower(), "PasswordLabel");

        public string ConfirmPasswordText => _languageService.GetLanguage(SettingsViewModel.Language.ToLower(), "ConfirmPasswordLabel");

        public string CreateAccountText => _languageService.GetLanguage(SettingsViewModel.Language.ToLower(), "CreateAccountLabel2");

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
    }
}