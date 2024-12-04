using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AutomationService.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class FixRooms : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "DeviceIds",
                table: "Rooms",
                type: "text",
                nullable: true,
                oldClrType: typeof(Guid[]),
                oldType: "uuid[]");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<Guid[]>(
                name: "DeviceIds",
                table: "Rooms",
                type: "uuid[]",
                nullable: false,
                defaultValue: new Guid[0],
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);
        }
    }
}
