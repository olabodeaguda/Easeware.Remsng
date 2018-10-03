using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Easeware.Remsng.Entities.Migrations
{
    public partial class verified11 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "VerificationDetails",
                columns: table => new
                {
                    CreatedDate = table.Column<DateTimeOffset>(nullable: false),
                    CreatedBy = table.Column<string>(nullable: true),
                    ModifiedDate = table.Column<DateTimeOffset>(nullable: false),
                    ModifiedBy = table.Column<string>(nullable: true),
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    VerificationCode = table.Column<string>(type: "nvarchar(50)", nullable: false),
                    OwnerId = table.Column<string>(type: "nvarchar(50)", nullable: false),
                    IsVerified = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VerificationDetails", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "VerificationDetails");
        }
    }
}
