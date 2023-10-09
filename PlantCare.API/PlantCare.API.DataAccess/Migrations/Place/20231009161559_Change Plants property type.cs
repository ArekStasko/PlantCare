using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PlantCare.API.DataAccess.Migrations.Place
{
    /// <inheritdoc />
    public partial class ChangePlantspropertytype : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RoomPlant");

            migrationBuilder.CreateIndex(
                name: "IX_Plant_PlaceId",
                table: "Plant",
                column: "PlaceId");

            migrationBuilder.AddForeignKey(
                name: "FK_Plant_Places_PlaceId",
                table: "Plant",
                column: "PlaceId",
                principalTable: "Places",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Plant_Places_PlaceId",
                table: "Plant");

            migrationBuilder.DropIndex(
                name: "IX_Plant_PlaceId",
                table: "Plant");

            migrationBuilder.CreateTable(
                name: "RoomPlant",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PlantId = table.Column<int>(type: "int", nullable: false),
                    PlaceId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RoomPlant", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RoomPlant_Places_PlaceId",
                        column: x => x.PlaceId,
                        principalTable: "Places",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_RoomPlant_Plant_PlantId",
                        column: x => x.PlantId,
                        principalTable: "Plant",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_RoomPlant_PlaceId",
                table: "RoomPlant",
                column: "PlaceId");

            migrationBuilder.CreateIndex(
                name: "IX_RoomPlant_PlantId",
                table: "RoomPlant",
                column: "PlantId");
        }
    }
}
