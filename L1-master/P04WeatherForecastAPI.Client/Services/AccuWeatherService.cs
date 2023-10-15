using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using P04WeatherForecastAPI.Client.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace P04WeatherForecastAPI.Client.Services
{
    internal class AccuWeatherService
    {
        private const string base_url = "http://dataservice.accuweather.com";
        private const string autocomplete_endpoint = "locations/v1/cities/autocomplete?apikey={0}&q={1}&language{2}";
        private const string current_conditions_endpoint = "currentconditions/v1/{0}?apikey={1}&language{2}";
        private const string historical_conditions_past_24h_endpoint = "currentconditions/v1/{0}/historical/24?apikey={1}&language{2}";
        private const string historical_conditions_past_6h_endpoint = "currentconditions/v1/{0}/historical/?apikey={1}&language{2}";
        private const string one_hour_of_hourly_forecasts = "forecasts/v1/hourly/1hour/{0}?apikey={1}&language{2}";
        private const string twelve_hour_of_hourly_forecasts = "forecasts/v1/hourly/12hour/{0}?apikey={1}&language{2}";
        private const string admin_area_list_endpoint = "locations/v1/adminareas/{0}?apikey={1}&language{2}";

        // private const string api_key = "";
        string api_key;
        //private const string language = "pl";
        string language;

        public AccuWeatherService()
        {
            var builder = new ConfigurationBuilder()
                .AddUserSecrets<App>()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsetings.json");

            var configuration = builder.Build();
            api_key = configuration["api_key"];
            language = configuration["default_language"];
        }


        public async Task<City[]> GetLocations(string locationName)
        {
            string uri = base_url + "/" + string.Format(autocomplete_endpoint, api_key, locationName, language);
            using (HttpClient client = new HttpClient())
            {
                var response = await client.GetAsync(uri);
                string json = await response.Content.ReadAsStringAsync();
                City[] cities = JsonConvert.DeserializeObject<City[]>(json);
                return cities;

            }
        }

        public async Task<Weather> GetCurrentConditions(string cityKey)
        {
            string uri = base_url + "/" + string.Format(current_conditions_endpoint, cityKey, api_key, language);
            using (HttpClient client = new HttpClient())
            {
                var response = await client.GetAsync(uri);
                string json = await response.Content.ReadAsStringAsync();
                Weather[] weathers = JsonConvert.DeserializeObject<Weather[]>(json);
                return weathers.FirstOrDefault();
            }
        }
        public async Task<Weather> GetHistoricalCurrentConditionsPast24h(string cityKey)
        {
            string uri = base_url + "/" + string.Format(historical_conditions_past_24h_endpoint, cityKey, api_key, language);
            using (HttpClient client = new HttpClient())
            {
                var response = await client.GetAsync(uri);
                string json = await response.Content.ReadAsStringAsync();
                Weather[] weathers = JsonConvert.DeserializeObject<Weather[]>(json);
                return weathers[23];
            }
        }
        public async Task<Weather> GetHistoricalCurrentConditionsPast6h(string cityKey)
        {
            string uri = base_url + "/" + string.Format(historical_conditions_past_6h_endpoint, cityKey, api_key, language);
            using (HttpClient client = new HttpClient())
            {
                var response = await client.GetAsync(uri);
                string json = await response.Content.ReadAsStringAsync();
                Weather[] weathers = JsonConvert.DeserializeObject<Weather[]>(json);
                return weathers[5];
            }
        }
        public async Task<OneHourWeatherForecast> GetWeatherDescriptionOneHourForecast(string cityKey)
        {
            string uri = base_url + "/" + string.Format(one_hour_of_hourly_forecasts, cityKey, api_key, language);
            using (HttpClient client = new HttpClient())
            {
                var response = await client.GetAsync(uri);
                string json = await response.Content.ReadAsStringAsync();
                OneHourWeatherForecast[] oneHourWeathers = JsonConvert.DeserializeObject<OneHourWeatherForecast[]>(json);
                return oneHourWeathers.FirstOrDefault();
            }
        }
        public async Task<OneHourWeatherForecast> GetWeatherDescriptionIn12Hours(string cityKey)
        {
            string uri = base_url + "/" + string.Format(twelve_hour_of_hourly_forecasts, cityKey, api_key, language);
            using (HttpClient client = new HttpClient())
            {
                var response = await client.GetAsync(uri);
                string json = await response.Content.ReadAsStringAsync();
                OneHourWeatherForecast[] oneHourWeathers = JsonConvert.DeserializeObject<OneHourWeatherForecast[]>(json);
                return oneHourWeathers[11];
            }
        }
        public async Task<int> GetAdministrativeAreasNumber(string regionCode)
        {
            string uri = base_url + "/" + string.Format(admin_area_list_endpoint, regionCode, api_key, language);
            using (HttpClient client = new HttpClient())
            {
                var response = await client.GetAsync(uri);
                string json = await response.Content.ReadAsStringAsync();
                AdministrativeArea[] administrativeAreas = JsonConvert.DeserializeObject<AdministrativeArea[]>(json);
                return administrativeAreas.Length;
            }
        }
    }
}
