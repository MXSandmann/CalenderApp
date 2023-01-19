using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    public partial class Changeconfigurationfortimeproperty : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "UserEvents",
                keyColumn: "Id",
                keyValue: new Guid("bc81b825-7a3e-41fa-bf92-ac2c8c5d9481"));

            migrationBuilder.InsertData(
                table: "UserEvents",
                columns: new[] { "Id", "AdditionalInfo", "Category", "Description", "EndDateTime", "ImageUrl", "Name", "Place", "Recurrency", "StartDateTime" },
                values: new object[] { new Guid("6646226a-2501-4fd0-83f6-7417d404de9b"), "test additionalInfo from seed", "test category from seed", "test description from seed", new DateTime(2023, 1, 19, 1, 55, 25, 27, DateTimeKind.Utc).AddTicks(5855), "test image url from seed", "Test name from seed", "test place from seed", "Weekly", new DateTime(2023, 1, 18, 23, 55, 25, 27, DateTimeKind.Utc).AddTicks(5853) });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "UserEvents",
                keyColumn: "Id",
                keyValue: new Guid("6646226a-2501-4fd0-83f6-7417d404de9b"));

            migrationBuilder.InsertData(
                table: "UserEvents",
                columns: new[] { "Id", "AdditionalInfo", "Category", "Description", "EndDateTime", "ImageUrl", "Name", "Place", "Recurrency", "StartDateTime" },
                values: new object[] { new Guid("bc81b825-7a3e-41fa-bf92-ac2c8c5d9481"), "test additionalInfo from seed", "test category from seed", "test description from seed", new DateTime(2023, 1, 18, 2, 39, 21, 729, DateTimeKind.Utc).AddTicks(9932), "test image url from seed", "Test name from seed", "test place from seed", "Weekly", new DateTime(2023, 1, 18, 0, 39, 21, 729, DateTimeKind.Utc).AddTicks(9928) });
        }
    }
}
