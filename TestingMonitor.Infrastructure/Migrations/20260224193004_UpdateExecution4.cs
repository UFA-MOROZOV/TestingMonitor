using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TestingMonitor.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UpdateExecution4 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "TimeInSeconds",
                table: "TestExecutions",
                newName: "DurationInSeconds");

            migrationBuilder.CreateIndex(
                name: "IX_HeaderFiles_Name",
                table: "HeaderFiles",
                column: "Name",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_HeaderFiles_Name",
                table: "HeaderFiles");

            migrationBuilder.RenameColumn(
                name: "DurationInSeconds",
                table: "TestExecutions",
                newName: "TimeInSeconds");
        }
    }
}
