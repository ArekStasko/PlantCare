using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PlantCare.API.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class AddModulemodel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CriticalMoistureLevel",
                table: "Plant");

            migrationBuilder.DropColumn(
                name: "MoistureLevel",
                table: "Plant");

            migrationBuilder.DropColumn(
                name: "RequiredMoistureLevel",
                table: "Plant");

            migrationBuilder.AlterColumn<int>(
                name: "ModuleId",
                table: "Plant",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.CreateTable(
                name: "Module",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PlantId = table.Column<int>(type: "int", nullable: false),
                    CurrentMoistureLevel = table.Column<int>(type: "int", nullable: true),
                    RequiredMoistureLevel = table.Column<int>(type: "int", nullable: false),
                    CriticalMoistureLevel = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Module", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "HumidityMeasurement",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ModuleId = table.Column<int>(type: "int", nullable: false),
                    Humidity = table.Column<int>(type: "int", nullable: false),
                    MeasurementDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HumidityMeasurement", x => x.Id);
                    table.ForeignKey(
                        name: "FK_HumidityMeasurement_Module_ModuleId",
                        column: x => x.ModuleId,
                        principalTable: "Module",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Plant_ModuleId",
                table: "Plant",
                column: "ModuleId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_HumidityMeasurement_ModuleId",
                table: "HumidityMeasurement",
                column: "ModuleId");

            migrationBuilder.AddForeignKey(
                name: "FK_Plant_Module_ModuleId",
                table: "Plant",
                column: "ModuleId",
                principalTable: "Module",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Plant_Module_ModuleId",
                table: "Plant");

            migrationBuilder.DropTable(
                name: "HumidityMeasurement");

            migrationBuilder.DropTable(
                name: "Module");

            migrationBuilder.DropIndex(
                name: "IX_Plant_ModuleId",
                table: "Plant");

            migrationBuilder.AlterColumn<string>(
                name: "ModuleId",
                table: "Plant",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<byte>(
                name: "CriticalMoistureLevel",
                table: "Plant",
                type: "tinyint",
                nullable: false,
                defaultValue: (byte)0);

            migrationBuilder.AddColumn<byte>(
                name: "MoistureLevel",
                table: "Plant",
                type: "tinyint",
                nullable: false,
                defaultValue: (byte)0);

            migrationBuilder.AddColumn<byte>(
                name: "RequiredMoistureLevel",
                table: "Plant",
                type: "tinyint",
                nullable: false,
                defaultValue: (byte)0);
        }
    }
}
