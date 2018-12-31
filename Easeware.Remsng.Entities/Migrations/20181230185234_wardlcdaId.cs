using Microsoft.EntityFrameworkCore.Migrations;

namespace Easeware.Remsng.Entities.Migrations
{
    public partial class wardlcdaId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Wards_Lcdas_LcdaCode",
                table: "Wards");

            migrationBuilder.DropIndex(
                name: "IX_Wards_LcdaCode",
                table: "Wards");

            migrationBuilder.DropUniqueConstraint(
                name: "AK_Lcdas_LcdaCode",
                table: "Lcdas");

            migrationBuilder.DropIndex(
                name: "IX_Lcdas_LcdaCode",
                table: "Lcdas");

            migrationBuilder.DropColumn(
                name: "LcdaCode",
                table: "Wards");

            migrationBuilder.AddColumn<long>(
                name: "LcdaId",
                table: "Wards",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AlterColumn<string>(
                name: "LcdaCode",
                table: "Lcdas",
                type: "nvarchar(20)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(20)");

            migrationBuilder.CreateIndex(
                name: "IX_Wards_LcdaId",
                table: "Wards",
                column: "LcdaId");

            migrationBuilder.CreateIndex(
                name: "IX_Lcdas_LcdaCode",
                table: "Lcdas",
                column: "LcdaCode",
                unique: true,
                filter: "[LcdaCode] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_Wards_Lcdas_LcdaId",
                table: "Wards",
                column: "LcdaId",
                principalTable: "Lcdas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Wards_Lcdas_LcdaId",
                table: "Wards");

            migrationBuilder.DropIndex(
                name: "IX_Wards_LcdaId",
                table: "Wards");

            migrationBuilder.DropIndex(
                name: "IX_Lcdas_LcdaCode",
                table: "Lcdas");

            migrationBuilder.DropColumn(
                name: "LcdaId",
                table: "Wards");

            migrationBuilder.AddColumn<string>(
                name: "LcdaCode",
                table: "Wards",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "LcdaCode",
                table: "Lcdas",
                type: "nvarchar(20)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(20)",
                oldNullable: true);

            migrationBuilder.AddUniqueConstraint(
                name: "AK_Lcdas_LcdaCode",
                table: "Lcdas",
                column: "LcdaCode");

            migrationBuilder.CreateIndex(
                name: "IX_Wards_LcdaCode",
                table: "Wards",
                column: "LcdaCode");

            migrationBuilder.CreateIndex(
                name: "IX_Lcdas_LcdaCode",
                table: "Lcdas",
                column: "LcdaCode",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Wards_Lcdas_LcdaCode",
                table: "Wards",
                column: "LcdaCode",
                principalTable: "Lcdas",
                principalColumn: "LcdaCode",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
