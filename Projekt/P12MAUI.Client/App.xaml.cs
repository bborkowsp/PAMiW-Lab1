using P06VehicleDealership.Shared.Languages;
using P12MAUI.Client.ViewModels;
namespace P12MAUI.Client
{
    public partial class App : Application
    {
        private readonly ILanguageService languageService;

        public App(ILanguageService _languageService)
        {
            InitializeComponent();
            languageService = _languageService;
            MainPage = new AppShell(languageService);
        }
    }
}