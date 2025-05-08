using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ReportsService.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Reports",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Title = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    Description = table.Column<string>(type: "character varying(2000)", maxLength: 2000, nullable: true),
                    ImageUrl = table.Column<string>(type: "text", nullable: false),
                    Status = table.Column<int>(type: "integer", nullable: false),
                    FieldId = table.Column<Guid>(type: "uuid", nullable: true),
                    FarmId = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedByUserId = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reports", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ReportComments",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    ReportId = table.Column<Guid>(type: "uuid", nullable: false),
                    ParentCommentId = table.Column<Guid>(type: "uuid", nullable: true),
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    CommentText = table.Column<string>(type: "character varying(1000)", maxLength: 1000, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReportComments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ReportComments_ReportComments_ParentCommentId",
                        column: x => x.ParentCommentId,
                        principalTable: "ReportComments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ReportComments_Reports_ReportId",
                        column: x => x.ReportId,
                        principalTable: "Reports",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ReportComments_ParentCommentId",
                table: "ReportComments",
                column: "ParentCommentId");

            migrationBuilder.CreateIndex(
                name: "IX_ReportComments_ReportId",
                table: "ReportComments",
                column: "ReportId");

            migrationBuilder.CreateIndex(
                name: "IX_ReportComments_UserId",
                table: "ReportComments",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Reports_CreatedByUserId",
                table: "Reports",
                column: "CreatedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Reports_FarmId",
                table: "Reports",
                column: "FarmId");

            migrationBuilder.CreateIndex(
                name: "IX_Reports_FieldId",
                table: "Reports",
                column: "FieldId");

            migrationBuilder.CreateIndex(
                name: "IX_Reports_Status",
                table: "Reports",
                column: "Status");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ReportComments");

            migrationBuilder.DropTable(
                name: "Reports");
        }
    }
}
