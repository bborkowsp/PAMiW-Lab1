using P12MAUI.Client.ViewModels;

namespace P12MAUI.Client
{
    public partial class MainPage : ContentPage
    {

        private MainViewModel _mainViewModel;

        public MainPage(MainViewModel mainViewModel)
        {
            InitializeComponent();
            BindingContext = mainViewModel;
            _mainViewModel = mainViewModel;
        }

        private void Loaded_Event(object sender, EventArgs e)
        {
            TestViewModel.LoadSettings();
            _mainViewModel.RefreshAllProperties();
        }
    }
}