using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace UserProfileService.Migrations
{
    /// <inheritdoc />
    public partial class Added_Attributes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AttributeCategories",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    IsPredefined = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AttributeCategories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ProfileAttributes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    CategoryId = table.Column<Guid>(type: "uuid", nullable: false),
                    IsPredefined = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProfileAttributes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProfileAttributes_AttributeCategories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "AttributeCategories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserProfileProfileAttribute",
                columns: table => new
                {
                    ProfileAttributesId = table.Column<Guid>(type: "uuid", nullable: false),
                    UserProfilesId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserProfileProfileAttribute", x => new { x.ProfileAttributesId, x.UserProfilesId });
                    table.ForeignKey(
                        name: "FK_UserProfileProfileAttribute_ProfileAttributes_ProfileAttrib~",
                        column: x => x.ProfileAttributesId,
                        principalTable: "ProfileAttributes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserProfileProfileAttribute_UserProfiles_UserProfilesId",
                        column: x => x.UserProfilesId,
                        principalTable: "UserProfiles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "AttributeCategories",
                columns: new[] { "Id", "IsPredefined", "Name" },
                values: new object[,]
                {
                    { new Guid("4f957bf9-5829-4cb8-8b6f-e78fae5d1b9c"), true, "Role" },
                    { new Guid("aa9487ef-7e74-467a-9e15-068db3529f02"), false, "Custom" },
                    { new Guid("e41c22a3-7d8d-4d25-b0b2-3b23a5eebe9b"), true, "General Farm Labor" },
                    { new Guid("f343ab87-eb1b-42fd-b11a-982ac0832fc0"), true, "Equipment Specialization" },
                    { new Guid("f6f8f617-1f24-475d-8366-b6476cc00ee9"), true, "Crop-Specific Roles" }
                });

            migrationBuilder.InsertData(
                table: "ProfileAttributes",
                columns: new[] { "Id", "CategoryId", "IsPredefined", "Name" },
                values: new object[,]
                {
                    { new Guid("0f2badb3-d2d1-47f2-a9ca-c77eece69fe8"), new Guid("f343ab87-eb1b-42fd-b11a-982ac0832fc0"), true, "Sprayer Operator" },
                    { new Guid("1fed148c-5cbb-412d-a6db-51423db450b5"), new Guid("f6f8f617-1f24-475d-8366-b6476cc00ee9"), true, "Weed Control Specialist" },
                    { new Guid("2a94340d-13c9-4eae-98b1-1edcee38a468"), new Guid("f6f8f617-1f24-475d-8366-b6476cc00ee9"), true, "Planter" },
                    { new Guid("32e8183f-90b2-4334-86c3-4d27c7b75b6f"), new Guid("e41c22a3-7d8d-4d25-b0b2-3b23a5eebe9b"), true, "Irrigation Technician" },
                    { new Guid("35e11364-0de4-4840-b63c-095323e6de59"), new Guid("f343ab87-eb1b-42fd-b11a-982ac0832fc0"), true, "Combine Operator" },
                    { new Guid("48f672f4-43c9-4bc5-ae98-d98b070cf8fa"), new Guid("f6f8f617-1f24-475d-8366-b6476cc00ee9"), true, "Pest Control Specialist" },
                    { new Guid("934182f1-6e90-46e0-bb58-2ae0e46c1c0a"), new Guid("f6f8f617-1f24-475d-8366-b6476cc00ee9"), true, "Greenhouse Worker" },
                    { new Guid("97d19c0d-cd03-43ca-ab57-63d70a18019c"), new Guid("f343ab87-eb1b-42fd-b11a-982ac0832fc0"), true, "Tractor Operator" },
                    { new Guid("98444a92-b679-44dc-91ba-7a69d0f561a2"), new Guid("e41c22a3-7d8d-4d25-b0b2-3b23a5eebe9b"), true, "Maintenance Worker" },
                    { new Guid("a91a5b91-cddb-44a1-a538-5568db16d8e5"), new Guid("e41c22a3-7d8d-4d25-b0b2-3b23a5eebe9b"), true, "Equipment Operator" },
                    { new Guid("bc589e44-3c9a-436d-ac77-836f67ee80b3"), new Guid("e41c22a3-7d8d-4d25-b0b2-3b23a5eebe9b"), true, "General Farmhand/Laborer" },
                    { new Guid("c2e5fb01-897d-4f17-b064-b6290663fef1"), new Guid("e41c22a3-7d8d-4d25-b0b2-3b23a5eebe9b"), true, "Field Worker" },
                    { new Guid("c7330d5e-5471-4b39-a907-9bd3c4bcc23e"), new Guid("f6f8f617-1f24-475d-8366-b6476cc00ee9"), true, "Harvester" },
                    { new Guid("c87a191b-dd53-473b-9e62-5557dfc8b133"), new Guid("e41c22a3-7d8d-4d25-b0b2-3b23a5eebe9b"), true, "Livestock Handler" },
                    { new Guid("ce81221b-95b6-41ec-8a3d-37c43d9aacb2"), new Guid("f6f8f617-1f24-475d-8366-b6476cc00ee9"), true, "Scout" },
                    { new Guid("e82d6839-fbe1-4c32-8653-147caac1989b"), new Guid("4f957bf9-5829-4cb8-8b6f-e78fae5d1b9c"), true, "Manager" },
                    { new Guid("ed65cd35-f17c-4a47-bd55-aeba19a47d86"), new Guid("f6f8f617-1f24-475d-8366-b6476cc00ee9"), true, "Crop Specialist / Agronomist Assistant" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_ProfileAttributes_CategoryId",
                table: "ProfileAttributes",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_UserProfileProfileAttribute_UserProfilesId",
                table: "UserProfileProfileAttribute",
                column: "UserProfilesId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserProfileProfileAttribute");

            migrationBuilder.DropTable(
                name: "ProfileAttributes");

            migrationBuilder.DropTable(
                name: "AttributeCategories");
        }
    }
}
