using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.AspNetCore.Components.Authorization;
using System.Diagnostics;
using P06VehicleDealership.Shared.Languages;

using P06VehicleDealership.Shared.Auth;
using P06VehicleDealership.Shared.Services.AuthService;
using P06VehicleDealership.Shared.MessageBox;
using P06VehicleDealership.Shared.Services.VehicleDealershipService;
using System.Reflection;
using System;
using System.Globalization;
using Microsoft.Maui.Controls;

namespace P12MAUI.Client.ViewModels
{
    public partial class SettingsViewModel : ObservableObject
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly IMessageDialogService _messageDialogService;
        private readonly ILanguageService _languageService;

        public static bool DarkTheme = true;

        public static string Language = "polish";


        [ObservableProperty]
        private bool myProperty;

        [ObservableProperty]
        private int selectedIndex;

        public SettingsViewModel(IServiceProvider serviceProvider, IMessageDialogService messageDialogService, ILanguageService languageService)
        {
            _serviceProvider = serviceProvider;
            _messageDialogService = messageDialogService;
            DarkTheme = Preferences.Get("isDarkTheme", true);
            MyProperty = DarkTheme;
            SetTheme(DarkTheme);
            SetLanguage();
            _languageService = languageService;
            SettingsViewModel.LanguageChanged += OnLanguageChanged;
            RefreshAllProperties();
        }

        private void SetLanguage()
        {
            var savedLanguage = Preferences.Get("language", "polish");
            if (savedLanguage == "English")
            {
                selectedIndex = 1;
                return;
            }
            selectedIndex = 2;
            return;

        }

        public static void LoadSettings()
        {
            SetTheme(Preferences.Get("isDarkTheme", true));
        }

        public void OnToggledCommand(object sender, ToggledEventArgs e)
        {
            DarkTheme = e.Value;
            Preferences.Set("isDarkTheme", DarkTheme);
            SetTheme(DarkTheme);
            RefreshAllProperties();
        }
        public static void SetTheme(bool DarkTheme)
        {
            SettingsViewModel.DarkTheme = DarkTheme;
            UpdateResources();
            Preferences.Set("isDarkTheme", DarkTheme);
        }

        public static void UpdateResources()
        {
            if (DarkTheme)
            {
                Application.Current.UserAppTheme = AppTheme.Dark;
            }
            else
            {
                Application.Current.UserAppTheme = AppTheme.Light;
            }
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

        private string selectedLanguage;

        public string SelectedLanguage
        {
            get { return selectedLanguage; }
            set
            {
                if (selectedLanguage != value)
                {
                    selectedLanguage = value;
                    RefreshAllProperties();
                }
            }
        }
        public static event EventHandler<string> LanguageChanged;


        public void OnLanguageSelected(object sender, EventArgs e)
        {
            var picker = sender as Picker;
            if (picker != null)
            {
                SelectedLanguage = picker.SelectedItem as string;

                Preferences.Set("language", SelectedLanguage);
                SettingsViewModel.Language = SelectedLanguage.ToLower();
                RefreshAllProperties();
                LanguageChanged?.Invoke(this, SettingsViewModel.Language);

            }
        }

        public string ToggleThemeText => _languageService.GetLanguage(SettingsViewModel.Language.ToLower(), "ToggleTheme");

        public string ChooseLanguageLabelText => _languageService.GetLanguage(SettingsViewModel.Language.ToLower(), "ChooseLanguageLabel");
        private void OnLanguageChanged(object sender, string newLanguage)
        {
            RefreshAllProperties();
        }
    }
}