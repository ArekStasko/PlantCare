using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PlantCare.Persistance.WriteDataManager.Migrations
{
    /// <inheritdoc />
    public partial class removemeasurementsfromplant : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_HumidityMeasurements_Plants_PlantId",
                table: "HumidityMeasurements");

            migrationBuilder.DropIndex(
                name: "IX_HumidityMeasurements_PlantId",
                table: "HumidityMeasurements");

            migrationBuilder.DropColumn(
                name: "PlantId",
                table: "HumidityMeasurements");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PlantId",
                table: "HumidityMeasurements",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_HumidityMeasurements_PlantId",
                table: "HumidityMeasurements",
                column: "PlantId");

            migrationBuilder.AddForeignKey(
                name: "FK_HumidityMeasurements_Plants_PlantId",
                table: "HumidityMeasurements",
                column: "PlantId",
                principalTable: "Plants",
                principalColumn: "Id");
        }
    }
}
