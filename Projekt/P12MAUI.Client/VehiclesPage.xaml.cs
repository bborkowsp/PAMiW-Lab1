using P12MAUI.Client.ViewModels;
using System.Diagnostics;
namespace P12MAUI.Client
{
    [QueryProperty(nameof(MainViewModel), nameof(MainViewModel))]
    public partial class VehiclesPage : ContentPage
    {
        public VehiclesPage(VehiclesViewModel vehiclesViewModel)
        {
            InitializeComponent();
            BindingContext = vehiclesViewModel;
        }
    }
}
