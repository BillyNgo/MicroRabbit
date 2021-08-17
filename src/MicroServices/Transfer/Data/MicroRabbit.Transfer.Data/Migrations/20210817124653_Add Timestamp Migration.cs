using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MicroRabbit.Transfer.Data.Migrations
{
    public partial class AddTimestampMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "dbo");

            migrationBuilder.RenameTable(
                name: "TransferLogs",
                newName: "TransferLogs",
                newSchema: "dbo");

            migrationBuilder.AddColumn<DateTime>(
                name: "TimeStamps",
                schema: "dbo",
                table: "TransferLogs",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TimeStamps",
                schema: "dbo",
                table: "TransferLogs");

            migrationBuilder.RenameTable(
                name: "TransferLogs",
                schema: "dbo",
                newName: "TransferLogs");
        }
    }
}
