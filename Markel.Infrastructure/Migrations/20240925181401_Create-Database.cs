using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Markel.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class CreateDatabase : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ClaimTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClaimTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Companies",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", maxLength: 200, nullable: false),
                    Address1 = table.Column<string>(type: "TEXT", maxLength: 100, nullable: true),
                    Address2 = table.Column<string>(type: "TEXT", maxLength: 100, nullable: true),
                    Address3 = table.Column<string>(type: "TEXT", maxLength: 100, nullable: true),
                    PostCode = table.Column<string>(type: "TEXT", maxLength: 20, nullable: true),
                    Country = table.Column<string>(type: "TEXT", maxLength: 50, nullable: true),
                    Active = table.Column<bool>(type: "INTEGER", nullable: false),
                    InsuranceEndDate = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Companies", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Claims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    ClaimTypeId = table.Column<int>(type: "INTEGER", nullable: false),
                    UCR = table.Column<string>(type: "TEXT", maxLength: 20, nullable: false),
                    ClaimDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    LossDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    AssuredName = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    IncurredLoss = table.Column<decimal>(type: "TEXT", nullable: false),
                    Closed = table.Column<bool>(type: "INTEGER", nullable: false),
                    CompanyId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Claims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Claims_ClaimTypes_ClaimTypeId",
                        column: x => x.ClaimTypeId,
                        principalTable: "ClaimTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Claims_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Companies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Claims_ClaimTypeId",
                table: "Claims",
                column: "ClaimTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Claims_CompanyId",
                table: "Claims",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_Claims_UCR",
                table: "Claims",
                column: "UCR",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Claims");

            migrationBuilder.DropTable(
                name: "ClaimTypes");

            migrationBuilder.DropTable(
                name: "Companies");
        }
    }
}
