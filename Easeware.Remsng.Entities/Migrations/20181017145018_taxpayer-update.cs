using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Easeware.Remsng.Entities.Migrations
{
    public partial class taxpayerupdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Status",
                table: "Wards",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Companies",
                columns: table => new
                {
                    CreatedDate = table.Column<DateTimeOffset>(nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(100)", nullable: true),
                    ModifiedDate = table.Column<DateTimeOffset>(nullable: true),
                    ModifiedBy = table.Column<string>(type: "nvarchar(100)", nullable: true),
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CompanyName = table.Column<string>(type: "nvarchar(150)", nullable: true),
                    CompanyCode = table.Column<string>(type: "nvarchar(20)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Companies", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Sectors",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    SectorCode = table.Column<string>(type: "nvarchar(20)", nullable: true),
                    SectorName = table.Column<string>(type: "nvarchar(100)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sectors", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Streets",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    StreetName = table.Column<string>(type: "nvarchar(150)", nullable: true),
                    StreetStatus = table.Column<string>(type: "nvarchar(20)", nullable: true),
                    WardId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Streets", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Streets_Wards_WardId",
                        column: x => x.WardId,
                        principalTable: "Wards",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Taxpayers",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CompanyId = table.Column<long>(nullable: false),
                    AddressId = table.Column<long>(nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(150)", nullable: true),
                    OtherNames = table.Column<string>(type: "nvarchar(150)", nullable: true),
                    Status = table.Column<string>(type: "nvarchar(50)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Taxpayers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Addresses",
                columns: table => new
                {
                    CreatedDate = table.Column<DateTimeOffset>(nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(100)", nullable: true),
                    ModifiedDate = table.Column<DateTimeOffset>(nullable: true),
                    ModifiedBy = table.Column<string>(type: "nvarchar(100)", nullable: true),
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    HouseNumber = table.Column<string>(type: "nvarchar(10)", nullable: true),
                    StreetId = table.Column<long>(nullable: false),
                    OwnerId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Addresses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Addresses_Streets_StreetId",
                        column: x => x.StreetId,
                        principalTable: "Streets",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Addresses_HouseNumber",
                table: "Addresses",
                column: "HouseNumber");

            migrationBuilder.CreateIndex(
                name: "IX_Addresses_OwnerId",
                table: "Addresses",
                column: "OwnerId");

            migrationBuilder.CreateIndex(
                name: "IX_Addresses_StreetId",
                table: "Addresses",
                column: "StreetId");

            migrationBuilder.CreateIndex(
                name: "IX_Companies_CompanyCode",
                table: "Companies",
                column: "CompanyCode",
                unique: true,
                filter: "[CompanyCode] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Sectors_SectorCode",
                table: "Sectors",
                column: "SectorCode",
                unique: true,
                filter: "[SectorCode] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Streets_WardId",
                table: "Streets",
                column: "WardId");

            migrationBuilder.CreateIndex(
                name: "IX_Taxpayers_AddressId",
                table: "Taxpayers",
                column: "AddressId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Taxpayers_CompanyId",
                table: "Taxpayers",
                column: "CompanyId",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Addresses");

            migrationBuilder.DropTable(
                name: "Companies");

            migrationBuilder.DropTable(
                name: "Sectors");

            migrationBuilder.DropTable(
                name: "Taxpayers");

            migrationBuilder.DropTable(
                name: "Streets");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "Wards");
        }
    }
}
