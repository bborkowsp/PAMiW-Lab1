namespace P12MAUI.Client
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();

            Routing.RegisterRoute(nameof(VehicleDetailsView), typeof(VehicleDetailsView));
            Routing.RegisterRoute(nameof(VehiclesPage), typeof(VehiclesPage));
        }
        
        private void ChangeTheme_Clicked(object sender, EventArgs e)
        {
            // Handle theme change logic here
        }

        private void ChangeLanguage_Clicked(object sender, EventArgs e)
        {
            // Handle language change logic here
        }
    }
}