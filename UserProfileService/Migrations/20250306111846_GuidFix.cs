using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace UserProfileService.Migrations
{
    /// <inheritdoc />
    public partial class GuidFix : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AttributeCategories",
                keyColumn: "Id",
                keyValue: new Guid("aa9487ef-7e74-467a-9e15-068db3529f02"));

            migrationBuilder.DeleteData(
                table: "ProfileAttributes",
                keyColumn: "Id",
                keyValue: new Guid("0f2badb3-d2d1-47f2-a9ca-c77eece69fe8"));

            migrationBuilder.DeleteData(
                table: "ProfileAttributes",
                keyColumn: "Id",
                keyValue: new Guid("1fed148c-5cbb-412d-a6db-51423db450b5"));

            migrationBuilder.DeleteData(
                table: "ProfileAttributes",
                keyColumn: "Id",
                keyValue: new Guid("2a94340d-13c9-4eae-98b1-1edcee38a468"));

            migrationBuilder.DeleteData(
                table: "ProfileAttributes",
                keyColumn: "Id",
                keyValue: new Guid("32e8183f-90b2-4334-86c3-4d27c7b75b6f"));

            migrationBuilder.DeleteData(
                table: "ProfileAttributes",
                keyColumn: "Id",
                keyValue: new Guid("35e11364-0de4-4840-b63c-095323e6de59"));

            migrationBuilder.DeleteData(
                table: "ProfileAttributes",
                keyColumn: "Id",
                keyValue: new Guid("48f672f4-43c9-4bc5-ae98-d98b070cf8fa"));

            migrationBuilder.DeleteData(
                table: "ProfileAttributes",
                keyColumn: "Id",
                keyValue: new Guid("934182f1-6e90-46e0-bb58-2ae0e46c1c0a"));

            migrationBuilder.DeleteData(
                table: "ProfileAttributes",
                keyColumn: "Id",
                keyValue: new Guid("97d19c0d-cd03-43ca-ab57-63d70a18019c"));

            migrationBuilder.DeleteData(
                table: "ProfileAttributes",
                keyColumn: "Id",
                keyValue: new Guid("98444a92-b679-44dc-91ba-7a69d0f561a2"));

            migrationBuilder.DeleteData(
                table: "ProfileAttributes",
                keyColumn: "Id",
                keyValue: new Guid("a91a5b91-cddb-44a1-a538-5568db16d8e5"));

            migrationBuilder.DeleteData(
                table: "ProfileAttributes",
                keyColumn: "Id",
                keyValue: new Guid("bc589e44-3c9a-436d-ac77-836f67ee80b3"));

            migrationBuilder.DeleteData(
                table: "ProfileAttributes",
                keyColumn: "Id",
                keyValue: new Guid("c2e5fb01-897d-4f17-b064-b6290663fef1"));

            migrationBuilder.DeleteData(
                table: "ProfileAttributes",
                keyColumn: "Id",
                keyValue: new Guid("c7330d5e-5471-4b39-a907-9bd3c4bcc23e"));

            migrationBuilder.DeleteData(
                table: "ProfileAttributes",
                keyColumn: "Id",
                keyValue: new Guid("c87a191b-dd53-473b-9e62-5557dfc8b133"));

            migrationBuilder.DeleteData(
                table: "ProfileAttributes",
                keyColumn: "Id",
                keyValue: new Guid("ce81221b-95b6-41ec-8a3d-37c43d9aacb2"));

            migrationBuilder.DeleteData(
                table: "ProfileAttributes",
                keyColumn: "Id",
                keyValue: new Guid("e82d6839-fbe1-4c32-8653-147caac1989b"));

            migrationBuilder.DeleteData(
                table: "ProfileAttributes",
                keyColumn: "Id",
                keyValue: new Guid("ed65cd35-f17c-4a47-bd55-aeba19a47d86"));

            migrationBuilder.DeleteData(
                table: "AttributeCategories",
                keyColumn: "Id",
                keyValue: new Guid("4f957bf9-5829-4cb8-8b6f-e78fae5d1b9c"));

            migrationBuilder.DeleteData(
                table: "AttributeCategories",
                keyColumn: "Id",
                keyValue: new Guid("e41c22a3-7d8d-4d25-b0b2-3b23a5eebe9b"));

            migrationBuilder.DeleteData(
                table: "AttributeCategories",
                keyColumn: "Id",
                keyValue: new Guid("f343ab87-eb1b-42fd-b11a-982ac0832fc0"));

            migrationBuilder.DeleteData(
                table: "AttributeCategories",
                keyColumn: "Id",
                keyValue: new Guid("f6f8f617-1f24-475d-8366-b6476cc00ee9"));

            migrationBuilder.InsertData(
                table: "AttributeCategories",
                columns: new[] { "Id", "IsPredefined", "Name" },
                values: new object[,]
                {
                    { new Guid("04c71678-4205-4a37-817b-92d05dc9fd72"), true, "Equipment Specialization" },
                    { new Guid("a1ef0f83-97a7-49c5-b0bd-2f6fbd6cd102"), true, "General Farm Labor" },
                    { new Guid("da69270b-6390-428f-92ed-b077f512d31f"), false, "Custom" },
                    { new Guid("e7aa259c-8b80-4a74-a99e-78d42f330a7c"), true, "Crop-Specific Roles" },
                    { new Guid("fee5d6bf-3a86-4800-a309-d0b0a4af5168"), true, "Role" }
                });

            migrationBuilder.InsertData(
                table: "ProfileAttributes",
                columns: new[] { "Id", "CategoryId", "IsPredefined", "Name" },
                values: new object[,]
                {
                    { new Guid("08312aa3-68b0-42a5-9522-aeebb5b4d85d"), new Guid("04c71678-4205-4a37-817b-92d05dc9fd72"), true, "Combine Operator" },
                    { new Guid("093cefca-3ec1-4f52-a39c-62dedbc82a61"), new Guid("e7aa259c-8b80-4a74-a99e-78d42f330a7c"), true, "Scout" },
                    { new Guid("19467a7f-2707-42be-8f7d-a179014936db"), new Guid("a1ef0f83-97a7-49c5-b0bd-2f6fbd6cd102"), true, "Livestock Handler" },
                    { new Guid("1c4e6180-9edb-4683-9afc-a0631dac381d"), new Guid("e7aa259c-8b80-4a74-a99e-78d42f330a7c"), true, "Planter" },
                    { new Guid("2366a79e-5332-4b68-897e-e7de174fa69c"), new Guid("a1ef0f83-97a7-49c5-b0bd-2f6fbd6cd102"), true, "Equipment Operator" },
                    { new Guid("255c4fb4-76ed-4234-8ff5-9e7116224a40"), new Guid("e7aa259c-8b80-4a74-a99e-78d42f330a7c"), true, "Weed Control Specialist" },
                    { new Guid("3c4b4a67-d9f4-4b10-bc52-8be1f5de2643"), new Guid("a1ef0f83-97a7-49c5-b0bd-2f6fbd6cd102"), true, "Irrigation Technician" },
                    { new Guid("3fa29a42-7763-4c0d-b391-458de8e79a33"), new Guid("fee5d6bf-3a86-4800-a309-d0b0a4af5168"), true, "Manager" },
                    { new Guid("59a1672f-0c87-4ec5-b52e-1ec09065c7ee"), new Guid("a1ef0f83-97a7-49c5-b0bd-2f6fbd6cd102"), true, "General Farmhand/Laborer" },
                    { new Guid("5d85c8f3-774e-47f7-8b4b-5334f1a3722f"), new Guid("e7aa259c-8b80-4a74-a99e-78d42f330a7c"), true, "Greenhouse Worker" },
                    { new Guid("911c9119-6570-4070-9a0d-9a5301b9773b"), new Guid("e7aa259c-8b80-4a74-a99e-78d42f330a7c"), true, "Crop Specialist / Agronomist Assistant" },
                    { new Guid("a8a06283-6dd4-400d-8fd4-3800122d4856"), new Guid("e7aa259c-8b80-4a74-a99e-78d42f330a7c"), true, "Pest Control Specialist" },
                    { new Guid("b00c6b53-65cd-43d5-a9a6-83149923a0ee"), new Guid("04c71678-4205-4a37-817b-92d05dc9fd72"), true, "Sprayer Operator" },
                    { new Guid("bb96978e-f161-4b91-98d8-eb7098727d77"), new Guid("04c71678-4205-4a37-817b-92d05dc9fd72"), true, "Tractor Operator" },
                    { new Guid("bd820d62-3e3c-4fad-9ad5-187fb055d9bf"), new Guid("a1ef0f83-97a7-49c5-b0bd-2f6fbd6cd102"), true, "Maintenance Worker" },
                    { new Guid("f5596fd6-ae03-4927-8a13-68e012a37d9d"), new Guid("a1ef0f83-97a7-49c5-b0bd-2f6fbd6cd102"), true, "Field Worker" },
                    { new Guid("fab9ea3e-1589-45cb-98c8-9ed5c4148a38"), new Guid("e7aa259c-8b80-4a74-a99e-78d42f330a7c"), true, "Harvester" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AttributeCategories",
                keyColumn: "Id",
                keyValue: new Guid("da69270b-6390-428f-92ed-b077f512d31f"));

            migrationBuilder.DeleteData(
                table: "ProfileAttributes",
                keyColumn: "Id",
                keyValue: new Guid("08312aa3-68b0-42a5-9522-aeebb5b4d85d"));

            migrationBuilder.DeleteData(
                table: "ProfileAttributes",
                keyColumn: "Id",
                keyValue: new Guid("093cefca-3ec1-4f52-a39c-62dedbc82a61"));

            migrationBuilder.DeleteData(
                table: "ProfileAttributes",
                keyColumn: "Id",
                keyValue: new Guid("19467a7f-2707-42be-8f7d-a179014936db"));

            migrationBuilder.DeleteData(
                table: "ProfileAttributes",
                keyColumn: "Id",
                keyValue: new Guid("1c4e6180-9edb-4683-9afc-a0631dac381d"));

            migrationBuilder.DeleteData(
                table: "ProfileAttributes",
                keyColumn: "Id",
                keyValue: new Guid("2366a79e-5332-4b68-897e-e7de174fa69c"));

            migrationBuilder.DeleteData(
                table: "ProfileAttributes",
                keyColumn: "Id",
                keyValue: new Guid("255c4fb4-76ed-4234-8ff5-9e7116224a40"));

            migrationBuilder.DeleteData(
                table: "ProfileAttributes",
                keyColumn: "Id",
                keyValue: new Guid("3c4b4a67-d9f4-4b10-bc52-8be1f5de2643"));

            migrationBuilder.DeleteData(
                table: "ProfileAttributes",
                keyColumn: "Id",
                keyValue: new Guid("3fa29a42-7763-4c0d-b391-458de8e79a33"));

            migrationBuilder.DeleteData(
                table: "ProfileAttributes",
                keyColumn: "Id",
                keyValue: new Guid("59a1672f-0c87-4ec5-b52e-1ec09065c7ee"));

            migrationBuilder.DeleteData(
                table: "ProfileAttributes",
                keyColumn: "Id",
                keyValue: new Guid("5d85c8f3-774e-47f7-8b4b-5334f1a3722f"));

            migrationBuilder.DeleteData(
                table: "ProfileAttributes",
                keyColumn: "Id",
                keyValue: new Guid("911c9119-6570-4070-9a0d-9a5301b9773b"));

            migrationBuilder.DeleteData(
                table: "ProfileAttributes",
                keyColumn: "Id",
                keyValue: new Guid("a8a06283-6dd4-400d-8fd4-3800122d4856"));

            migrationBuilder.DeleteData(
                table: "ProfileAttributes",
                keyColumn: "Id",
                keyValue: new Guid("b00c6b53-65cd-43d5-a9a6-83149923a0ee"));

            migrationBuilder.DeleteData(
                table: "ProfileAttributes",
                keyColumn: "Id",
                keyValue: new Guid("bb96978e-f161-4b91-98d8-eb7098727d77"));

            migrationBuilder.DeleteData(
                table: "ProfileAttributes",
                keyColumn: "Id",
                keyValue: new Guid("bd820d62-3e3c-4fad-9ad5-187fb055d9bf"));

            migrationBuilder.DeleteData(
                table: "ProfileAttributes",
                keyColumn: "Id",
                keyValue: new Guid("f5596fd6-ae03-4927-8a13-68e012a37d9d"));

            migrationBuilder.DeleteData(
                table: "ProfileAttributes",
                keyColumn: "Id",
                keyValue: new Guid("fab9ea3e-1589-45cb-98c8-9ed5c4148a38"));

            migrationBuilder.DeleteData(
                table: "AttributeCategories",
                keyColumn: "Id",
                keyValue: new Guid("04c71678-4205-4a37-817b-92d05dc9fd72"));

            migrationBuilder.DeleteData(
                table: "AttributeCategories",
                keyColumn: "Id",
                keyValue: new Guid("a1ef0f83-97a7-49c5-b0bd-2f6fbd6cd102"));

            migrationBuilder.DeleteData(
                table: "AttributeCategories",
                keyColumn: "Id",
                keyValue: new Guid("e7aa259c-8b80-4a74-a99e-78d42f330a7c"));

            migrationBuilder.DeleteData(
                table: "AttributeCategories",
                keyColumn: "Id",
                keyValue: new Guid("fee5d6bf-3a86-4800-a309-d0b0a4af5168"));

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
        }
    }
}
