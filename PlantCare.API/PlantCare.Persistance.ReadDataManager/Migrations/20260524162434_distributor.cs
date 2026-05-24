using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PlantCare.Persistance.ReadDataManager.Migrations
{
    /// <inheritdoc />
    public partial class distributor : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "DistributorId",
                table: "Plants",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Distributors",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Distributors", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Distributors");

            migrationBuilder.DropColumn(
                name: "DistributorId",
                table: "Plants");
        }
    }
}
