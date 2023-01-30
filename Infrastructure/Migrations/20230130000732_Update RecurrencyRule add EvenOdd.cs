using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    public partial class UpdateRecurrencyRuleaddEvenOdd : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "UserEvents",
                keyColumn: "Id",
                keyValue: new Guid("14881e12-e2f5-45f1-992b-698525eb628c"));

            migrationBuilder.AddColumn<int>(
                name: "EvenOdd",
                table: "RecurrencyRules",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.InsertData(
                table: "UserEvents",
                columns: new[] { "Id", "AdditionalInfo", "Category", "Date", "Description", "EndTime", "HasRecurrency", "ImageUrl", "LastDate", "Name", "Place", "StartTime" },
                values: new object[] { new Guid("906807a9-edc4-4abc-974d-1a30c6e2ea70"), "test additionalInfo from seed", "test category from seed", new DateTime(2023, 1, 30, 0, 7, 31, 772, DateTimeKind.Utc).AddTicks(1037), "test description from seed", new DateTime(2023, 1, 30, 2, 7, 31, 772, DateTimeKind.Utc).AddTicks(1014), "Yes", "test image url from seed", new DateTime(2023, 1, 30, 0, 7, 31, 772, DateTimeKind.Utc).AddTicks(1038), "Test name from seed", "test place from seed", new DateTime(2023, 1, 30, 0, 7, 31, 772, DateTimeKind.Utc).AddTicks(1011) });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "UserEvents",
                keyColumn: "Id",
                keyValue: new Guid("906807a9-edc4-4abc-974d-1a30c6e2ea70"));

            migrationBuilder.DropColumn(
                name: "EvenOdd",
                table: "RecurrencyRules");

            migrationBuilder.InsertData(
                table: "UserEvents",
                columns: new[] { "Id", "AdditionalInfo", "Category", "Date", "Description", "EndTime", "HasRecurrency", "ImageUrl", "LastDate", "Name", "Place", "StartTime" },
                values: new object[] { new Guid("14881e12-e2f5-45f1-992b-698525eb628c"), "test additionalInfo from seed", "test category from seed", new DateTime(2023, 1, 29, 22, 13, 7, 572, DateTimeKind.Utc).AddTicks(1143), "test description from seed", new DateTime(2023, 1, 30, 0, 13, 7, 572, DateTimeKind.Utc).AddTicks(1113), "Yes", "test image url from seed", new DateTime(2023, 1, 29, 22, 13, 7, 572, DateTimeKind.Utc).AddTicks(1143), "Test name from seed", "test place from seed", new DateTime(2023, 1, 29, 22, 13, 7, 572, DateTimeKind.Utc).AddTicks(1109) });
        }
    }
}
