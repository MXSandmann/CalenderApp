using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    public partial class AddStartDateTimeandEndDateTimeandRecurrency : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "UserEvents",
                keyColumn: "Id",
                keyValue: new Guid("8f45561f-68c7-4868-864e-806ea40b6427"));

            migrationBuilder.RenameColumn(
                name: "Time",
                table: "UserEvents",
                newName: "StartDateTime");

            migrationBuilder.AddColumn<DateTime>(
                name: "EndDateTime",
                table: "UserEvents",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "Recurrency",
                table: "UserEvents",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.InsertData(
                table: "UserEvents",
                columns: new[] { "Id", "AdditionalInfo", "Category", "Description", "EndDateTime", "ImageUrl", "Name", "Place", "Recurrency", "StartDateTime" },
                values: new object[] { new Guid("bc81b825-7a3e-41fa-bf92-ac2c8c5d9481"), "test additionalInfo from seed", "test category from seed", "test description from seed", new DateTime(2023, 1, 18, 2, 39, 21, 729, DateTimeKind.Utc).AddTicks(9932), "test image url from seed", "Test name from seed", "test place from seed", "Weekly", new DateTime(2023, 1, 18, 0, 39, 21, 729, DateTimeKind.Utc).AddTicks(9928) });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "UserEvents",
                keyColumn: "Id",
                keyValue: new Guid("bc81b825-7a3e-41fa-bf92-ac2c8c5d9481"));

            migrationBuilder.DropColumn(
                name: "EndDateTime",
                table: "UserEvents");

            migrationBuilder.DropColumn(
                name: "Recurrency",
                table: "UserEvents");

            migrationBuilder.RenameColumn(
                name: "StartDateTime",
                table: "UserEvents",
                newName: "Time");

            migrationBuilder.InsertData(
                table: "UserEvents",
                columns: new[] { "Id", "AdditionalInfo", "Category", "Description", "ImageUrl", "Name", "Place", "Time" },
                values: new object[] { new Guid("8f45561f-68c7-4868-864e-806ea40b6427"), "test additionalInfo from seed", "test category from seed", "test description from seed", "test image url from seed", "Test name from seed", "test place from seed", new DateTime(2023, 1, 18, 0, 18, 47, 550, DateTimeKind.Utc).AddTicks(731) });
        }
    }
}
