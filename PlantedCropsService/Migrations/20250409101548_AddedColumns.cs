using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PlantedCropsService.Migrations
{
    /// <inheritdoc />
    public partial class AddedColumns : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateOnly>(
                name: "ExpectedLastHarvestDate",
                table: "Crops",
                type: "date",
                nullable: true,
                oldClrType: typeof(DateOnly),
                oldType: "date");

            migrationBuilder.AlterColumn<DateOnly>(
                name: "ExpectedFirstHarvestDate",
                table: "Crops",
                type: "date",
                nullable: true,
                oldClrType: typeof(DateOnly),
                oldType: "date");

            migrationBuilder.AddColumn<DateOnly>(
                name: "ExpectedFirstHarvestEndDate",
                table: "Crops",
                type: "date",
                nullable: true);

            migrationBuilder.AddColumn<DateOnly>(
                name: "ExpectedFirstHarvestStartDate",
                table: "Crops",
                type: "date",
                nullable: true);

            migrationBuilder.AddColumn<DateOnly>(
                name: "ExpectedLastHarvestEndDate",
                table: "Crops",
                type: "date",
                nullable: true);

            migrationBuilder.AddColumn<DateOnly>(
                name: "ExpectedLastHarvestStartDate",
                table: "Crops",
                type: "date",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ExpectedFirstHarvestEndDate",
                table: "Crops");

            migrationBuilder.DropColumn(
                name: "ExpectedFirstHarvestStartDate",
                table: "Crops");

            migrationBuilder.DropColumn(
                name: "ExpectedLastHarvestEndDate",
                table: "Crops");

            migrationBuilder.DropColumn(
                name: "ExpectedLastHarvestStartDate",
                table: "Crops");

            migrationBuilder.AlterColumn<DateOnly>(
                name: "ExpectedLastHarvestDate",
                table: "Crops",
                type: "date",
                nullable: false,
                defaultValue: new DateOnly(1, 1, 1),
                oldClrType: typeof(DateOnly),
                oldType: "date",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateOnly>(
                name: "ExpectedFirstHarvestDate",
                table: "Crops",
                type: "date",
                nullable: false,
                defaultValue: new DateOnly(1, 1, 1),
                oldClrType: typeof(DateOnly),
                oldType: "date",
                oldNullable: true);
        }
    }
}
