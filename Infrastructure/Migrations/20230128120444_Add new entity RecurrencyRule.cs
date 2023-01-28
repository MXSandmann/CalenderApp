using System;
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
                values: new object[] { new Guid("88b921b0-a20d-49b4-83f5-e42298531f61"), "test additionalInfo from seed", "test category from seed", new DateTime(2023, 1, 28, 12, 4, 44, 163, DateTimeKind.Utc).AddTicks(9550), "test description from seed", new DateTime(2023, 1, 28, 14, 4, 44, 163, DateTimeKind.Utc).AddTicks(9525), "Yes", "test image url from seed", new DateTime(2023, 1, 28, 12, 4, 44, 163, DateTimeKind.Utc).AddTicks(9551), "Test name from seed", "test place from seed", new DateTime(2023, 1, 28, 12, 4, 44, 163, DateTimeKind.Utc).AddTicks(9519) });

            migrationBuilder.CreateIndex(
                name: "IX_RecurrencyRules_UserEventId",
                table: "RecurrencyRules",
                column: "UserEventId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RecurrencyRules");

            migrationBuilder.DeleteData(
                table: "UserEvents",
                keyColumn: "Id",
                keyValue: new Guid("88b921b0-a20d-49b4-83f5-e42298531f61"));

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
