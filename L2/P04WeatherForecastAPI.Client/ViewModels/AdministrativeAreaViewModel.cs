using P04WeatherForecastAPI.Client.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P04WeatherForecastAPI.Client.ViewModels
{
    public class AdministrativeAreaViewModel
    {
        public string LocalizedName { get; set; }

        public AdministrativeAreaViewModel(AdministrativeArea administrativeArea)
        {
            LocalizedName = administrativeArea.LocalizedName;
        }

    }
}
