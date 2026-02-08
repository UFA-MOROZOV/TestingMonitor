using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TestingMonitor.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Fix : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TestGroups_TestGroups_TestGroupId",
                table: "TestGroups");

            migrationBuilder.DropForeignKey(
                name: "FK_Tests_TestGroups_TestGroupId",
                table: "Tests");

            migrationBuilder.DropIndex(
                name: "IX_TestGroups_TestGroupId",
                table: "TestGroups");

            migrationBuilder.DropColumn(
                name: "TestGroupId",
                table: "TestGroups");

            migrationBuilder.CreateIndex(
                name: "IX_TestGroups_ParentGroupId",
                table: "TestGroups",
                column: "ParentGroupId");

            migrationBuilder.AddForeignKey(
                name: "FK_TestGroups_TestGroups_ParentGroupId",
                table: "TestGroups",
                column: "ParentGroupId",
                principalTable: "TestGroups",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Tests_TestGroups_TestGroupId",
                table: "Tests",
                column: "TestGroupId",
                principalTable: "TestGroups",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TestGroups_TestGroups_ParentGroupId",
                table: "TestGroups");

            migrationBuilder.DropForeignKey(
                name: "FK_Tests_TestGroups_TestGroupId",
                table: "Tests");

            migrationBuilder.DropIndex(
                name: "IX_TestGroups_ParentGroupId",
                table: "TestGroups");

            migrationBuilder.AddColumn<Guid>(
                name: "TestGroupId",
                table: "TestGroups",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_TestGroups_TestGroupId",
                table: "TestGroups",
                column: "TestGroupId");

            migrationBuilder.AddForeignKey(
                name: "FK_TestGroups_TestGroups_TestGroupId",
                table: "TestGroups",
                column: "TestGroupId",
                principalTable: "TestGroups",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Tests_TestGroups_TestGroupId",
                table: "Tests",
                column: "TestGroupId",
                principalTable: "TestGroups",
                principalColumn: "Id");
        }
    }
}
