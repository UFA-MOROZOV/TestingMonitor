using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TestingMonitor.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Final2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TestExecutions_CompilerTasks_ExecutionTaskId",
                table: "TestExecutions");

            migrationBuilder.RenameColumn(
                name: "ExecutionTaskId",
                table: "TestExecutions",
                newName: "CompilerTaskId");

            migrationBuilder.RenameIndex(
                name: "IX_TestExecutions_ExecutionTaskId",
                table: "TestExecutions",
                newName: "IX_TestExecutions_CompilerTaskId");

            migrationBuilder.AddForeignKey(
                name: "FK_TestExecutions_CompilerTasks_CompilerTaskId",
                table: "TestExecutions",
                column: "CompilerTaskId",
                principalTable: "CompilerTasks",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TestExecutions_CompilerTasks_CompilerTaskId",
                table: "TestExecutions");

            migrationBuilder.RenameColumn(
                name: "CompilerTaskId",
                table: "TestExecutions",
                newName: "ExecutionTaskId");

            migrationBuilder.RenameIndex(
                name: "IX_TestExecutions_CompilerTaskId",
                table: "TestExecutions",
                newName: "IX_TestExecutions_ExecutionTaskId");

            migrationBuilder.AddForeignKey(
                name: "FK_TestExecutions_CompilerTasks_ExecutionTaskId",
                table: "TestExecutions",
                column: "ExecutionTaskId",
                principalTable: "CompilerTasks",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
