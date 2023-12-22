using P12MAUI.Client.ViewModels;

namespace P12MAUI.Client
{
    public partial class TestPage : ContentPage
    {
        public TestPage(TestViewModel testViewModel)
        {
            InitializeComponent();
            BindingContext = testViewModel;
        }

        private void OnToggledCommand(object sender, ToggledEventArgs e)
        {
            ((TestViewModel)BindingContext).OnToggledCommand(sender, e);
        }

        public void OnLanguageSelected(object sender, EventArgs e)
        {
            ((TestViewModel)BindingContext).OnLanguageSelected(sender, e);
        }
    }
}