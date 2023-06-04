using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddInvitationIdsToUserEvent : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "UserEvents",
                keyColumn: "Id",
                keyValue: new Guid("4d342bd5-99d9-42c3-b214-649872d6554c"));

            migrationBuilder.AddColumn<string>(
                name: "InvitationIds",
                table: "UserEvents",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.InsertData(
                table: "UserEvents",
                columns: new[] { "Id", "AdditionalInfo", "Category", "Date", "Description", "Done", "EndTime", "HasRecurrency", "ImageUrl", "InstructorId", "InvitationIds", "LastDate", "Name", "Place", "StartTime" },
                values: new object[] { new Guid("a8f34e98-d649-4903-b0ac-e6e4f903f2fc"), "test additionalInfo from seed", "test category from seed", new DateTime(2023, 6, 3, 19, 42, 48, 417, DateTimeKind.Utc).AddTicks(8114), "test description from seed", false, new DateTime(2023, 6, 3, 21, 42, 48, 417, DateTimeKind.Utc).AddTicks(8094), true, "test image url from seed", null, "[]", new DateTime(2023, 6, 3, 19, 42, 48, 417, DateTimeKind.Utc).AddTicks(8115), "Test name from seed", "test place from seed", new DateTime(2023, 6, 3, 19, 42, 48, 417, DateTimeKind.Utc).AddTicks(8091) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "UserEvents",
                keyColumn: "Id",
                keyValue: new Guid("a8f34e98-d649-4903-b0ac-e6e4f903f2fc"));

            migrationBuilder.DropColumn(
                name: "InvitationIds",
                table: "UserEvents");

            migrationBuilder.InsertData(
                table: "UserEvents",
                columns: new[] { "Id", "AdditionalInfo", "Category", "Date", "Description", "Done", "EndTime", "HasRecurrency", "ImageUrl", "InstructorId", "LastDate", "Name", "Place", "StartTime" },
                values: new object[] { new Guid("4d342bd5-99d9-42c3-b214-649872d6554c"), "test additionalInfo from seed", "test category from seed", new DateTime(2023, 4, 4, 21, 17, 28, 368, DateTimeKind.Utc).AddTicks(4188), "test description from seed", false, new DateTime(2023, 4, 4, 23, 17, 28, 368, DateTimeKind.Utc).AddTicks(4162), true, "test image url from seed", null, new DateTime(2023, 4, 4, 21, 17, 28, 368, DateTimeKind.Utc).AddTicks(4188), "Test name from seed", "test place from seed", new DateTime(2023, 4, 4, 21, 17, 28, 368, DateTimeKind.Utc).AddTicks(4160) });
        }
    }
}
