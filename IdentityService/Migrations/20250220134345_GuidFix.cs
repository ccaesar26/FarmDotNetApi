using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace IdentityService.Migrations
{
    /// <inheritdoc />
    public partial class GuidFix : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("2259c9c5-cb22-459f-89d3-5310856c93f9"));

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("49d4dd5a-05ea-4709-9bd3-3bca33ee9857"));

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("51e0c1cf-1d3e-42ac-a4f9-e1e59e58655a"));

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { new Guid("3f6c5d10-1e23-4f58-a7a3-c5f30c3b6a6d"), "Manager" },
                    { new Guid("4d6d5e20-2a67-491e-91f3-d7f78c1c2e7f"), "Worker" },
                    { new Guid("b1a6dcd6-6c30-4f69-91b2-f19f7d1f9c3a"), "Admin" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("3f6c5d10-1e23-4f58-a7a3-c5f30c3b6a6d"));

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("4d6d5e20-2a67-491e-91f3-d7f78c1c2e7f"));

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("b1a6dcd6-6c30-4f69-91b2-f19f7d1f9c3a"));

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { new Guid("2259c9c5-cb22-459f-89d3-5310856c93f9"), "Worker" },
                    { new Guid("49d4dd5a-05ea-4709-9bd3-3bca33ee9857"), "Admin" },
                    { new Guid("51e0c1cf-1d3e-42ac-a4f9-e1e59e58655a"), "Manager" }
                });
        }
    }
}
