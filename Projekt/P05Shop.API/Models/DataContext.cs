using Microsoft.EntityFrameworkCore;
using P06Shop.Shared.Auth;
using P06Shop.Shared.VehicleDealership;
using P07Shop.DataSeeder;

namespace P05Shop.API.Models
{
    public class DataContext : DbContext
    {

        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }

        public DbSet<Vehicle> Vehicles { get; set; }

        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // fluent api 


            modelBuilder.Entity<Vehicle>()
                .Property(p => p.Model)
                .IsRequired()
                .HasMaxLength(100);

            modelBuilder.Entity<Vehicle>()
                .Property(p => p.Fuel)
                .IsRequired()
                .HasMaxLength(100);



            // data seed 

            modelBuilder.Entity<Vehicle>().HasData(VehicleSeeder.GenerateVehicleData());
        }
    }
}
// instalacja dotnet ef 
//dotnet tool install --global dotnet-ef

// aktualizacja 
//dotnet tool update --global dotnet-ef

// dotnet ef migrations add [nazwa_migracji]
// dotnet ef database update 


// cofniecie migraji niezaplikowanych 
//dotnet ef migrations remove


// Dodanie migracji nazwanej "AddUsers" dodającej Users i aktualizacja bazy:
// dotnet ef migrations add AddUsers
// dotnet ef database update