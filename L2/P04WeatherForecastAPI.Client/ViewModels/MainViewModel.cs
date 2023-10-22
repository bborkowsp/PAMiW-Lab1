using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using Microsoft.Extensions.DependencyInjection;
using P04WeatherForecastAPI.Client.Commands;
using P04WeatherForecastAPI.Client.DataSeeders;
using P04WeatherForecastAPI.Client.Models;
using P04WeatherForecastAPI.Client.Services;
using P04WeatherForecastAPI.Client.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows;

namespace P04WeatherForecastAPI.Client.ViewModels
{
    // przekazywanie wartosci do innego formularza 
    public partial class MainViewModel : ObservableObject
    {
        private CityViewModel _selectedCity;
        private Weather _weather;
        private Weather _weatherYesterdayTemperature;
        private Weather _weather6hAgo;
        private OneHourWeatherForecast _weatherForecastIn1h;
        private OneHourWeatherForecast _weatherForecastIn12h;
        private readonly IAccuWeatherService _accuWeatherService;

        public MainViewModel(IAccuWeatherService accuWeatherService)
        {
            _accuWeatherService = accuWeatherService;
            Cities = new ObservableCollection<CityViewModel>(); // podejście nr 2 
        }

        [ObservableProperty]
        private WeatherViewModel weatherView;


        public CityViewModel SelectedCity
        {
            get => _selectedCity;
            set
            {
                _selectedCity = value;
                OnPropertyChanged();
                LoadWeather();
            }
        }


        private async void LoadWeather()
        {
            if (SelectedCity != null)
            {
                _weather = await _accuWeatherService.GetCurrentConditions(SelectedCity.Key);
                _weatherYesterdayTemperature = await _accuWeatherService.GetHistoricalCurrentConditionsPast24h(SelectedCity.Key);
                _weather6hAgo = await _accuWeatherService.GetHistoricalCurrentConditionsPast6h(SelectedCity.Key);
                _weatherForecastIn1h = await _accuWeatherService.GetWeatherDescriptionOneHourForecast(SelectedCity.Key);
                _weatherForecastIn12h = await _accuWeatherService.GetWeatherDescriptionIn12Hours(SelectedCity.Key);
                WeatherView = new WeatherViewModel(_weather, _weatherYesterdayTemperature, _weather6hAgo, _weatherForecastIn1h,
                _weatherForecastIn12h);
            }
        }

        // podajście nr 2 do przechowywania kolekcji obiektów:
        public ObservableCollection<CityViewModel> Cities { get; set; }

        [RelayCommand]
        public async void LoadCities(string locationName)
        {
            // podejście nr 2:
            var cities = await _accuWeatherService.GetLocations(locationName);
            Cities.Clear();
            foreach (var city in cities)
                Cities.Add(new CityViewModel(city));
        }

        private AdministrativeArea _administrativeArea;
        [RelayCommand]
        public async void LoadRegionCode(string regionCode)
        {
            _administrativeArea = await _accuWeatherService.GetAdministrativeArea(regionCode);
            SelectedAdministrativeArea = new AdministrativeAreaViewModel(_administrativeArea);
        }
        private AdministrativeAreaViewModel _selectedAdministrativeArea;
        public AdministrativeAreaViewModel SelectedAdministrativeArea
        {
            get => _selectedAdministrativeArea;
            set
            {
                _selectedAdministrativeArea = value;
                OnPropertyChanged();
            }
        }
    }
}
