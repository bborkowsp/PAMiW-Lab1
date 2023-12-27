using P12MAUI.Client.ViewModels;
using System.Diagnostics;

namespace P12MAUI.Client;

public partial class RegisterPage : ContentPage
{
    private RegisterViewModel registerViewModel;
    public RegisterPage(RegisterViewModel _registerViewModel)
    {
        BindingContext = _registerViewModel;
        registerViewModel = _registerViewModel;
        InitializeComponent();
    }
    private void Loaded_Event(object sender, EventArgs e)
    {
        SettingsViewModel.LoadSettings();
        registerViewModel.RefreshAllProperties();
    }

}