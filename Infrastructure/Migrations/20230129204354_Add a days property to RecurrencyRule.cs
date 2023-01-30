using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    public partial class AddadayspropertytoRecurrencyRule : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.AddColumn<byte>(
                name: "CertainDays",
                table: "RecurrencyRules",
                type: "smallint",
                nullable: false,
                defaultValue: (byte)0);

            migrationBuilder.InsertData(
                table: "UserEvents",
                columns: new[] { "Id", "AdditionalInfo", "Category", "Date", "Description", "EndTime", "HasRecurrency", "ImageUrl", "LastDate", "Name", "Place", "StartTime" },
                values: new object[] { new Guid("e3cbb56c-3137-436c-a73f-dea568a16183"), "test additionalInfo from seed", "test category from seed", new DateTime(2023, 1, 29, 20, 43, 54, 539, DateTimeKind.Utc).AddTicks(4064), "test description from seed", new DateTime(2023, 1, 29, 22, 43, 54, 539, DateTimeKind.Utc).AddTicks(4038), "Yes", "test image url from seed", new DateTime(2023, 1, 29, 20, 43, 54, 539, DateTimeKind.Utc).AddTicks(4065), "Test name from seed", "test place from seed", new DateTime(2023, 1, 29, 20, 43, 54, 539, DateTimeKind.Utc).AddTicks(4037) });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "UserEvents",
                keyColumn: "Id",
                keyValue: new Guid("e3cbb56c-3137-436c-a73f-dea568a16183"));

            migrationBuilder.DropColumn(
                name: "CertainDays",
                table: "RecurrencyRules");

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
    }
}
