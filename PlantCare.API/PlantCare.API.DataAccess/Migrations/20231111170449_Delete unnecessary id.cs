using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PlantCare.API.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class Deleteunnecessaryid : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PlantId",
                table: "Module");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PlantId",
                table: "Module",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
