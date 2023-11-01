using Bogus;
using P06Shop.Shared.VehicleDealership;

namespace P07Shop.DataSeeder
{
    public class VehicleSeeder
    {
        public static List<Vehicle> GenerateVehicleData()
        {
            int VehicleId = 1;
            var VehicleFaker = new Faker<Vehicle>()
                .RuleFor(x => x.Name, x => x.Vehicle.Model())
                .RuleFor(x => x.Fuel, x => x.Vehicle.Fuel())
                .RuleFor(x => x.Id, x => VehicleId++);

            return VehicleFaker.Generate(10).ToList();

        }
    }
}