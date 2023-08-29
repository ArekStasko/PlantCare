using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PlantCare.API.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class AddPlantandRecordmodels : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImageUrl",
                table: "Plants");

            migrationBuilder.AlterColumn<byte>(
                name: "RequiredMoistureLevel",
                table: "Plants",
                type: "tinyint",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<byte>(
                name: "MoistureLevel",
                table: "Plants",
                type: "tinyint",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<byte>(
                name: "CriticalMoistureLevel",
                table: "Plants",
                type: "tinyint",
                nullable: false,
                defaultValue: (byte)0);

            migrationBuilder.AddColumn<int>(
                name: "Type",
                table: "Plants",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Records",
                columns: table => new
                {
                    PlantId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MoistureLevel = table.Column<byte>(type: "tinyint", nullable: false),
                    IrrigationDate = table.Column<TimeSpan>(type: "time", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Records", x => x.PlantId);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Records");

            migrationBuilder.DropColumn(
                name: "CriticalMoistureLevel",
                table: "Plants");

            migrationBuilder.DropColumn(
                name: "Type",
                table: "Plants");

            migrationBuilder.AlterColumn<int>(
                name: "RequiredMoistureLevel",
                table: "Plants",
                type: "int",
                nullable: false,
                oldClrType: typeof(byte),
                oldType: "tinyint");

            migrationBuilder.AlterColumn<int>(
                name: "MoistureLevel",
                table: "Plants",
                type: "int",
                nullable: false,
                oldClrType: typeof(byte),
                oldType: "tinyint");

            migrationBuilder.AddColumn<string>(
                name: "ImageUrl",
                table: "Plants",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
