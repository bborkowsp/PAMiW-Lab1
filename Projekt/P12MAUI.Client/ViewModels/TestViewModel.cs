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

        public static bool DarkTheme = false;


        [ObservableProperty]
        private bool myProperty;

        public TestViewModel(IServiceProvider serviceProvider, IMessageDialogService messageDialogService)
        {
            _serviceProvider = serviceProvider;
            _messageDialogService = messageDialogService;
        }

        public void OnToggledCommand(object sender, ToggledEventArgs e)
        {
            // Handle the switch toggle event if needed
            Debug.WriteLine($"Switch toggled. New value: {e.Value}");
            SetTheme(e.Value);

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


    }
}
