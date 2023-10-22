using P04WeatherForecastAPI.Client.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P04WeatherForecastAPI.Client.Services
{
    public interface IAccuWeatherService
    {
        Task<City[]> GetLocations(string locationName);
        Task<Weather> GetCurrentConditions(string cityKey);
        Task<Weather> GetHistoricalCurrentConditionsPast24h(string cityKey);
        Task<Weather> GetHistoricalCurrentConditionsPast6h(string cityKey);
        Task<OneHourWeatherForecast> GetWeatherDescriptionOneHourForecast(string cityKey);
        Task<OneHourWeatherForecast> GetWeatherDescriptionIn12Hours(string cityKey);
        Task<AdministrativeArea> GetAdministrativeArea(string regionCode);
    }
}
