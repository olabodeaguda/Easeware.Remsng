using Microsoft.EntityFrameworkCore.Migrations;

namespace Easeware.Remsng.Entities.Migrations
{
    public partial class db_update : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "LcdaStatus",
                table: "Lcdas",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LcdaStatus",
                table: "Lcdas");
        }
    }
}
