using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AutomationService.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class RefactRoutine : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int[]>(
                name: "DaysOfWeek",
                table: "Routines",
                type: "integer[]",
                nullable: false,
                defaultValue: new int[0]);

            migrationBuilder.AddColumn<bool>(
                name: "IsOneTime",
                table: "Routines",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DaysOfWeek",
                table: "Routines");

            migrationBuilder.DropColumn(
                name: "IsOneTime",
                table: "Routines");
        }
    }
}
