using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddInstructorId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "UserEvents",
                keyColumn: "Id",
                keyValue: new Guid("4414c8ef-31c0-4163-9ecc-ab2d2bc4832d"));

            migrationBuilder.AddColumn<Guid>(
                name: "InstructorId",
                table: "UserEvents",
                type: "uuid",
                nullable: true);

            migrationBuilder.InsertData(
                table: "UserEvents",
                columns: new[] { "Id", "AdditionalInfo", "Category", "Date", "Description", "EndTime", "HasRecurrency", "ImageUrl", "InstructorId", "LastDate", "Name", "Place", "StartTime" },
                values: new object[] { new Guid("5b4a1480-55a1-4407-b7da-d6f650c025ae"), "test additionalInfo from seed", "test category from seed", new DateTime(2023, 3, 31, 12, 23, 27, 461, DateTimeKind.Utc).AddTicks(772), "test description from seed", new DateTime(2023, 3, 31, 14, 23, 27, 461, DateTimeKind.Utc).AddTicks(741), true, "test image url from seed", null, new DateTime(2023, 3, 31, 12, 23, 27, 461, DateTimeKind.Utc).AddTicks(773), "Test name from seed", "test place from seed", new DateTime(2023, 3, 31, 12, 23, 27, 461, DateTimeKind.Utc).AddTicks(739) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "UserEvents",
                keyColumn: "Id",
                keyValue: new Guid("5b4a1480-55a1-4407-b7da-d6f650c025ae"));

            migrationBuilder.DropColumn(
                name: "InstructorId",
                table: "UserEvents");

            migrationBuilder.InsertData(
                table: "UserEvents",
                columns: new[] { "Id", "AdditionalInfo", "Category", "Date", "Description", "EndTime", "HasRecurrency", "ImageUrl", "LastDate", "Name", "Place", "StartTime" },
                values: new object[] { new Guid("4414c8ef-31c0-4163-9ecc-ab2d2bc4832d"), "test additionalInfo from seed", "test category from seed", new DateTime(2023, 3, 1, 23, 44, 36, 271, DateTimeKind.Utc).AddTicks(8885), "test description from seed", new DateTime(2023, 3, 2, 1, 44, 36, 271, DateTimeKind.Utc).AddTicks(8861), true, "test image url from seed", new DateTime(2023, 3, 1, 23, 44, 36, 271, DateTimeKind.Utc).AddTicks(8886), "Test name from seed", "test place from seed", new DateTime(2023, 3, 1, 23, 44, 36, 271, DateTimeKind.Utc).AddTicks(8859) });
        }
    }
}
