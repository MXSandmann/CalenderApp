using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    public partial class ChangedaysoftheweekinRecurrencyRule : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "UserEvents",
                keyColumn: "Id",
                keyValue: new Guid("12ba54ed-980f-41f4-a296-b74e6509caf3"));

            migrationBuilder.DropColumn(
                name: "CertainDays",
                table: "RecurrencyRules");

            migrationBuilder.AlterColumn<bool>(
                name: "HasRecurrency",
                table: "UserEvents",
                type: "boolean",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AddColumn<int>(
                name: "DayOfWeek",
                table: "RecurrencyRules",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.InsertData(
                table: "UserEvents",
                columns: new[] { "Id", "AdditionalInfo", "Category", "Date", "Description", "EndTime", "HasRecurrency", "ImageUrl", "LastDate", "Name", "Place", "StartTime" },
                values: new object[] { new Guid("3cca59c6-821b-4cff-8773-1f37efb5527e"), "test additionalInfo from seed", "test category from seed", new DateTime(2023, 2, 2, 22, 2, 11, 332, DateTimeKind.Utc).AddTicks(1697), "test description from seed", new DateTime(2023, 2, 3, 0, 2, 11, 332, DateTimeKind.Utc).AddTicks(1669), true, "test image url from seed", new DateTime(2023, 2, 2, 22, 2, 11, 332, DateTimeKind.Utc).AddTicks(1698), "Test name from seed", "test place from seed", new DateTime(2023, 2, 2, 22, 2, 11, 332, DateTimeKind.Utc).AddTicks(1665) });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "UserEvents",
                keyColumn: "Id",
                keyValue: new Guid("3cca59c6-821b-4cff-8773-1f37efb5527e"));

            migrationBuilder.DropColumn(
                name: "DayOfWeek",
                table: "RecurrencyRules");

            migrationBuilder.AlterColumn<string>(
                name: "HasRecurrency",
                table: "UserEvents",
                type: "text",
                nullable: false,
                oldClrType: typeof(bool),
                oldType: "boolean");

            migrationBuilder.AddColumn<byte>(
                name: "CertainDays",
                table: "RecurrencyRules",
                type: "smallint",
                nullable: false,
                defaultValue: (byte)0);

            migrationBuilder.InsertData(
                table: "UserEvents",
                columns: new[] { "Id", "AdditionalInfo", "Category", "Date", "Description", "EndTime", "HasRecurrency", "ImageUrl", "LastDate", "Name", "Place", "StartTime" },
                values: new object[] { new Guid("12ba54ed-980f-41f4-a296-b74e6509caf3"), "test additionalInfo from seed", "test category from seed", new DateTime(2023, 1, 30, 23, 46, 56, 470, DateTimeKind.Utc).AddTicks(1754), "test description from seed", new DateTime(2023, 1, 31, 1, 46, 56, 470, DateTimeKind.Utc).AddTicks(1733), "Yes", "test image url from seed", new DateTime(2023, 1, 30, 23, 46, 56, 470, DateTimeKind.Utc).AddTicks(1755), "Test name from seed", "test place from seed", new DateTime(2023, 1, 30, 23, 46, 56, 470, DateTimeKind.Utc).AddTicks(1728) });
        }
    }
}
