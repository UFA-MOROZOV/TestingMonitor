using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TestingMonitor.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UpdateExecution2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TestExecution_ExecutionTasks_ExecutionTaskId",
                table: "TestExecution");

            migrationBuilder.DropForeignKey(
                name: "FK_TestExecution_Tests_TestId",
                table: "TestExecution");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TestExecution",
                table: "TestExecution");

            migrationBuilder.RenameTable(
                name: "TestExecution",
                newName: "TestExecutions");

            migrationBuilder.RenameColumn(
                name: "ErrorMessage",
                table: "TestExecutions",
                newName: "Output");

            migrationBuilder.RenameIndex(
                name: "IX_TestExecution_TestId",
                table: "TestExecutions",
                newName: "IX_TestExecutions_TestId");

            migrationBuilder.RenameIndex(
                name: "IX_TestExecution_ExecutionTaskId",
                table: "TestExecutions",
                newName: "IX_TestExecutions_ExecutionTaskId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TestExecutions",
                table: "TestExecutions",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_TestExecutions_ExecutionTasks_ExecutionTaskId",
                table: "TestExecutions",
                column: "ExecutionTaskId",
                principalTable: "ExecutionTasks",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TestExecutions_Tests_TestId",
                table: "TestExecutions",
                column: "TestId",
                principalTable: "Tests",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TestExecutions_ExecutionTasks_ExecutionTaskId",
                table: "TestExecutions");

            migrationBuilder.DropForeignKey(
                name: "FK_TestExecutions_Tests_TestId",
                table: "TestExecutions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TestExecutions",
                table: "TestExecutions");

            migrationBuilder.RenameTable(
                name: "TestExecutions",
                newName: "TestExecution");

            migrationBuilder.RenameColumn(
                name: "Output",
                table: "TestExecution",
                newName: "ErrorMessage");

            migrationBuilder.RenameIndex(
                name: "IX_TestExecutions_TestId",
                table: "TestExecution",
                newName: "IX_TestExecution_TestId");

            migrationBuilder.RenameIndex(
                name: "IX_TestExecutions_ExecutionTaskId",
                table: "TestExecution",
                newName: "IX_TestExecution_ExecutionTaskId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TestExecution",
                table: "TestExecution",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_TestExecution_ExecutionTasks_ExecutionTaskId",
                table: "TestExecution",
                column: "ExecutionTaskId",
                principalTable: "ExecutionTasks",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TestExecution_Tests_TestId",
                table: "TestExecution",
                column: "TestId",
                principalTable: "Tests",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
