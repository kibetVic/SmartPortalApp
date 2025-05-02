using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SmartPortalApp.Migrations
{
    /// <inheritdoc />
    public partial class TranferModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Transfers_FromCourseId",
                table: "Transfers",
                column: "FromCourseId");

            migrationBuilder.CreateIndex(
                name: "IX_Transfers_ToCourseId",
                table: "Transfers",
                column: "ToCourseId");

            migrationBuilder.AddForeignKey(
                name: "FK_Transfers_Courses_FromCourseId",
                table: "Transfers",
                column: "FromCourseId",
                principalTable: "Courses",
                principalColumn: "CourseId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Transfers_Courses_ToCourseId",
                table: "Transfers",
                column: "ToCourseId",
                principalTable: "Courses",
                principalColumn: "CourseId",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Transfers_Courses_FromCourseId",
                table: "Transfers");

            migrationBuilder.DropForeignKey(
                name: "FK_Transfers_Courses_ToCourseId",
                table: "Transfers");

            migrationBuilder.DropIndex(
                name: "IX_Transfers_FromCourseId",
                table: "Transfers");

            migrationBuilder.DropIndex(
                name: "IX_Transfers_ToCourseId",
                table: "Transfers");
        }
    }
}
