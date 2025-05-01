using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SmartPortalApp.Migrations
{
    /// <inheritdoc />
    public partial class Tranfer : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Transfers_StudentId",
                table: "Transfers",
                column: "StudentId");

            migrationBuilder.AddForeignKey(
                name: "FK_Transfers_Students_StudentId",
                table: "Transfers",
                column: "StudentId",
                principalTable: "Students",
                principalColumn: "StudentId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Transfers_Students_StudentId",
                table: "Transfers");

            migrationBuilder.DropIndex(
                name: "IX_Transfers_StudentId",
                table: "Transfers");
        }
    }
}
