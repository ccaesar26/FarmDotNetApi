using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ClimateIndicesService.Migrations
{
    /// <inheritdoc />
    public partial class fghjk : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("Npgsql:PostgresExtension:postgis", ",,")
                .OldAnnotation("Npgsql:PostgresExtension:postgis", ",,")
                .OldAnnotation("Npgsql:PostgresExtension:postgis_raster", ",,");

            migrationBuilder.AlterColumn<byte[]>(
                name: "RasterData",
                table: "DroughtRecords",
                type: "bytea",
                nullable: false,
                oldClrType: typeof(byte[]),
                oldType: "raster");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("Npgsql:PostgresExtension:postgis", ",,")
                .Annotation("Npgsql:PostgresExtension:postgis_raster", ",,")
                .OldAnnotation("Npgsql:PostgresExtension:postgis", ",,");

            migrationBuilder.AlterColumn<byte[]>(
                name: "RasterData",
                table: "DroughtRecords",
                type: "raster",
                nullable: false,
                oldClrType: typeof(byte[]),
                oldType: "bytea");
        }
    }
}
