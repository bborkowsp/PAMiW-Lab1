﻿using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Maui.Controls;
using P12MAUI.Client.ViewModels;
using P06VehicleDealership.Shared.Configuration;
using P06VehicleDealership.Shared.MessageBox;
using P06VehicleDealership.Shared.Services.AuthService;
using P06VehicleDealership.Shared.Services.VehicleDealershipService;
using P12MAUI.Client.MessageBox;
using P06VehicleDealership.Shared.Languages;
using Microsoft.AspNetCore.Components.Authorization;
using P12MAUI.Client.Services.CustomAuthStateProvider;
using System.Diagnostics;

namespace P12MAUI.Client
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
              .UseMauiApp<App>()
              .ConfigureFonts(fonts =>
              {
                  fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                  fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
              });

#if DEBUG
      builder.Logging.AddDebug();
#endif

            ConfigureServices(builder.Services);
            return builder.Build();
        }

        private static void ConfigureServices(IServiceCollection services)
        {
            var appSettingsSection = ConfigureAppSettings(services);
            ConfigureAppServices(services, appSettingsSection);
            ConfigureViewModels(services);
            ConfigureViews(services);
            ConfigureHttpClients(services, appSettingsSection);
        }

        private static AppSettings ConfigureAppSettings(IServiceCollection services)
        {

            string workingDirectory = AppContext.BaseDirectory;
            string projectDir = Directory.GetParent(workingDirectory).Parent.Parent.Parent.Parent.Parent.FullName;

            string s = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
            var builder = new ConfigurationBuilder()
              .AddUserSecrets<MauiApp>()
              .SetBasePath(projectDir)
              .AddJsonFile("appsettings.json");
            IConfiguration _configuration = builder.Build();

            var appSettings = _configuration.GetSection(nameof(AppSettings));
            var appSettingsSection = appSettings.Get<AppSettings>();

            services.AddSingleton(appSettingsSection);

            // string baseUrl;
            // if (Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") == "production")
            // {
            //     baseUrl = "https://localhost:7230";
            // }

            // var appSettingsSection = new AppSettings()
            // {
            //     BaseAPIUrl = "https://localhost:7230",
            //     VehicleDealershipEndpoints = new VehicleDealershipEndpoints()
            //     {
            //         Base_url = "api/Vehicle/",
            //         GetVehiclesEndpoint= "api/Vehicle",
            //         GetVehicleEndpoint= "api/Vehicle/{0}",
            //         UpdateVehicleEndpoint= "api/Vehicle/{0}",
            //         DeleteVehicleEndpoint= "api/Vehicle/{0}",
            //         AddVehicleEndpoint= "api/Vehicle",
            //         SearchVehiclesEndpoint= "api/Vehicle/search"
            //     },

            // };
            //  services.AddSingleton(appSettingsSection);

            return appSettingsSection;
        }

        private static void ConfigureAppServices(IServiceCollection services, AppSettings appSettings)
        {
            services.AddSingleton<IConnectivity>(Connectivity.Current);
            services.AddSingleton<IGeolocation>(Geolocation.Default);
            services.AddSingleton<IMap>(Map.Default);
            services.AddSingleton<IVehicleDealershipService, VehicleDealershipService>();
            services.AddSingleton<IMessageDialogService, MauiMessageDialogService>();
            services.AddSingleton<ILanguageService, LanguageService>();
            services.AddSingleton<ILanguageService>(languageService =>
            {
                return new LanguageService();
            });
        }

        private static void ConfigureViewModels(IServiceCollection services)
        {
            services.AddSingleton<VehiclesViewModel>();
            services.AddTransient<VehicleDetailsViewModel>();
            services.AddSingleton<MainViewModel>();
            services.AddSingleton<SettingsViewModel>();
            services.AddSingleton<RegisterViewModel>();
        }

        private static void ConfigureViews(IServiceCollection services)
        {
            services.AddSingleton<MainPage>();
            services.AddTransient<VehicleDetailsView>();
            services.AddSingleton<VehiclesPage>();
            services.AddSingleton<SettingsPage>();
            services.AddSingleton<RegisterPage>();
        }

        private static void ConfigureHttpClients(IServiceCollection services, AppSettings appSettingsSection)
        {
            var uriBuilder = new UriBuilder(appSettingsSection.BaseAPIUrl) { };
            services.AddHttpClient<IVehicleDealershipService, VehicleDealershipService>(client => client.BaseAddress = uriBuilder.Uri);
            services.AddHttpClient<IAuthService, AuthService>(client => client.BaseAddress = uriBuilder.Uri);
            services.AddScoped<AuthenticationStateProvider, CustomAuthStateProvider>();
        }
    }
}