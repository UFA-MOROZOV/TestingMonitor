using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TestingMonitor.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddHasDockerLocally : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "HasDockerLocally",
                table: "Compilers",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "HasDockerLocally",
                table: "Compilers");
        }
    }
}
