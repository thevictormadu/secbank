using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SecBank.Migrations
{
    /// <inheritdoc />
    public partial class addedcreatedatdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CustomerId",
                table: "Transactions");

            migrationBuilder.AddColumn<DateTime>(
                name: "DateCreated",
                table: "Transactions",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DateCreated",
                table: "Transactions");

            migrationBuilder.AddColumn<int>(
                name: "CustomerId",
                table: "Transactions",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
