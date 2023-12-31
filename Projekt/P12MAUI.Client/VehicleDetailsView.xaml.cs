using P12MAUI.Client.ViewModels;

namespace P12MAUI.Client;

public partial class VehicleDetailsView : ContentPage
{
	private VehicleDetailsViewModel _vehicleDetailsViewModel;
	public VehicleDetailsView(VehicleDetailsViewModel vehicleDetailsViewModel)
	{
		BindingContext = vehicleDetailsViewModel;
		_vehicleDetailsViewModel = vehicleDetailsViewModel;
		InitializeComponent();
	}
	private void Loaded_Event(object sender, EventArgs e)
	{
		SettingsViewModel.LoadSettings();
		_vehicleDetailsViewModel.RefreshAllProperties();
	}
}