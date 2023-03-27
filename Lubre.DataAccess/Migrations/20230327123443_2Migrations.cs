using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Lubre.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class _2Migrations : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "EmployeeId",
                table: "Genders",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Genders_EmployeeId",
                table: "Genders",
                column: "EmployeeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Genders_Employees_EmployeeId",
                table: "Genders",
                column: "EmployeeId",
                principalTable: "Employees",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Genders_Employees_EmployeeId",
                table: "Genders");

            migrationBuilder.DropIndex(
                name: "IX_Genders_EmployeeId",
                table: "Genders");

            migrationBuilder.DropColumn(
                name: "EmployeeId",
                table: "Genders");
        }
    }
}
