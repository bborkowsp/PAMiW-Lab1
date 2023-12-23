using P12MAUI.Client.ViewModels;
using System.Diagnostics;

namespace P12MAUI.Client;

public partial class RegisterPage : ContentPage
{
    private RegisterViewModel loginViewModel;
    public RegisterPage(RegisterViewModel _loginViewModel)
    {
        BindingContext = _loginViewModel;
        loginViewModel = _loginViewModel;
        InitializeComponent();
    }

    // private void Navigated(object sender, WebNavigatedEventArgs e)
    // {
    //     loginViewModel.OnPageLoaded();
    // }

}