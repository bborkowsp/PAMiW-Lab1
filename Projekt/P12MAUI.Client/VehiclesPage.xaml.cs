using P12MAUI.Client.ViewModels;
using System.Diagnostics;
namespace P12MAUI.Client
{
    public partial class VehiclesPage : ContentPage
    {
        private VehiclesViewModel vehiclesViewModel;

        public VehiclesPage(VehiclesViewModel _vehiclesViewModel)
        {
            BindingContext = _vehiclesViewModel;
            vehiclesViewModel = _vehiclesViewModel;
            InitializeComponent();
        }
                private void Loaded_Event(object sender, EventArgs e)
        {
            SettingsViewModel.LoadSettings();
            vehiclesViewModel.RefreshAllProperties();
        }
    }
}
