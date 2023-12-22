using P12MAUI.Client.ViewModels;

namespace P12MAUI.Client
{
    public partial class TestPage : ContentPage
    {
        public TestPage(TestViewModel mainViewModel)
        {
            InitializeComponent();
            BindingContext = mainViewModel;
        }

        private void OnToggledCommand(object sender, ToggledEventArgs e)
        {
            ((TestViewModel)BindingContext).OnToggledCommand(sender, e);
        }
    }
}