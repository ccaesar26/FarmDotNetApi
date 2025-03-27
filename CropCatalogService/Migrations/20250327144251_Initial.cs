using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CropCatalogService.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CropCatalogEntries",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    BinomialName = table.Column<string>(type: "text", nullable: false),
                    IsPerennial = table.Column<bool>(type: "boolean", nullable: false),
                    DaysToFirstHarvest = table.Column<int>(type: "integer", nullable: true),
                    DaysToLastHarvest = table.Column<int>(type: "integer", nullable: true),
                    MinMonthsToBearFruit = table.Column<int>(type: "integer", nullable: true),
                    MaxMonthsToBearFruit = table.Column<int>(type: "integer", nullable: true),
                    HarvestSeasonStart = table.Column<DateOnly>(type: "date", nullable: true),
                    HarvestSeasonEnd = table.Column<DateOnly>(type: "date", nullable: true),
                    Description = table.Column<string>(type: "text", nullable: true),
                    WikipediaLink = table.Column<string>(type: "text", nullable: true),
                    ImageLink = table.Column<string>(type: "text", nullable: true),
                    SunRequirements = table.Column<string>(type: "text", nullable: true),
                    SowingMethod = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CropCatalogEntries", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CropCatalogEntries");
        }
    }
}
