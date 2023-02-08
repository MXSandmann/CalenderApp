using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    public partial class UpdateRecurrencyRule : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "UserEvents",
                keyColumn: "Id",
                keyValue: new Guid("e3cbb56c-3137-436c-a73f-dea568a16183"));

            migrationBuilder.DropColumn(
                name: "DayOfWeek",
                table: "RecurrencyRules");

            migrationBuilder.DropColumn(
                name: "Gap",
                table: "RecurrencyRules");

            migrationBuilder.DropColumn(
                name: "MaximumOccurrencies",
                table: "RecurrencyRules");

            migrationBuilder.InsertData(
                table: "UserEvents",
                columns: new[] { "Id", "AdditionalInfo", "Category", "Date", "Description", "EndTime", "HasRecurrency", "ImageUrl", "LastDate", "Name", "Place", "StartTime" },
                values: new object[] { new Guid("14881e12-e2f5-45f1-992b-698525eb628c"), "test additionalInfo from seed", "test category from seed", new DateTime(2023, 1, 29, 22, 13, 7, 572, DateTimeKind.Utc).AddTicks(1143), "test description from seed", new DateTime(2023, 1, 30, 0, 13, 7, 572, DateTimeKind.Utc).AddTicks(1113), "Yes", "test image url from seed", new DateTime(2023, 1, 29, 22, 13, 7, 572, DateTimeKind.Utc).AddTicks(1143), "Test name from seed", "test place from seed", new DateTime(2023, 1, 29, 22, 13, 7, 572, DateTimeKind.Utc).AddTicks(1109) });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "UserEvents",
                keyColumn: "Id",
                keyValue: new Guid("14881e12-e2f5-45f1-992b-698525eb628c"));

            migrationBuilder.AddColumn<string>(
                name: "DayOfWeek",
                table: "RecurrencyRules",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "Gap",
                table: "RecurrencyRules",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "MaximumOccurrencies",
                table: "RecurrencyRules",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.InsertData(
                table: "UserEvents",
                columns: new[] { "Id", "AdditionalInfo", "Category", "Date", "Description", "EndTime", "HasRecurrency", "ImageUrl", "LastDate", "Name", "Place", "StartTime" },
                values: new object[] { new Guid("e3cbb56c-3137-436c-a73f-dea568a16183"), "test additionalInfo from seed", "test category from seed", new DateTime(2023, 1, 29, 20, 43, 54, 539, DateTimeKind.Utc).AddTicks(4064), "test description from seed", new DateTime(2023, 1, 29, 22, 43, 54, 539, DateTimeKind.Utc).AddTicks(4038), "Yes", "test image url from seed", new DateTime(2023, 1, 29, 20, 43, 54, 539, DateTimeKind.Utc).AddTicks(4065), "Test name from seed", "test place from seed", new DateTime(2023, 1, 29, 20, 43, 54, 539, DateTimeKind.Utc).AddTicks(4037) });
        }
    }
}
