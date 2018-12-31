using Microsoft.EntityFrameworkCore.Migrations;

namespace Easeware.Remsng.Entities.Migrations
{
    public partial class streetUpdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Streets_Wards_WardCode",
                table: "Streets");

            migrationBuilder.DropUniqueConstraint(
                name: "AK_Wards_WardCode",
                table: "Wards");

            migrationBuilder.DropIndex(
                name: "IX_Wards_WardCode",
                table: "Wards");

            migrationBuilder.DropIndex(
                name: "IX_Streets_WardCode",
                table: "Streets");

            migrationBuilder.DropColumn(
                name: "WardCode",
                table: "Streets");

            migrationBuilder.AlterColumn<string>(
                name: "WardCode",
                table: "Wards",
                type: "nvarchar(20)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(20)");

            migrationBuilder.AddColumn<long>(
                name: "WardId",
                table: "Streets",
                nullable: false,
                defaultValue: 0L);

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

            migrationBuilder.AddForeignKey(
                name: "FK_Streets_Wards_WardId",
                table: "Streets",
                column: "WardId",
                principalTable: "Wards",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Streets_Wards_WardId",
                table: "Streets");

            migrationBuilder.DropIndex(
                name: "IX_Wards_WardCode",
                table: "Wards");

            migrationBuilder.DropIndex(
                name: "IX_Streets_WardId",
                table: "Streets");

            migrationBuilder.DropColumn(
                name: "WardId",
                table: "Streets");

            migrationBuilder.AlterColumn<string>(
                name: "WardCode",
                table: "Wards",
                type: "nvarchar(20)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(20)",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "WardCode",
                table: "Streets",
                nullable: true);

            migrationBuilder.AddUniqueConstraint(
                name: "AK_Wards_WardCode",
                table: "Wards",
                column: "WardCode");

            migrationBuilder.CreateIndex(
                name: "IX_Wards_WardCode",
                table: "Wards",
                column: "WardCode",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Streets_WardCode",
                table: "Streets",
                column: "WardCode");

            migrationBuilder.AddForeignKey(
                name: "FK_Streets_Wards_WardCode",
                table: "Streets",
                column: "WardCode",
                principalTable: "Wards",
                principalColumn: "WardCode",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
