using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ReportsService.Migrations
{
    /// <inheritdoc />
    public partial class AddedImageData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImageUrl",
                table: "Reports");

            migrationBuilder.AddColumn<byte[]>(
                name: "ImageData",
                table: "Reports",
                type: "bytea",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ImageMimeType",
                table: "Reports",
                type: "character varying(100)",
                maxLength: 100,
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImageData",
                table: "Reports");

            migrationBuilder.DropColumn(
                name: "ImageMimeType",
                table: "Reports");

            migrationBuilder.AddColumn<string>(
                name: "ImageUrl",
                table: "Reports",
                type: "text",
                nullable: false,
                defaultValue: "");
        }
    }
}
