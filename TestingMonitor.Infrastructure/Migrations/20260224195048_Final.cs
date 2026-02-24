using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TestingMonitor.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Final : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TestExecutions_ExecutionTasks_ExecutionTaskId",
                table: "TestExecutions");

            migrationBuilder.DropTable(
                name: "ExecutionTasks");

            migrationBuilder.CreateTable(
                name: "CompilerTasks",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
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

            migrationBuilder.AddForeignKey(
                name: "FK_TestExecutions_CompilerTasks_ExecutionTaskId",
                table: "TestExecutions",
                column: "ExecutionTaskId",
                principalTable: "CompilerTasks",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TestExecutions_CompilerTasks_ExecutionTaskId",
                table: "TestExecutions");

            migrationBuilder.DropTable(
                name: "CompilerTasks");

            migrationBuilder.CreateTable(
                name: "ExecutionTasks",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    CompilerId = table.Column<int>(type: "integer", nullable: false),
                    TestGroupId = table.Column<Guid>(type: "uuid", nullable: true),
                    TestId = table.Column<Guid>(type: "uuid", nullable: true),
                    DateOfCreation = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    DateOfEnd = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    DateOfStart = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
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

            migrationBuilder.AddForeignKey(
                name: "FK_TestExecutions_ExecutionTasks_ExecutionTaskId",
                table: "TestExecutions",
                column: "ExecutionTaskId",
                principalTable: "ExecutionTasks",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
