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

            // Retrieve the saved theme preference
            DarkTheme = Preferences.Get("isDarkTheme", true);

            // Set the initial theme
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
            Debug.WriteLine($"Switch toggled. New value: {e.Value}");

            // Update the theme setting and save it
            DarkTheme = e.Value;
            Preferences.Set("isDarkTheme", DarkTheme);

            // Set the theme based on the updated setting
            SetTheme(DarkTheme);
            RefreshAllProperties();
        }
        public static void SetTheme(bool DarkTheme)
        {
            Trace.WriteLine("DarkTheme: ", DarkTheme.ToString());
            TestViewModel.DarkTheme = DarkTheme;
            UpdateResources();
            Preferences.Set("isDarkTheme", DarkTheme);
        }

        public static void UpdateResources()
        {
            if (DarkTheme)
            {
                Trace.WriteLine("If darkTheme = true");

                Application.Current.UserAppTheme = AppTheme.Dark;
            }
            else
            {
                Trace.WriteLine("If darkTheme = false");  // Corrected log statement

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
