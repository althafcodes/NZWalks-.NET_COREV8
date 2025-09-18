using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace NZWalks.Migrations
{
    /// <inheritdoc />
    public partial class seedingdifficultiesandregions : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Difficulties",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { new Guid("3acb3af2-a6ee-4c29-8a0a-b481ee03a574"), "Easy" },
                    { new Guid("8fb0a33c-ae5d-44fa-80a2-564e319b672c"), "Medium" },
                    { new Guid("9ff5d3d5-9d2a-47a3-8c51-abc5034f3121"), "Hard" }
                });

            migrationBuilder.InsertData(
                table: "Regions",
                columns: new[] { "ID", "Code", "Name", "RegionImageURL" },
                values: new object[,]
                {
                    { new Guid("0a9b8c7d-6e5f-4d3c-b2a1-9f8e7d6c4004"), "WKO", "Waikato", "https://example.com/images/waikato.png" },
                    { new Guid("12345678-90ab-cdef-1234-567890abcdef"), "OTA", "Otago", "https://example.com/images/otago.png" },
                    { new Guid("3f5e2c2b-7f8a-4f1e-9b5f-91c2b184e001"), "AKL", "Auckland", "https://example.com/images/auckland.png" },
                    { new Guid("7c8b9a0d-2e3f-4c6a-b7d9-23a7b681e002"), "WLG", "Wellington", "https://example.com/images/wellington.png" },
                    { new Guid("9a1c2d3e-4f5a-678b-9c0d-1e2f3a4b5003"), "CAN", "Canterbury", "https://example.com/images/canterbury.png" },
                    { new Guid("abcdef12-3456-7890-abcd-ef1234567890"), "BOP", "Bay of Plenty", "https://example.com/images/bayofplenty.png" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Difficulties",
                keyColumn: "Id",
                keyValue: new Guid("3acb3af2-a6ee-4c29-8a0a-b481ee03a574"));

            migrationBuilder.DeleteData(
                table: "Difficulties",
                keyColumn: "Id",
                keyValue: new Guid("8fb0a33c-ae5d-44fa-80a2-564e319b672c"));

            migrationBuilder.DeleteData(
                table: "Difficulties",
                keyColumn: "Id",
                keyValue: new Guid("9ff5d3d5-9d2a-47a3-8c51-abc5034f3121"));

            migrationBuilder.DeleteData(
                table: "Regions",
                keyColumn: "ID",
                keyValue: new Guid("0a9b8c7d-6e5f-4d3c-b2a1-9f8e7d6c4004"));

            migrationBuilder.DeleteData(
                table: "Regions",
                keyColumn: "ID",
                keyValue: new Guid("12345678-90ab-cdef-1234-567890abcdef"));

            migrationBuilder.DeleteData(
                table: "Regions",
                keyColumn: "ID",
                keyValue: new Guid("3f5e2c2b-7f8a-4f1e-9b5f-91c2b184e001"));

            migrationBuilder.DeleteData(
                table: "Regions",
                keyColumn: "ID",
                keyValue: new Guid("7c8b9a0d-2e3f-4c6a-b7d9-23a7b681e002"));

            migrationBuilder.DeleteData(
                table: "Regions",
                keyColumn: "ID",
                keyValue: new Guid("9a1c2d3e-4f5a-678b-9c0d-1e2f3a4b5003"));

            migrationBuilder.DeleteData(
                table: "Regions",
                keyColumn: "ID",
                keyValue: new Guid("abcdef12-3456-7890-abcd-ef1234567890"));
        }
    }
}
