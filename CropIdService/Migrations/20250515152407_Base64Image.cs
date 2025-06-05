using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CropIdService.Migrations
{
    /// <inheritdoc />
    public partial class Base64Image : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImageData",
                table: "IdEntries");

            migrationBuilder.DropColumn(
                name: "ImageMimeType",
                table: "IdEntries");

            migrationBuilder.AddColumn<string>(
                name: "ImageBase64Data",
                table: "IdEntries",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImageBase64Data",
                table: "IdEntries");

            migrationBuilder.AddColumn<byte[]>(
                name: "ImageData",
                table: "IdEntries",
                type: "bytea",
                nullable: false,
                defaultValue: new byte[0]);

            migrationBuilder.AddColumn<string>(
                name: "ImageMimeType",
                table: "IdEntries",
                type: "character varying(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");
        }
    }
}
