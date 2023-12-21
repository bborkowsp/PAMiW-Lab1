using P12MAUI.Client.ViewModels;
using System.Diagnostics;
namespace P12MAUI.Client
{
    [QueryProperty(nameof(MainViewModel), nameof(MainViewModel))]
    public partial class VehiclesPage : ContentPage
    {
        // public VehiclesPage()
        // {
        //     Trace.WriteLine("vehicles page BEZ ARG--------");
        //     InitializeComponent();
        // }

        // Constructor with a parameter for VehiclesViewModel
        public VehiclesPage(VehiclesViewModel vehiclesViewModel)
        {
            Trace.WriteLine("vehicles page Z ARG+++++")
    ;
            InitializeComponent();
            BindingContext = vehiclesViewModel;
        }
    }
}
