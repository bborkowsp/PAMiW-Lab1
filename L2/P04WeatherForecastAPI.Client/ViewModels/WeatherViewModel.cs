using P04WeatherForecastAPI.Client.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P04WeatherForecastAPI.Client.ViewModels
{
    public class WeatherViewModel
    {
        public WeatherViewModel(Weather weather, Weather _weatherYesterdayTemperature,
        Weather _weather6hAgo, OneHourWeatherForecast _weatherForecastIn1h,
        OneHourWeatherForecast _weatherForecastIn12h)
        {
            CurrentTemperature = weather.Temperature.Metric.Value;
            YesterdayTemperature = _weatherYesterdayTemperature.Temperature.Metric.Value;
            Temperature6hAgo = _weather6hAgo.Temperature.Metric.Value;
            WeatherForecastIn1h = _weatherForecastIn1h.IconPhrase;
            WeatherForecastIn12h = _weatherForecastIn12h.IconPhrase;
            HasPrecipitation = weather.HasPrecipitation;
        }
        public double CurrentTemperature { get; set; }
        public double YesterdayTemperature { get; set; }
        public double Temperature6hAgo { get; set; }
        public string WeatherForecastIn1h { get; set; }
        public string WeatherForecastIn12h { get; set; }
        public bool HasPrecipitation { get; set; }
    }
}
