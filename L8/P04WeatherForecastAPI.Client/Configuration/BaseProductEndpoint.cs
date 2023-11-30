using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P04WeatherForecastAPI.Client.Configuration
{
    internal class BaseVehicleEndpoint
    {
        public string Base_url { get; set; }
        public string GetAllVehiclesEndpoint { get; set; }
        public string NewVehicleEndpoint { get; set; }
        public string UpdateVehicleEndpoint { get; set; }
        public string DeleteVehicleEndpoint { get; set; }

    }
}
