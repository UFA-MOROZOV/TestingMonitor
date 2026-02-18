using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TestingMonitor.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddHeaders : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "HFiles",
                table: "Tests");

            migrationBuilder.CreateTable(
                name: "HeaderFiles",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Path = table.Column<string>(type: "text", nullable: false),
                    TestGroupId = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HeaderFiles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_HeaderFiles_TestGroups_TestGroupId",
                        column: x => x.TestGroupId,
                        principalTable: "TestGroups",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "TestToHeaderFiles",
                columns: table => new
                {
                    HeaderId = table.Column<Guid>(type: "uuid", nullable: false),
                    TestId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TestToHeaderFiles", x => new { x.HeaderId, x.TestId });
                    table.ForeignKey(
                        name: "FK_TestToHeaderFiles_HeaderFiles_HeaderId",
                        column: x => x.HeaderId,
                        principalTable: "HeaderFiles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TestToHeaderFiles_Tests_TestId",
                        column: x => x.TestId,
                        principalTable: "Tests",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_HeaderFiles_TestGroupId",
                table: "HeaderFiles",
                column: "TestGroupId");

            migrationBuilder.CreateIndex(
                name: "IX_TestToHeaderFiles_TestId",
                table: "TestToHeaderFiles",
                column: "TestId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TestToHeaderFiles");

            migrationBuilder.DropTable(
                name: "HeaderFiles");

            migrationBuilder.AddColumn<string[]>(
                name: "HFiles",
                table: "Tests",
                type: "text[]",
                nullable: false,
                defaultValue: new string[0]);
        }
    }
}
