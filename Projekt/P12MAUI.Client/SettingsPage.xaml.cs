using P12MAUI.Client.ViewModels;

namespace P12MAUI.Client
{
    public partial class SettingsPage : ContentPage
    {
        private SettingsViewModel _settingsViewModel;

        public SettingsPage(SettingsViewModel settingsViewModel)
        {
            InitializeComponent();
            BindingContext = settingsViewModel;
            _settingsViewModel = settingsViewModel;
        }

        private void OnToggledCommand(object sender, ToggledEventArgs e)
        {
            ((SettingsViewModel)BindingContext).OnToggledCommand(sender, e);
        }

        public void OnLanguageSelected(object sender, EventArgs e)
        {
            ((SettingsViewModel)BindingContext).OnLanguageSelected(sender, e);
        }

        private void Loaded_Event(object sender, EventArgs e)
        {
            SettingsViewModel.LoadSettings();
            _settingsViewModel.RefreshAllProperties();
        }
    }
}