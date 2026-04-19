using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PlantCare.Persistance.WriteDataManager.Migrations
{
    /// <inheritdoc />
    public partial class batterylevel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Address",
                table: "Modules");

            migrationBuilder.DropColumn(
                name: "IsMonitoring",
                table: "Modules");

            migrationBuilder.AddColumn<int>(
                name: "BatteryLevel",
                table: "HumidityMeasurements",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BatteryLevel",
                table: "HumidityMeasurements");

            migrationBuilder.AddColumn<string>(
                name: "Address",
                table: "Modules",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<bool>(
                name: "IsMonitoring",
                table: "Modules",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}
