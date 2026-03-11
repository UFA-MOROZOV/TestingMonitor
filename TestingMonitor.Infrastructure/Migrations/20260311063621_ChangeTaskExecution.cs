using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TestingMonitor.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class ChangeTaskExecution : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Duration",
                table: "TestExecutions");

            migrationBuilder.DropColumn(
                name: "IsSuccessful",
                table: "TestExecutions");

            migrationBuilder.DropColumn(
                name: "Output",
                table: "TestExecutions");

            migrationBuilder.AddColumn<bool>(
                name: "CompilationSucceeded",
                table: "TestExecutions",
                type: "boolean",
                nullable: true);

            migrationBuilder.AddColumn<TimeSpan>(
                name: "CompileDuration",
                table: "TestExecutions",
                type: "interval",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CompilerExitCode",
                table: "TestExecutions",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CompilerOutput",
                table: "TestExecutions",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "ProgramExitCode",
                table: "TestExecutions",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ProgramOutput",
                table: "TestExecutions",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<TimeSpan>(
                name: "RunDuration",
                table: "TestExecutions",
                type: "interval",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CompilationSucceeded",
                table: "TestExecutions");

            migrationBuilder.DropColumn(
                name: "CompileDuration",
                table: "TestExecutions");

            migrationBuilder.DropColumn(
                name: "CompilerExitCode",
                table: "TestExecutions");

            migrationBuilder.DropColumn(
                name: "CompilerOutput",
                table: "TestExecutions");

            migrationBuilder.DropColumn(
                name: "ProgramExitCode",
                table: "TestExecutions");

            migrationBuilder.DropColumn(
                name: "ProgramOutput",
                table: "TestExecutions");

            migrationBuilder.DropColumn(
                name: "RunDuration",
                table: "TestExecutions");

            migrationBuilder.AddColumn<TimeSpan>(
                name: "Duration",
                table: "TestExecutions",
                type: "interval",
                nullable: false,
                defaultValue: new TimeSpan(0, 0, 0, 0, 0));

            migrationBuilder.AddColumn<bool>(
                name: "IsSuccessful",
                table: "TestExecutions",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "Output",
                table: "TestExecutions",
                type: "text",
                nullable: true);
        }
    }
}
