using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class MakeInvitationIdsNullable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "UserEvents",
                keyColumn: "Id",
                keyValue: new Guid("a8f34e98-d649-4903-b0ac-e6e4f903f2fc"));

            migrationBuilder.AlterColumn<string>(
                name: "InvitationIds",
                table: "UserEvents",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.InsertData(
                table: "UserEvents",
                columns: new[] { "Id", "AdditionalInfo", "Category", "Date", "Description", "Done", "EndTime", "HasRecurrency", "ImageUrl", "InstructorId", "InvitationIds", "LastDate", "Name", "Place", "StartTime" },
                values: new object[] { new Guid("8d9c033c-9035-46fa-bfb1-9892e98ea9c6"), "test additionalInfo from seed", "test category from seed", new DateTime(2023, 6, 4, 21, 3, 35, 140, DateTimeKind.Utc).AddTicks(8775), "test description from seed", false, new DateTime(2023, 6, 4, 23, 3, 35, 140, DateTimeKind.Utc).AddTicks(8746), true, "test image url from seed", null, "[]", new DateTime(2023, 6, 4, 21, 3, 35, 140, DateTimeKind.Utc).AddTicks(8775), "Test name from seed", "test place from seed", new DateTime(2023, 6, 4, 21, 3, 35, 140, DateTimeKind.Utc).AddTicks(8743) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "UserEvents",
                keyColumn: "Id",
                keyValue: new Guid("8d9c033c-9035-46fa-bfb1-9892e98ea9c6"));

            migrationBuilder.AlterColumn<string>(
                name: "InvitationIds",
                table: "UserEvents",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.InsertData(
                table: "UserEvents",
                columns: new[] { "Id", "AdditionalInfo", "Category", "Date", "Description", "Done", "EndTime", "HasRecurrency", "ImageUrl", "InstructorId", "InvitationIds", "LastDate", "Name", "Place", "StartTime" },
                values: new object[] { new Guid("a8f34e98-d649-4903-b0ac-e6e4f903f2fc"), "test additionalInfo from seed", "test category from seed", new DateTime(2023, 6, 3, 19, 42, 48, 417, DateTimeKind.Utc).AddTicks(8114), "test description from seed", false, new DateTime(2023, 6, 3, 21, 42, 48, 417, DateTimeKind.Utc).AddTicks(8094), true, "test image url from seed", null, "[]", new DateTime(2023, 6, 3, 19, 42, 48, 417, DateTimeKind.Utc).AddTicks(8115), "Test name from seed", "test place from seed", new DateTime(2023, 6, 3, 19, 42, 48, 417, DateTimeKind.Utc).AddTicks(8091) });
        }
    }
}
