using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Easeware.Remsng.Entities.Migrations
{
    public partial class userdetails : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    username = table.Column<string>(nullable: false),
                    email = table.Column<string>(nullable: false),
                    lastname = table.Column<string>(nullable: false),
                    otherNames = table.Column<string>(nullable: true),
                    gender = table.Column<string>(nullable: false),
                    passwordHash = table.Column<string>(nullable: true),
                    securityStamp = table.Column<string>(nullable: true),
                    lockedOutEndDateUTC = table.Column<DateTimeOffset>(nullable: true),
                    lockedoutenabled = table.Column<bool>(nullable: false),
                    userStatus = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "UserLcdas",
                columns: table => new
                {
                    UserId = table.Column<long>(nullable: false),
                    LcdaId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserLcdas", x => new { x.LcdaId, x.UserId });
                    table.ForeignKey(
                        name: "FK_UserLcdas_Lcdas_LcdaId",
                        column: x => x.LcdaId,
                        principalTable: "Lcdas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserLcdas_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UserLcdas_UserId",
                table: "UserLcdas",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserLcdas");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
