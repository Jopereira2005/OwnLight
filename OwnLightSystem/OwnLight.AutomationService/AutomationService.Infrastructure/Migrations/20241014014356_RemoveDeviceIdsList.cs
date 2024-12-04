using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AutomationService.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class RemoveDeviceIdsList : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DeviceIdsList",
                table: "Groups");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid[]>(
                name: "DeviceIdsList",
                table: "Groups",
                type: "uuid[]",
                nullable: false,
                defaultValue: new Guid[0]);
        }
    }
}
