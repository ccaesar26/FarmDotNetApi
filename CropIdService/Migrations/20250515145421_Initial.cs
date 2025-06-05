using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CropIdService.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "IdEntries",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Longitude = table.Column<double>(type: "double precision", nullable: false),
                    Latitude = table.Column<double>(type: "double precision", nullable: false),
                    FieldName = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    FarmId = table.Column<Guid>(type: "uuid", nullable: false),
                    ImageData = table.Column<byte[]>(type: "bytea", nullable: false),
                    ImageMimeType = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    Datetime = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    IsPlant = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IdEntries", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SuggestionEntries",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Probability = table.Column<double>(type: "double precision", nullable: false),
                    ScientificName = table.Column<string>(type: "text", nullable: false),
                    IdEntryId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SuggestionEntries", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SuggestionEntries_IdEntries_IdEntryId",
                        column: x => x.IdEntryId,
                        principalTable: "IdEntries",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SuggestionEntries_IdEntryId",
                table: "SuggestionEntries",
                column: "IdEntryId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SuggestionEntries");

            migrationBuilder.DropTable(
                name: "IdEntries");
        }
    }
}
