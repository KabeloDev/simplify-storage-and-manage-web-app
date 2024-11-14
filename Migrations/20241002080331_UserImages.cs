using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TextEditor.Migrations
{
    /// <inheritdoc />
    public partial class UserImages : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Events_Locations_LocationsId",
                table: "Events");

            migrationBuilder.DropTable(
                name: "Locations");

            migrationBuilder.DropIndex(
                name: "IX_Events_LocationsId",
                table: "Events");

            migrationBuilder.DropColumn(
                name: "LocationsId",
                table: "Events");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "LocationsId",
                table: "Events",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Locations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Locations", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Events_LocationsId",
                table: "Events",
                column: "LocationsId");

            migrationBuilder.AddForeignKey(
                name: "FK_Events_Locations_LocationsId",
                table: "Events",
                column: "LocationsId",
                principalTable: "Locations",
                principalColumn: "Id");
        }
    }
}
