using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CropIdService.Migrations
{
    /// <inheritdoc />
    public partial class SuggestionType : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Type",
                table: "SuggestionEntries",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Type",
                table: "SuggestionEntries");
        }
    }
}
