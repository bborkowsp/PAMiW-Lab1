using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace P05Shop.API.Migrations
{
    /// <inheritdoc />
    public partial class AddingVehiclesTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Vehicles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Model = table.Column<string>(type: "nvarchar(12)", maxLength: 12, nullable: false),
                    Fuel = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Vehicles", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Vehicles",
                columns: new[] { "Id", "Fuel", "Model" },
                values: new object[,]
                {
                    { 1, "Hybrid", "A8" },
                    { 2, "Gasoline", "Beetle" },
                    { 3, "Gasoline", "Malibu" },
                    { 4, "Electric", "1" },
                    { 5, "Electric", "XC90" },
                    { 6, "Electric", "Silverado" },
                    { 7, "Electric", "Land Cruiser" },
                    { 8, "Diesel", "XC90" },
                    { 9, "Hybrid", "Mercielago" },
                    { 10, "Electric", "Element" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Vehicles");
        }
    }
}
