using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Lubre.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class updatefirstMigrations : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cities_Towns_StateId",
                table: "Cities");

            migrationBuilder.RenameColumn(
                name: "StateId",
                table: "Cities",
                newName: "TownId");

            migrationBuilder.RenameIndex(
                name: "IX_Cities_StateId",
                table: "Cities",
                newName: "IX_Cities_TownId");

            migrationBuilder.AddForeignKey(
                name: "FK_Cities_Towns_TownId",
                table: "Cities",
                column: "TownId",
                principalTable: "Towns",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cities_Towns_TownId",
                table: "Cities");

            migrationBuilder.RenameColumn(
                name: "TownId",
                table: "Cities",
                newName: "StateId");

            migrationBuilder.RenameIndex(
                name: "IX_Cities_TownId",
                table: "Cities",
                newName: "IX_Cities_StateId");

            migrationBuilder.AddForeignKey(
                name: "FK_Cities_Towns_StateId",
                table: "Cities",
                column: "StateId",
                principalTable: "Towns",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
