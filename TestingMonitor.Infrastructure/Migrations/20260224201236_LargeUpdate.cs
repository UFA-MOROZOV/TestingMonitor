using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TestingMonitor.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class LargeUpdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "HFiles",
                table: "Tests");

            migrationBuilder.AddColumn<string>(
                name: "ImageName",
                table: "Compilers",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "CompilerTasks",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    CompilerId = table.Column<int>(type: "integer", nullable: false),
                    DateOfCreation = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    DateOfStart = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    DateOfCompletion = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    TestId = table.Column<Guid>(type: "uuid", nullable: true),
                    TestGroupId = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CompilerTasks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CompilerTasks_Compilers_CompilerId",
                        column: x => x.CompilerId,
                        principalTable: "Compilers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CompilerTasks_TestGroups_TestGroupId",
                        column: x => x.TestGroupId,
                        principalTable: "TestGroups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CompilerTasks_Tests_TestId",
                        column: x => x.TestId,
                        principalTable: "Tests",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "HeaderFiles",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Path = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HeaderFiles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TestExecutions",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    CompilerTaskId = table.Column<Guid>(type: "uuid", nullable: false),
                    TestId = table.Column<Guid>(type: "uuid", nullable: true),
                    Duration = table.Column<TimeSpan>(type: "interval", nullable: false),
                    IsSuccessful = table.Column<bool>(type: "boolean", nullable: false),
                    ErrorMessage = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TestExecutions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TestExecutions_CompilerTasks_CompilerTaskId",
                        column: x => x.CompilerTaskId,
                        principalTable: "CompilerTasks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TestExecutions_Tests_TestId",
                        column: x => x.TestId,
                        principalTable: "Tests",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TestGroupToHeaderFile",
                columns: table => new
                {
                    HeaderId = table.Column<Guid>(type: "uuid", nullable: false),
                    TestGroupId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TestGroupToHeaderFile", x => new { x.HeaderId, x.TestGroupId });
                    table.ForeignKey(
                        name: "FK_TestGroupToHeaderFile_HeaderFiles_HeaderId",
                        column: x => x.HeaderId,
                        principalTable: "HeaderFiles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TestGroupToHeaderFile_TestGroups_TestGroupId",
                        column: x => x.TestGroupId,
                        principalTable: "TestGroups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
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
                name: "IX_CompilerTasks_CompilerId",
                table: "CompilerTasks",
                column: "CompilerId");

            migrationBuilder.CreateIndex(
                name: "IX_CompilerTasks_TestGroupId",
                table: "CompilerTasks",
                column: "TestGroupId");

            migrationBuilder.CreateIndex(
                name: "IX_CompilerTasks_TestId",
                table: "CompilerTasks",
                column: "TestId");

            migrationBuilder.CreateIndex(
                name: "IX_HeaderFiles_Name",
                table: "HeaderFiles",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_TestExecutions_CompilerTaskId",
                table: "TestExecutions",
                column: "CompilerTaskId");

            migrationBuilder.CreateIndex(
                name: "IX_TestExecutions_TestId",
                table: "TestExecutions",
                column: "TestId");

            migrationBuilder.CreateIndex(
                name: "IX_TestGroupToHeaderFile_TestGroupId",
                table: "TestGroupToHeaderFile",
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
                name: "TestExecutions");

            migrationBuilder.DropTable(
                name: "TestGroupToHeaderFile");

            migrationBuilder.DropTable(
                name: "TestToHeaderFiles");

            migrationBuilder.DropTable(
                name: "CompilerTasks");

            migrationBuilder.DropTable(
                name: "HeaderFiles");

            migrationBuilder.DropColumn(
                name: "ImageName",
                table: "Compilers");

            migrationBuilder.AddColumn<string[]>(
                name: "HFiles",
                table: "Tests",
                type: "text[]",
                nullable: false,
                defaultValue: new string[0]);
        }
    }
}
