using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    public partial class Addagainstarttimeandendtimeandateseparately : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "UserEvents",
                keyColumn: "Id",
                keyValue: new Guid("6646226a-2501-4fd0-83f6-7417d404de9b"));

            migrationBuilder.RenameColumn(
                name: "StartDateTime",
                table: "UserEvents",
                newName: "StartTime");

            migrationBuilder.RenameColumn(
                name: "EndDateTime",
                table: "UserEvents",
                newName: "LastDate");

            migrationBuilder.AddColumn<DateTime>(
                name: "Date",
                table: "UserEvents",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "EndTime",
                table: "UserEvents",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.InsertData(
                table: "UserEvents",
                columns: new[] { "Id", "AdditionalInfo", "Category", "Date", "Description", "EndTime", "ImageUrl", "LastDate", "Name", "Place", "Recurrency", "StartTime" },
                values: new object[] { new Guid("4d4c5425-48f7-40f3-b98f-15d78c22c622"), "test additionalInfo from seed", "test category from seed", new DateTime(2023, 1, 19, 0, 40, 30, 215, DateTimeKind.Utc).AddTicks(7291), "test description from seed", new DateTime(2023, 1, 19, 2, 40, 30, 215, DateTimeKind.Utc).AddTicks(7267), "test image url from seed", new DateTime(2023, 1, 19, 0, 40, 30, 215, DateTimeKind.Utc).AddTicks(7291), "Test name from seed", "test place from seed", "Weekly", new DateTime(2023, 1, 19, 0, 40, 30, 215, DateTimeKind.Utc).AddTicks(7265) });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "UserEvents",
                keyColumn: "Id",
                keyValue: new Guid("4d4c5425-48f7-40f3-b98f-15d78c22c622"));

            migrationBuilder.DropColumn(
                name: "Date",
                table: "UserEvents");

            migrationBuilder.DropColumn(
                name: "EndTime",
                table: "UserEvents");

            migrationBuilder.RenameColumn(
                name: "StartTime",
                table: "UserEvents",
                newName: "StartDateTime");

            migrationBuilder.RenameColumn(
                name: "LastDate",
                table: "UserEvents",
                newName: "EndDateTime");

            migrationBuilder.InsertData(
                table: "UserEvents",
                columns: new[] { "Id", "AdditionalInfo", "Category", "Description", "EndDateTime", "ImageUrl", "Name", "Place", "Recurrency", "StartDateTime" },
                values: new object[] { new Guid("6646226a-2501-4fd0-83f6-7417d404de9b"), "test additionalInfo from seed", "test category from seed", "test description from seed", new DateTime(2023, 1, 19, 1, 55, 25, 27, DateTimeKind.Utc).AddTicks(5855), "test image url from seed", "Test name from seed", "test place from seed", "Weekly", new DateTime(2023, 1, 18, 23, 55, 25, 27, DateTimeKind.Utc).AddTicks(5853) });
        }
    }
}
