using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddpropertyDone : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "UserEvents",
                keyColumn: "Id",
                keyValue: new Guid("5b4a1480-55a1-4407-b7da-d6f650c025ae"));

            migrationBuilder.AddColumn<bool>(
                name: "Done",
                table: "UserEvents",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.InsertData(
                table: "UserEvents",
                columns: new[] { "Id", "AdditionalInfo", "Category", "Date", "Description", "Done", "EndTime", "HasRecurrency", "ImageUrl", "InstructorId", "LastDate", "Name", "Place", "StartTime" },
                values: new object[] { new Guid("4d342bd5-99d9-42c3-b214-649872d6554c"), "test additionalInfo from seed", "test category from seed", new DateTime(2023, 4, 4, 21, 17, 28, 368, DateTimeKind.Utc).AddTicks(4188), "test description from seed", false, new DateTime(2023, 4, 4, 23, 17, 28, 368, DateTimeKind.Utc).AddTicks(4162), true, "test image url from seed", null, new DateTime(2023, 4, 4, 21, 17, 28, 368, DateTimeKind.Utc).AddTicks(4188), "Test name from seed", "test place from seed", new DateTime(2023, 4, 4, 21, 17, 28, 368, DateTimeKind.Utc).AddTicks(4160) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "UserEvents",
                keyColumn: "Id",
                keyValue: new Guid("4d342bd5-99d9-42c3-b214-649872d6554c"));

            migrationBuilder.DropColumn(
                name: "Done",
                table: "UserEvents");

            migrationBuilder.InsertData(
                table: "UserEvents",
                columns: new[] { "Id", "AdditionalInfo", "Category", "Date", "Description", "EndTime", "HasRecurrency", "ImageUrl", "InstructorId", "LastDate", "Name", "Place", "StartTime" },
                values: new object[] { new Guid("5b4a1480-55a1-4407-b7da-d6f650c025ae"), "test additionalInfo from seed", "test category from seed", new DateTime(2023, 3, 31, 12, 23, 27, 461, DateTimeKind.Utc).AddTicks(772), "test description from seed", new DateTime(2023, 3, 31, 14, 23, 27, 461, DateTimeKind.Utc).AddTicks(741), true, "test image url from seed", null, new DateTime(2023, 3, 31, 12, 23, 27, 461, DateTimeKind.Utc).AddTicks(773), "Test name from seed", "test place from seed", new DateTime(2023, 3, 31, 12, 23, 27, 461, DateTimeKind.Utc).AddTicks(739) });
        }
    }
}
