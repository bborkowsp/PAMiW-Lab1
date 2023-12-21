using P12MAUI.Client.ViewModels;

namespace P12MAUI.Client
{
    public partial class MainPage : ContentPage
    {
        public MainPage(MainViewModel mainViewModel)
        {
            InitializeComponent();
            BindingContext = mainViewModel;
        }
    }
}