using System;
using Microsoft.EntityFrameworkCore.Migrations;
using NetTopologySuite.Geometries;

#nullable disable

namespace PlantedCropsService.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("Npgsql:PostgresExtension:postgis", ",,");

            migrationBuilder.CreateTable(
                name: "Crops",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    BinomialName = table.Column<string>(type: "text", nullable: false),
                    CultivatedVariety = table.Column<string>(type: "text", nullable: true),
                    ImageLink = table.Column<string>(type: "text", nullable: true),
                    Perennial = table.Column<bool>(type: "boolean", nullable: false),
                    ExpectedFirstHarvestDate = table.Column<DateOnly>(type: "date", nullable: false),
                    ExpectedLastHarvestDate = table.Column<DateOnly>(type: "date", nullable: false),
                    FieldId = table.Column<Guid>(type: "uuid", nullable: false),
                    Surface = table.Column<Polygon>(type: "geometry", nullable: true),
                    Area = table.Column<double>(type: "double precision", nullable: false),
                    CropCatalogId = table.Column<Guid>(type: "uuid", nullable: false),
                    FarmId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Crops", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Notes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Text = table.Column<string>(type: "text", nullable: false),
                    Timestamp = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    AuthorUserId = table.Column<Guid>(type: "uuid", nullable: false),
                    AuthorUserName = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Notes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "FertilizerEvents",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    CropId = table.Column<Guid>(type: "uuid", nullable: false),
                    FertilizerType = table.Column<string>(type: "text", nullable: false),
                    ApplicationDate = table.Column<DateOnly>(type: "date", nullable: false),
                    QuantityApplied = table.Column<double>(type: "double precision", nullable: false),
                    Units = table.Column<string>(type: "text", nullable: false),
                    ApplicationMethod = table.Column<string>(type: "text", nullable: true),
                    EquipmentUsed = table.Column<string>(type: "text", nullable: true),
                    AppliedByUserId = table.Column<Guid>(type: "uuid", nullable: false),
                    AppliedByUserName = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FertilizerEvents", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FertilizerEvents_Crops_CropId",
                        column: x => x.CropId,
                        principalTable: "Crops",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "GrowthStageEvents",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    CropId = table.Column<Guid>(type: "uuid", nullable: false),
                    Stage = table.Column<string>(type: "text", nullable: false),
                    Timestamp = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    RecordedByUserId = table.Column<Guid>(type: "uuid", nullable: false),
                    RecordedByUserName = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GrowthStageEvents", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GrowthStageEvents_Crops_CropId",
                        column: x => x.CropId,
                        principalTable: "Crops",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "HealthStatusEvents",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    CropId = table.Column<Guid>(type: "uuid", nullable: false),
                    HealthStatus = table.Column<string>(type: "text", nullable: false),
                    Timestamp = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    RecordedByUserId = table.Column<Guid>(type: "uuid", nullable: false),
                    RecordedByUserName = table.Column<string>(type: "text", nullable: false),
                    PestOrDisease = table.Column<string>(type: "text", nullable: true),
                    SeverityLevel = table.Column<string>(type: "text", nullable: false),
                    TreatmentApplied = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HealthStatusEvents", x => x.Id);
                    table.ForeignKey(
                        name: "FK_HealthStatusEvents_Crops_CropId",
                        column: x => x.CropId,
                        principalTable: "Crops",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "FertilizerEventNote",
                columns: table => new
                {
                    FertilizerEventsId = table.Column<Guid>(type: "uuid", nullable: false),
                    NotesId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FertilizerEventNote", x => new { x.FertilizerEventsId, x.NotesId });
                    table.ForeignKey(
                        name: "FK_FertilizerEventNote_FertilizerEvents_FertilizerEventsId",
                        column: x => x.FertilizerEventsId,
                        principalTable: "FertilizerEvents",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FertilizerEventNote_Notes_NotesId",
                        column: x => x.NotesId,
                        principalTable: "Notes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "GrowthStageEventNote",
                columns: table => new
                {
                    GrowthStageEventsId = table.Column<Guid>(type: "uuid", nullable: false),
                    NotesId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GrowthStageEventNote", x => new { x.GrowthStageEventsId, x.NotesId });
                    table.ForeignKey(
                        name: "FK_GrowthStageEventNote_GrowthStageEvents_GrowthStageEventsId",
                        column: x => x.GrowthStageEventsId,
                        principalTable: "GrowthStageEvents",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GrowthStageEventNote_Notes_NotesId",
                        column: x => x.NotesId,
                        principalTable: "Notes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "HealthStatusEventNote",
                columns: table => new
                {
                    HealthStatusEventsId = table.Column<Guid>(type: "uuid", nullable: false),
                    NotesId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HealthStatusEventNote", x => new { x.HealthStatusEventsId, x.NotesId });
                    table.ForeignKey(
                        name: "FK_HealthStatusEventNote_HealthStatusEvents_HealthStatusEvents~",
                        column: x => x.HealthStatusEventsId,
                        principalTable: "HealthStatusEvents",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_HealthStatusEventNote_Notes_NotesId",
                        column: x => x.NotesId,
                        principalTable: "Notes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_FertilizerEventNote_NotesId",
                table: "FertilizerEventNote",
                column: "NotesId");

            migrationBuilder.CreateIndex(
                name: "IX_FertilizerEvents_CropId",
                table: "FertilizerEvents",
                column: "CropId");

            migrationBuilder.CreateIndex(
                name: "IX_GrowthStageEventNote_NotesId",
                table: "GrowthStageEventNote",
                column: "NotesId");

            migrationBuilder.CreateIndex(
                name: "IX_GrowthStageEvents_CropId",
                table: "GrowthStageEvents",
                column: "CropId");

            migrationBuilder.CreateIndex(
                name: "IX_HealthStatusEventNote_NotesId",
                table: "HealthStatusEventNote",
                column: "NotesId");

            migrationBuilder.CreateIndex(
                name: "IX_HealthStatusEvents_CropId",
                table: "HealthStatusEvents",
                column: "CropId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FertilizerEventNote");

            migrationBuilder.DropTable(
                name: "GrowthStageEventNote");

            migrationBuilder.DropTable(
                name: "HealthStatusEventNote");

            migrationBuilder.DropTable(
                name: "FertilizerEvents");

            migrationBuilder.DropTable(
                name: "GrowthStageEvents");

            migrationBuilder.DropTable(
                name: "HealthStatusEvents");

            migrationBuilder.DropTable(
                name: "Notes");

            migrationBuilder.DropTable(
                name: "Crops");
        }
    }
}
