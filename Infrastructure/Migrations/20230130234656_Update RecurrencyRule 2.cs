using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    public partial class UpdateRecurrencyRule2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "UserEvents",
                keyColumn: "Id",
                keyValue: new Guid("906807a9-edc4-4abc-974d-1a30c6e2ea70"));

            migrationBuilder.DropColumn(
                name: "MonthOfYear",
                table: "RecurrencyRules");

            migrationBuilder.InsertData(
                table: "UserEvents",
                columns: new[] { "Id", "AdditionalInfo", "Category", "Date", "Description", "EndTime", "HasRecurrency", "ImageUrl", "LastDate", "Name", "Place", "StartTime" },
                values: new object[] { new Guid("12ba54ed-980f-41f4-a296-b74e6509caf3"), "test additionalInfo from seed", "test category from seed", new DateTime(2023, 1, 30, 23, 46, 56, 470, DateTimeKind.Utc).AddTicks(1754), "test description from seed", new DateTime(2023, 1, 31, 1, 46, 56, 470, DateTimeKind.Utc).AddTicks(1733), "Yes", "test image url from seed", new DateTime(2023, 1, 30, 23, 46, 56, 470, DateTimeKind.Utc).AddTicks(1755), "Test name from seed", "test place from seed", new DateTime(2023, 1, 30, 23, 46, 56, 470, DateTimeKind.Utc).AddTicks(1728) });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "UserEvents",
                keyColumn: "Id",
                keyValue: new Guid("12ba54ed-980f-41f4-a296-b74e6509caf3"));

            migrationBuilder.AddColumn<string>(
                name: "MonthOfYear",
                table: "RecurrencyRules",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.InsertData(
                table: "UserEvents",
                columns: new[] { "Id", "AdditionalInfo", "Category", "Date", "Description", "EndTime", "HasRecurrency", "ImageUrl", "LastDate", "Name", "Place", "StartTime" },
                values: new object[] { new Guid("906807a9-edc4-4abc-974d-1a30c6e2ea70"), "test additionalInfo from seed", "test category from seed", new DateTime(2023, 1, 30, 0, 7, 31, 772, DateTimeKind.Utc).AddTicks(1037), "test description from seed", new DateTime(2023, 1, 30, 2, 7, 31, 772, DateTimeKind.Utc).AddTicks(1014), "Yes", "test image url from seed", new DateTime(2023, 1, 30, 0, 7, 31, 772, DateTimeKind.Utc).AddTicks(1038), "Test name from seed", "test place from seed", new DateTime(2023, 1, 30, 0, 7, 31, 772, DateTimeKind.Utc).AddTicks(1011) });
        }
    }
}
