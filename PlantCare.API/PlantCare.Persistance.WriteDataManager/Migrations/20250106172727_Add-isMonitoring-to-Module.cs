using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PlantCare.Persistance.WriteDataManager.Migrations
{
    /// <inheritdoc />
    public partial class AddisMonitoringtoModule : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsMonitoring",
                table: "Modules",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsMonitoring",
                table: "Modules");
        }
    }
}
