using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Easeware.Remsng.Entities.Migrations
{
    public partial class updateTaxpayer : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Taxpayers",
                columns: table => new
                {
                    CreatedDate = table.Column<DateTimeOffset>(nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(100)", nullable: true),
                    ModifiedDate = table.Column<DateTimeOffset>(nullable: true),
                    ModifiedBy = table.Column<string>(type: "nvarchar(100)", nullable: true),
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CompanyId = table.Column<long>(nullable: false),
                    AddressId = table.Column<long>(nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(150)", nullable: true),
                    OtherNames = table.Column<string>(type: "nvarchar(150)", nullable: true),
                    Status = table.Column<string>(type: "nvarchar(50)", nullable: true),
                    TaxCategory = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Taxpayers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Taxpayers_Addresses_AddressId",
                        column: x => x.AddressId,
                        principalTable: "Addresses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Taxpayers_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Companies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Taxpayers_AddressId",
                table: "Taxpayers",
                column: "AddressId");

            migrationBuilder.CreateIndex(
                name: "IX_Taxpayers_CompanyId",
                table: "Taxpayers",
                column: "CompanyId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Taxpayers");
        }
    }
}
