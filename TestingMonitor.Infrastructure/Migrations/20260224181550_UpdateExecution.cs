using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TestingMonitor.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UpdateExecution : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_HeaderFiles_TestGroups_TestGroupId",
                table: "HeaderFiles");

            migrationBuilder.DropIndex(
                name: "IX_HeaderFiles_TestGroupId",
                table: "HeaderFiles");

            migrationBuilder.DropColumn(
                name: "TestGroupId",
                table: "HeaderFiles");

            migrationBuilder.CreateTable(
                name: "ExecutionTasks",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    CompilerId = table.Column<int>(type: "integer", nullable: false),
                    DateOfCreation = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    DateOfStart = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    DateOfEnd = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    TestId = table.Column<Guid>(type: "uuid", nullable: true),
                    TestGroupId = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExecutionTasks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ExecutionTasks_Compilers_CompilerId",
                        column: x => x.CompilerId,
                        principalTable: "Compilers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ExecutionTasks_TestGroups_TestGroupId",
                        column: x => x.TestGroupId,
                        principalTable: "TestGroups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ExecutionTasks_Tests_TestId",
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
                name: "TestExecution",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    ExecutionTaskId = table.Column<Guid>(type: "uuid", nullable: false),
                    TestId = table.Column<Guid>(type: "uuid", nullable: true),
                    TimeInSeconds = table.Column<int>(type: "integer", nullable: false),
                    IsSuccessful = table.Column<bool>(type: "boolean", nullable: false),
                    ErrorMessage = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TestExecution", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TestExecution_ExecutionTasks_ExecutionTaskId",
                        column: x => x.ExecutionTaskId,
                        principalTable: "ExecutionTasks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TestExecution_Tests_TestId",
                        column: x => x.TestId,
                        principalTable: "Tests",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ExecutionTasks_CompilerId",
                table: "ExecutionTasks",
                column: "CompilerId");

            migrationBuilder.CreateIndex(
                name: "IX_ExecutionTasks_TestGroupId",
                table: "ExecutionTasks",
                column: "TestGroupId");

            migrationBuilder.CreateIndex(
                name: "IX_ExecutionTasks_TestId",
                table: "ExecutionTasks",
                column: "TestId");

            migrationBuilder.CreateIndex(
                name: "IX_TestExecution_ExecutionTaskId",
                table: "TestExecution",
                column: "ExecutionTaskId");

            migrationBuilder.CreateIndex(
                name: "IX_TestExecution_TestId",
                table: "TestExecution",
                column: "TestId");

            migrationBuilder.CreateIndex(
                name: "IX_TestGroupToHeaderFile_TestGroupId",
                table: "TestGroupToHeaderFile",
                column: "TestGroupId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TestExecution");

            migrationBuilder.DropTable(
                name: "TestGroupToHeaderFile");

            migrationBuilder.DropTable(
                name: "ExecutionTasks");

            migrationBuilder.AddColumn<Guid>(
                name: "TestGroupId",
                table: "HeaderFiles",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_HeaderFiles_TestGroupId",
                table: "HeaderFiles",
                column: "TestGroupId");

            migrationBuilder.AddForeignKey(
                name: "FK_HeaderFiles_TestGroups_TestGroupId",
                table: "HeaderFiles",
                column: "TestGroupId",
                principalTable: "TestGroups",
                principalColumn: "Id");
        }
    }
}
