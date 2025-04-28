using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SmartPortalApp.Migrations
{
    /// <inheritdoc />
    public partial class SmartappSystem : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Reason",
                table: "CourseTransfers",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Reason",
                table: "CourseTransfers");
        }
    }
}
