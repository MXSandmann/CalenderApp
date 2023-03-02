using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class SetbooleantypeforHasRecurrency : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "UserEvents",
                keyColumn: "Id",
                keyValue: new Guid("3cca59c6-821b-4cff-8773-1f37efb5527e"));

            migrationBuilder.InsertData(
                table: "UserEvents",
                columns: new[] { "Id", "AdditionalInfo", "Category", "Date", "Description", "EndTime", "HasRecurrency", "ImageUrl", "LastDate", "Name", "Place", "StartTime" },
                values: new object[] { new Guid("4414c8ef-31c0-4163-9ecc-ab2d2bc4832d"), "test additionalInfo from seed", "test category from seed", new DateTime(2023, 3, 1, 23, 44, 36, 271, DateTimeKind.Utc).AddTicks(8885), "test description from seed", new DateTime(2023, 3, 2, 1, 44, 36, 271, DateTimeKind.Utc).AddTicks(8861), true, "test image url from seed", new DateTime(2023, 3, 1, 23, 44, 36, 271, DateTimeKind.Utc).AddTicks(8886), "Test name from seed", "test place from seed", new DateTime(2023, 3, 1, 23, 44, 36, 271, DateTimeKind.Utc).AddTicks(8859) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "UserEvents",
                keyColumn: "Id",
                keyValue: new Guid("4414c8ef-31c0-4163-9ecc-ab2d2bc4832d"));

            migrationBuilder.InsertData(
                table: "UserEvents",
                columns: new[] { "Id", "AdditionalInfo", "Category", "Date", "Description", "EndTime", "HasRecurrency", "ImageUrl", "LastDate", "Name", "Place", "StartTime" },
                values: new object[] { new Guid("3cca59c6-821b-4cff-8773-1f37efb5527e"), "test additionalInfo from seed", "test category from seed", new DateTime(2023, 2, 2, 22, 2, 11, 332, DateTimeKind.Utc).AddTicks(1697), "test description from seed", new DateTime(2023, 2, 3, 0, 2, 11, 332, DateTimeKind.Utc).AddTicks(1669), true, "test image url from seed", new DateTime(2023, 2, 2, 22, 2, 11, 332, DateTimeKind.Utc).AddTicks(1698), "Test name from seed", "test place from seed", new DateTime(2023, 2, 2, 22, 2, 11, 332, DateTimeKind.Utc).AddTicks(1665) });
        }
    }
}
