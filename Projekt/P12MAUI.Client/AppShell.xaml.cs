using P06VehicleDealership.Shared.Languages;
using P12MAUI.Client.ViewModels;
using System.Reflection;

namespace P12MAUI.Client
{
    public partial class AppShell : Shell
    {
        private readonly ILanguageService _languageService;

        public AppShell(ILanguageService languageService)
        {
            InitializeComponent();

            Routing.RegisterRoute(nameof(VehicleDetailsView), typeof(VehicleDetailsView));
            Routing.RegisterRoute(nameof(VehiclesPage), typeof(VehiclesPage));
            Routing.RegisterRoute(nameof(RegisterPage), typeof(RegisterPage));

            _languageService = languageService;
            BindingContext = this;
            SettingsViewModel.LanguageChanged += OnLanguageChanged;
        }
        public string HomeTitle => _languageService.GetLanguage(SettingsViewModel.Language.ToLower(), "HomeNavLink");
        public string SettingsTitle => _languageService.GetLanguage(SettingsViewModel.Language.ToLower(), "SettingsTitle");
        private void OnLanguageChanged(object sender, string newLanguage)
        {
            RefreshAllProperties();
        }
        public void RefreshAllProperties()
        {
            OnPropertyChanged();
            var properties = GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance);
            foreach (var property in properties)
            {
                OnPropertyChanged(property.Name);
            }
        }
    }
}

