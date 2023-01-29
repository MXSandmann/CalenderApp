using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    public partial class AddnewpropertiestoRecurrencyRule : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "UserEvents",
                keyColumn: "Id",
                keyValue: new Guid("da58013a-c5ea-4dd5-a76a-f8d69abf00c9"));

            migrationBuilder.AddColumn<bool>(
                name: "OnFriday",
                table: "RecurrencyRules",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "OnMonday",
                table: "RecurrencyRules",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "OnSaturday",
                table: "RecurrencyRules",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "OnSunday",
                table: "RecurrencyRules",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "OnThursday",
                table: "RecurrencyRules",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "OnTuesday",
                table: "RecurrencyRules",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "OnWednesday",
                table: "RecurrencyRules",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.InsertData(
                table: "UserEvents",
                columns: new[] { "Id", "AdditionalInfo", "Category", "Date", "Description", "EndTime", "HasRecurrency", "ImageUrl", "LastDate", "Name", "Place", "StartTime" },
                values: new object[] { new Guid("fcdb43b6-9411-48c9-9844-a407201c07ed"), "test additionalInfo from seed", "test category from seed", new DateTime(2023, 1, 29, 1, 49, 25, 719, DateTimeKind.Utc).AddTicks(4994), "test description from seed", new DateTime(2023, 1, 29, 3, 49, 25, 719, DateTimeKind.Utc).AddTicks(4957), "Yes", "test image url from seed", new DateTime(2023, 1, 29, 1, 49, 25, 719, DateTimeKind.Utc).AddTicks(4994), "Test name from seed", "test place from seed", new DateTime(2023, 1, 29, 1, 49, 25, 719, DateTimeKind.Utc).AddTicks(4954) });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "UserEvents",
                keyColumn: "Id",
                keyValue: new Guid("fcdb43b6-9411-48c9-9844-a407201c07ed"));

            migrationBuilder.DropColumn(
                name: "OnFriday",
                table: "RecurrencyRules");

            migrationBuilder.DropColumn(
                name: "OnMonday",
                table: "RecurrencyRules");

            migrationBuilder.DropColumn(
                name: "OnSaturday",
                table: "RecurrencyRules");

            migrationBuilder.DropColumn(
                name: "OnSunday",
                table: "RecurrencyRules");

            migrationBuilder.DropColumn(
                name: "OnThursday",
                table: "RecurrencyRules");

            migrationBuilder.DropColumn(
                name: "OnTuesday",
                table: "RecurrencyRules");

            migrationBuilder.DropColumn(
                name: "OnWednesday",
                table: "RecurrencyRules");

            migrationBuilder.InsertData(
                table: "UserEvents",
                columns: new[] { "Id", "AdditionalInfo", "Category", "Date", "Description", "EndTime", "HasRecurrency", "ImageUrl", "LastDate", "Name", "Place", "StartTime" },
                values: new object[] { new Guid("da58013a-c5ea-4dd5-a76a-f8d69abf00c9"), "test additionalInfo from seed", "test category from seed", new DateTime(2023, 1, 28, 12, 38, 49, 286, DateTimeKind.Utc).AddTicks(6641), "test description from seed", new DateTime(2023, 1, 28, 14, 38, 49, 286, DateTimeKind.Utc).AddTicks(6612), "Yes", "test image url from seed", new DateTime(2023, 1, 28, 12, 38, 49, 286, DateTimeKind.Utc).AddTicks(6641), "Test name from seed", "test place from seed", new DateTime(2023, 1, 28, 12, 38, 49, 286, DateTimeKind.Utc).AddTicks(6608) });
        }
    }
}
