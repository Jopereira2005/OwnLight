using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AutomationService.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class FixDeviceIds : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "DeviceIds",
                table: "Groups",
                type: "text",
                nullable: true,
                oldClrType: typeof(Guid[]),
                oldType: "uuid[]");

            migrationBuilder.AddColumn<Guid[]>(
                name: "DeviceIdsList",
                table: "Groups",
                type: "uuid[]",
                nullable: false,
                defaultValue: new Guid[0]);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DeviceIdsList",
                table: "Groups");

            migrationBuilder.AlterColumn<Guid[]>(
                name: "DeviceIds",
                table: "Groups",
                type: "uuid[]",
                nullable: false,
                defaultValue: new Guid[0],
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);
        }
    }
}
