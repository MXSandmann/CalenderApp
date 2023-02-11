using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    public partial class AddnewentityRecurrencyRule : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "UserEvents",
                keyColumn: "Id",
                keyValue: new Guid("4d4c5425-48f7-40f3-b98f-15d78c22c622"));

            migrationBuilder.RenameColumn(
                name: "Recurrency",
                table: "UserEvents",
                newName: "HasRecurrency");

            migrationBuilder.CreateTable(
                name: "RecurrencyRules",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    UserEventId = table.Column<Guid>(type: "uuid", nullable: false),
                    Recurrency = table.Column<string>(type: "text", nullable: false),
                    Gap = table.Column<int>(type: "integer", nullable: false),
                    MaximumOccurrencies = table.Column<int>(type: "integer", nullable: false),
                    DayOfWeek = table.Column<string>(type: "text", nullable: false),
                    WeekOfMonth = table.Column<string>(type: "text", nullable: false),
                    MonthOfYear = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RecurrencyRules", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RecurrencyRules_UserEvents_UserEventId",
                        column: x => x.UserEventId,
                        principalTable: "UserEvents",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "UserEvents",
                columns: new[] { "Id", "AdditionalInfo", "Category", "Date", "Description", "EndTime", "HasRecurrency", "ImageUrl", "LastDate", "Name", "Place", "StartTime" },
                values: new object[] { new Guid("da58013a-c5ea-4dd5-a76a-f8d69abf00c9"), "test additionalInfo from seed", "test category from seed", new DateTime(2023, 1, 28, 12, 38, 49, 286, DateTimeKind.Utc).AddTicks(6641), "test description from seed", new DateTime(2023, 1, 28, 14, 38, 49, 286, DateTimeKind.Utc).AddTicks(6612), "Yes", "test image url from seed", new DateTime(2023, 1, 28, 12, 38, 49, 286, DateTimeKind.Utc).AddTicks(6641), "Test name from seed", "test place from seed", new DateTime(2023, 1, 28, 12, 38, 49, 286, DateTimeKind.Utc).AddTicks(6608) });

            migrationBuilder.CreateIndex(
                name: "IX_RecurrencyRules_UserEventId",
                table: "RecurrencyRules",
                column: "UserEventId",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RecurrencyRules");

            migrationBuilder.DeleteData(
                table: "UserEvents",
                keyColumn: "Id",
                keyValue: new Guid("da58013a-c5ea-4dd5-a76a-f8d69abf00c9"));

            migrationBuilder.RenameColumn(
                name: "HasRecurrency",
                table: "UserEvents",
                newName: "Recurrency");

            migrationBuilder.InsertData(
                table: "UserEvents",
                columns: new[] { "Id", "AdditionalInfo", "Category", "Date", "Description", "EndTime", "ImageUrl", "LastDate", "Name", "Place", "Recurrency", "StartTime" },
                values: new object[] { new Guid("4d4c5425-48f7-40f3-b98f-15d78c22c622"), "test additionalInfo from seed", "test category from seed", new DateTime(2023, 1, 19, 0, 40, 30, 215, DateTimeKind.Utc).AddTicks(7291), "test description from seed", new DateTime(2023, 1, 19, 2, 40, 30, 215, DateTimeKind.Utc).AddTicks(7267), "test image url from seed", new DateTime(2023, 1, 19, 0, 40, 30, 215, DateTimeKind.Utc).AddTicks(7291), "Test name from seed", "test place from seed", "Weekly", new DateTime(2023, 1, 19, 0, 40, 30, 215, DateTimeKind.Utc).AddTicks(7265) });
        }
    }
}
