using Microsoft.EntityFrameworkCore.Migrations;

namespace Easeware.Remsng.Entities.Migrations
{
    public partial class removesectorcode_unique : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Sectors_SectorCode",
                table: "Sectors");

            migrationBuilder.AddColumn<long>(
                name: "LcdaId",
                table: "Sectors",
                nullable: false,
                defaultValue: 0L);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LcdaId",
                table: "Sectors");

            migrationBuilder.CreateIndex(
                name: "IX_Sectors_SectorCode",
                table: "Sectors",
                column: "SectorCode",
                unique: true,
                filter: "[SectorCode] IS NOT NULL");
        }
    }
}
