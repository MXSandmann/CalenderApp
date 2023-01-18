using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    public partial class RemoveredundantDateproperty : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "UserEvents",
                keyColumn: "Id",
                keyValue: new Guid("25c00451-9238-43bb-849a-55b0cfc436e4"));

            migrationBuilder.DropColumn(
                name: "Date",
                table: "UserEvents");

            migrationBuilder.InsertData(
                table: "UserEvents",
                columns: new[] { "Id", "AdditionalInfo", "Category", "Description", "ImageUrl", "Name", "Place", "Time" },
                values: new object[] { new Guid("8f45561f-68c7-4868-864e-806ea40b6427"), "test additionalInfo from seed", "test category from seed", "test description from seed", "test image url from seed", "Test name from seed", "test place from seed", new DateTime(2023, 1, 18, 0, 18, 47, 550, DateTimeKind.Utc).AddTicks(731) });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "UserEvents",
                keyColumn: "Id",
                keyValue: new Guid("8f45561f-68c7-4868-864e-806ea40b6427"));

            migrationBuilder.AddColumn<DateTime>(
                name: "Date",
                table: "UserEvents",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.InsertData(
                table: "UserEvents",
                columns: new[] { "Id", "AdditionalInfo", "Category", "Date", "Description", "ImageUrl", "Name", "Place", "Time" },
                values: new object[] { new Guid("25c00451-9238-43bb-849a-55b0cfc436e4"), "test additionalInfo from seed", "test category from seed", new DateTime(2023, 1, 12, 11, 47, 7, 275, DateTimeKind.Utc).AddTicks(9093), "test description from seed", "test image url from seed", "Test name from seed", "test place from seed", new DateTime(2023, 1, 12, 11, 47, 7, 275, DateTimeKind.Utc).AddTicks(9094) });
        }
    }
}
