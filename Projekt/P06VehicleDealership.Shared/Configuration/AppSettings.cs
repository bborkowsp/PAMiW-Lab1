using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P06VehicleDealership.Shared.Configuration
{
    public class AppSettings
    {
        public string BaseAPIUrl { get; set; }
        public VehicleDealershipEndpoints VehicleDealershipEndpoints { get; set; }

    }
}
