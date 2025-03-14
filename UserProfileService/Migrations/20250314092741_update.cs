using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UserProfileService.Migrations
{
    /// <inheritdoc />
    public partial class update : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AttributeCategories",
                keyColumn: "Id",
                keyValue: new Guid("fee5d6bf-3a86-4800-a309-d0b0a4af5168"),
                column: "Name",
                value: "Administrative");

            migrationBuilder.CreateIndex(
                name: "IX_ProfileAttributes_Name",
                table: "ProfileAttributes",
                column: "Name",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_ProfileAttributes_Name",
                table: "ProfileAttributes");

            migrationBuilder.UpdateData(
                table: "AttributeCategories",
                keyColumn: "Id",
                keyValue: new Guid("fee5d6bf-3a86-4800-a309-d0b0a4af5168"),
                column: "Name",
                value: "Role");
        }
    }
}
