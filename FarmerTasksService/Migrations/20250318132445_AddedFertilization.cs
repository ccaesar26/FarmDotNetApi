using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FarmerTasksService.Migrations
{
    /// <inheritdoc />
    public partial class AddedFertilization : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "TaskCategories",
                columns: new[] { "Id", "Name" },
                values: new object[] { new Guid("d64372d4-a7de-4b3b-a3c1-ba5edec17bc1"), "Fertilization" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "TaskCategories",
                keyColumn: "Id",
                keyValue: new Guid("d64372d4-a7de-4b3b-a3c1-ba5edec17bc1"));
        }
    }
}
