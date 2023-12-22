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
    
    }
}