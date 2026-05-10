using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PlantCare.Persistance.ReadDataManager.Migrations
{
    /// <inheritdoc />
    public partial class humiditystatus : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "maxHumidity",
                table: "Plants",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "minHumidity",
                table: "Plants",
                type: "int",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "maxHumidity",
                table: "Plants");

            migrationBuilder.DropColumn(
                name: "minHumidity",
                table: "Plants");
        }
    }
}
