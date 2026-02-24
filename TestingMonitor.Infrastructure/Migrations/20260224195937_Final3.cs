using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TestingMonitor.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Final3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DurationInSeconds",
                table: "TestExecutions");

            migrationBuilder.AddColumn<TimeSpan>(
                name: "Duration",
                table: "TestExecutions",
                type: "interval",
                nullable: false,
                defaultValue: new TimeSpan(0, 0, 0, 0, 0));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Duration",
                table: "TestExecutions");

            migrationBuilder.AddColumn<int>(
                name: "DurationInSeconds",
                table: "TestExecutions",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }
    }
}
