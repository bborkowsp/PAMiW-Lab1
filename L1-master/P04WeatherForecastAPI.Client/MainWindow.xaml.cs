using P04WeatherForecastAPI.Client.Models;
using P04WeatherForecastAPI.Client.Services;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace P04WeatherForecastAPI.Client
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        AccuWeatherService accuWeatherService;
        public MainWindow()
        {
            InitializeComponent();
            accuWeatherService = new AccuWeatherService();
        }

        private async void btnSearch_Click(object sender, RoutedEventArgs e)
        {

            City[] cities = await accuWeatherService.GetLocations(txtCity.Text);

            // standardowy sposób dodawania elementów
            //lbData.Items.Clear();
            //foreach (var c in cities)
            //    lbData.Items.Add(c.LocalizedName);

            // teraz musimy skorzystac z bindowania danych bo chcemy w naszej kontrolce przechowywac takze id miasta 
            lbData.ItemsSource = cities;

            await ShowYesterdayTemperature();
            await ShowPast6hTemperature();
            await ShowWeatherDescriptionInOneHour();
            await ShowTemperatureIn12HourForecast();
        }
        private async void btnGetRegionCode(object sender, RoutedEventArgs e)
        {
            string textFromTextBox = TextRegionCode.Text;

            int numberOfAministrativeArea = await accuWeatherService.GetAdministrativeAreasNumber(textFromTextBox);
            lblAdminAreaList.Content = Convert.ToString(numberOfAministrativeArea);

        }

        private async void lbData_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var selectedCity = (City)lbData.SelectedItem;
            if (selectedCity != null)
            {
                var weather = await accuWeatherService.GetCurrentConditions(selectedCity.Key);
                lblCityName.Content = selectedCity.LocalizedName;
                double tempValue = weather.Temperature.Metric.Value;
                lblTemperatureValue.Content = Convert.ToString(tempValue);

                await ShowYesterdayTemperature();
                await ShowPast6hTemperature();
                await ShowWeatherDescriptionInOneHour();
                await ShowTemperatureIn12HourForecast();

            }
        }
        private async Task ShowYesterdayTemperature()
        {
            var selectedCity = (City)lbData.SelectedItem;
            if (selectedCity != null)
            {
                var weather = await accuWeatherService.GetHistoricalCurrentConditionsPast24h(selectedCity.Key);
                double tempValue = weather.Temperature.Metric.Value;
                lblTemperatureValueYesterday.Content = Convert.ToString(tempValue);
            }
        }
        private async Task ShowPast6hTemperature()
        {
            var selectedCity = (City)lbData.SelectedItem;
            if (selectedCity != null)
            {
                var weather = await accuWeatherService.GetHistoricalCurrentConditionsPast6h(selectedCity.Key);
                double tempValue = weather.Temperature.Metric.Value;
                lblTemperatureValuePast6h.Content = Convert.ToString(tempValue);
            }
        }
        private async Task ShowWeatherDescriptionInOneHour()
        {
            var selectedCity = (City)lbData.SelectedItem;
            if (selectedCity != null)
            {
                var oneHourWeatherForecast = await accuWeatherService.GetWeatherDescriptionOneHourForecast(selectedCity.Key);
                string weatherDescription = oneHourWeatherForecast.IconPhrase;
                lblWeatherDescription.Content = Convert.ToString(weatherDescription);
            }
        }
        private async Task ShowTemperatureIn12HourForecast()
        {
            var selectedCity = (City)lbData.SelectedItem;
            if (selectedCity != null)
            {
                var oneHourWeatherForecast = await accuWeatherService.GetWeatherDescriptionIn12Hours(selectedCity.Key);
                string weatherDescription = oneHourWeatherForecast.IconPhrase;
                lblWeatherDescriptionIn12Hour.Content = Convert.ToString(weatherDescription);
            }
        }

    }
}
