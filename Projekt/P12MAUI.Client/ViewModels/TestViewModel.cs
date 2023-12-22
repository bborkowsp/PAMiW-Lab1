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
    public partial class TestViewModel : ObservableObject
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly IMessageDialogService _messageDialogService;

        public static bool DarkTheme = true;

        [ObservableProperty]
        private bool myProperty;

        public TestViewModel(IServiceProvider serviceProvider, IMessageDialogService messageDialogService)
        {
            _serviceProvider = serviceProvider;
            _messageDialogService = messageDialogService;
            DarkTheme = Preferences.Get("isDarkTheme", true);
            MyProperty = DarkTheme;
            SetTheme(DarkTheme);
            RefreshAllProperties();
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
            TestViewModel.DarkTheme = DarkTheme;
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

    }
}