using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PlantCare.Persistance.WriteDataManager.Migrations
{
    /// <inheritdoc />
    public partial class removewrongrelations : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_HumidityMeasurements_Modules_ModuleId",
                table: "HumidityMeasurements");

            migrationBuilder.DropIndex(
                name: "IX_HumidityMeasurements_ModuleId",
                table: "HumidityMeasurements");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_HumidityMeasurements_ModuleId",
                table: "HumidityMeasurements",
                column: "ModuleId");

            migrationBuilder.AddForeignKey(
                name: "FK_HumidityMeasurements_Modules_ModuleId",
                table: "HumidityMeasurements",
                column: "ModuleId",
                principalTable: "Modules",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
