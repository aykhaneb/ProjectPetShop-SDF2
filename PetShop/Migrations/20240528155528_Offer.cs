using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PetShop.Migrations
{
    public partial class Offer : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_OfferParts",
                table: "OfferParts");

            migrationBuilder.RenameTable(
                name: "OfferParts",
                newName: "OfferPart");

            migrationBuilder.AddPrimaryKey(
                name: "PK_OfferPart",
                table: "OfferPart",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "Offer",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Offer", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Offer");

            migrationBuilder.DropPrimaryKey(
                name: "PK_OfferPart",
                table: "OfferPart");

            migrationBuilder.RenameTable(
                name: "OfferPart",
                newName: "OfferParts");

            migrationBuilder.AddPrimaryKey(
                name: "PK_OfferParts",
                table: "OfferParts",
                column: "Id");
        }
    }
}
