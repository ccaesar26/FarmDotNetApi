using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PlantedCropsService.Migrations
{
    /// <inheritdoc />
    public partial class AddedPlantingDate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateOnly>(
                name: "PlantingDate",
                table: "Crops",
                type: "date",
                nullable: false,
                defaultValue: new DateOnly(1, 1, 1));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PlantingDate",
                table: "Crops");
        }
    }
}
