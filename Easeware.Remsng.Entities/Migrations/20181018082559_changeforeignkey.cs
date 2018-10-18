using Microsoft.EntityFrameworkCore.Migrations;

namespace Easeware.Remsng.Entities.Migrations
{
    public partial class changeforeignkey : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Addresses_Streets_StreetId",
                table: "Addresses");

            migrationBuilder.DropForeignKey(
                name: "FK_Streets_Wards_WardId",
                table: "Streets");

            migrationBuilder.DropForeignKey(
                name: "FK_Wards_Lcdas_LcdaId",
                table: "Wards");

            migrationBuilder.DropIndex(
                name: "IX_Wards_LcdaId",
                table: "Wards");

            migrationBuilder.DropIndex(
                name: "IX_Wards_WardCode",
                table: "Wards");

            migrationBuilder.DropIndex(
                name: "IX_Streets_WardId",
                table: "Streets");

            migrationBuilder.DropIndex(
                name: "IX_Lcdas_LcdaCode",
                table: "Lcdas");

            migrationBuilder.DropIndex(
                name: "IX_Addresses_StreetId",
                table: "Addresses");

            migrationBuilder.DropColumn(
                name: "LcdaId",
                table: "Wards");

            migrationBuilder.DropColumn(
                name: "WardId",
                table: "Streets");

            migrationBuilder.DropColumn(
                name: "LcdaId",
                table: "Sectors");

            migrationBuilder.DropColumn(
                name: "StreetId",
                table: "Addresses");

            migrationBuilder.AlterColumn<string>(
                name: "WardCode",
                table: "Wards",
                type: "nvarchar(20)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(20)",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LcdaCode",
                table: "Wards",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "StreetCode",
                table: "Streets",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "WardCode",
                table: "Streets",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LcdaCode",
                table: "Sectors",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "LcdaCode",
                table: "Lcdas",
                type: "nvarchar(20)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(20)",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "StreetCode",
                table: "Addresses",
                nullable: true);

            migrationBuilder.AddUniqueConstraint(
                name: "AK_Wards_WardCode",
                table: "Wards",
                column: "WardCode");

            migrationBuilder.AddUniqueConstraint(
                name: "AK_Streets_StreetCode",
                table: "Streets",
                column: "StreetCode");

            migrationBuilder.AddUniqueConstraint(
                name: "AK_Lcdas_LcdaCode",
                table: "Lcdas",
                column: "LcdaCode");

            migrationBuilder.CreateIndex(
                name: "IX_Wards_LcdaCode",
                table: "Wards",
                column: "LcdaCode");

            migrationBuilder.CreateIndex(
                name: "IX_Wards_WardCode",
                table: "Wards",
                column: "WardCode",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Streets_StreetCode",
                table: "Streets",
                column: "StreetCode",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Streets_WardCode",
                table: "Streets",
                column: "WardCode");

            migrationBuilder.CreateIndex(
                name: "IX_Lcdas_LcdaCode",
                table: "Lcdas",
                column: "LcdaCode",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Addresses_StreetCode",
                table: "Addresses",
                column: "StreetCode");

            migrationBuilder.AddForeignKey(
                name: "FK_Addresses_Streets_StreetCode",
                table: "Addresses",
                column: "StreetCode",
                principalTable: "Streets",
                principalColumn: "StreetCode",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Streets_Wards_WardCode",
                table: "Streets",
                column: "WardCode",
                principalTable: "Wards",
                principalColumn: "WardCode",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Wards_Lcdas_LcdaCode",
                table: "Wards",
                column: "LcdaCode",
                principalTable: "Lcdas",
                principalColumn: "LcdaCode",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Addresses_Streets_StreetCode",
                table: "Addresses");

            migrationBuilder.DropForeignKey(
                name: "FK_Streets_Wards_WardCode",
                table: "Streets");

            migrationBuilder.DropForeignKey(
                name: "FK_Wards_Lcdas_LcdaCode",
                table: "Wards");

            migrationBuilder.DropUniqueConstraint(
                name: "AK_Wards_WardCode",
                table: "Wards");

            migrationBuilder.DropIndex(
                name: "IX_Wards_LcdaCode",
                table: "Wards");

            migrationBuilder.DropIndex(
                name: "IX_Wards_WardCode",
                table: "Wards");

            migrationBuilder.DropUniqueConstraint(
                name: "AK_Streets_StreetCode",
                table: "Streets");

            migrationBuilder.DropIndex(
                name: "IX_Streets_StreetCode",
                table: "Streets");

            migrationBuilder.DropIndex(
                name: "IX_Streets_WardCode",
                table: "Streets");

            migrationBuilder.DropUniqueConstraint(
                name: "AK_Lcdas_LcdaCode",
                table: "Lcdas");

            migrationBuilder.DropIndex(
                name: "IX_Lcdas_LcdaCode",
                table: "Lcdas");

            migrationBuilder.DropIndex(
                name: "IX_Addresses_StreetCode",
                table: "Addresses");

            migrationBuilder.DropColumn(
                name: "LcdaCode",
                table: "Wards");

            migrationBuilder.DropColumn(
                name: "StreetCode",
                table: "Streets");

            migrationBuilder.DropColumn(
                name: "WardCode",
                table: "Streets");

            migrationBuilder.DropColumn(
                name: "LcdaCode",
                table: "Sectors");

            migrationBuilder.DropColumn(
                name: "StreetCode",
                table: "Addresses");

            migrationBuilder.AlterColumn<string>(
                name: "WardCode",
                table: "Wards",
                type: "nvarchar(20)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(20)");

            migrationBuilder.AddColumn<long>(
                name: "LcdaId",
                table: "Wards",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "WardId",
                table: "Streets",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "LcdaId",
                table: "Sectors",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AlterColumn<string>(
                name: "LcdaCode",
                table: "Lcdas",
                type: "nvarchar(20)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(20)");

            migrationBuilder.AddColumn<long>(
                name: "StreetId",
                table: "Addresses",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.CreateIndex(
                name: "IX_Wards_LcdaId",
                table: "Wards",
                column: "LcdaId");

            migrationBuilder.CreateIndex(
                name: "IX_Wards_WardCode",
                table: "Wards",
                column: "WardCode",
                unique: true,
                filter: "[WardCode] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Streets_WardId",
                table: "Streets",
                column: "WardId");

            migrationBuilder.CreateIndex(
                name: "IX_Lcdas_LcdaCode",
                table: "Lcdas",
                column: "LcdaCode",
                unique: true,
                filter: "[LcdaCode] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Addresses_StreetId",
                table: "Addresses",
                column: "StreetId");

            migrationBuilder.AddForeignKey(
                name: "FK_Addresses_Streets_StreetId",
                table: "Addresses",
                column: "StreetId",
                principalTable: "Streets",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Streets_Wards_WardId",
                table: "Streets",
                column: "WardId",
                principalTable: "Wards",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Wards_Lcdas_LcdaId",
                table: "Wards",
                column: "LcdaId",
                principalTable: "Lcdas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
