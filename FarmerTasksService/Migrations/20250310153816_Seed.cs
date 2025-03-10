using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace FarmerTasksService.Migrations
{
    /// <inheritdoc />
    public partial class Seed : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "TaskCategories",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { new Guid("6d57eb9f-c3d0-4fbe-8f35-a7fb5c906b91"), "Maintenance" },
                    { new Guid("b01abce0-1604-483b-8c7d-ffc8de5c459e"), "Pest and Disease Control" },
                    { new Guid("ba84a42e-b766-4778-bbae-94429ffcd66c"), "Planting" },
                    { new Guid("d332a3b8-af4d-460e-8c35-a6acfd2d71c8"), "Irrigation" },
                    { new Guid("e41ec2cb-6567-434c-b079-7d5d13e8d194"), "Harvesting" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "TaskCategories",
                keyColumn: "Id",
                keyValue: new Guid("6d57eb9f-c3d0-4fbe-8f35-a7fb5c906b91"));

            migrationBuilder.DeleteData(
                table: "TaskCategories",
                keyColumn: "Id",
                keyValue: new Guid("b01abce0-1604-483b-8c7d-ffc8de5c459e"));

            migrationBuilder.DeleteData(
                table: "TaskCategories",
                keyColumn: "Id",
                keyValue: new Guid("ba84a42e-b766-4778-bbae-94429ffcd66c"));

            migrationBuilder.DeleteData(
                table: "TaskCategories",
                keyColumn: "Id",
                keyValue: new Guid("d332a3b8-af4d-460e-8c35-a6acfd2d71c8"));

            migrationBuilder.DeleteData(
                table: "TaskCategories",
                keyColumn: "Id",
                keyValue: new Guid("e41ec2cb-6567-434c-b079-7d5d13e8d194"));
        }
    }
}
