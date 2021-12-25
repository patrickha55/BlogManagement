using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BlogManagement.Data.Migrations
{
    public partial class AddCommentsRelationshipsAndPostRatingsTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ImageUrl",
                table: "Posts",
                type: "nvarchar(1000)",
                maxLength: 1000,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<long>(
                name: "UserId",
                table: "PostComments",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AlterColumn<long>(
                name: "ParentId",
                table: "Categories",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.CreateTable(
                name: "PostRatings",
                columns: table => new
                {
                    PostId = table.Column<long>(type: "bigint", nullable: false),
                    UserId = table.Column<long>(type: "bigint", nullable: false),
                    Rating = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PostRatings", x => new { x.PostId, x.UserId });
                    table.ForeignKey(
                        name: "FK_PostRatings_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_PostRatings_Posts_PostId",
                        column: x => x.PostId,
                        principalTable: "Posts",
                        principalColumn: "Id");
                });

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 1L,
                column: "ConcurrencyStamp",
                value: "ece65d0a-549e-4636-ae2d-2e60656f685f");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 2L,
                column: "ConcurrencyStamp",
                value: "ea3fb1d6-c59c-4f06-a1aa-ef3b0666a8ed");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 3L,
                column: "ConcurrencyStamp",
                value: "a876258d-d58d-4618-a7ef-670352b18ae5");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 1L,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "b013c30e-4d87-4061-8b3f-009be07e44c6", "AQAAAAEAACcQAAAAEEE0taJnUbOfePvbCzGApXWEj20Ms1cezQbmFMuugIOPwCX+E8ZACXdQAWLmGuP0zQ==", "32661E8D-66BD-42FB-9308-4E38D22E3051" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 2L,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "b2a02f2b-77e8-40bc-a921-9a2fb50f5574", "AQAAAAEAACcQAAAAEDDYl0KqWJdEwJ+48NPAm1b1vsWZSHDXlqRH8kOCpFIP6Hc1J21pJrX6XhRt25WEDw==", "1253A78A-FDBB-404F-B1D6-FD5BA90B72E7" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 3L,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "42194467-edc4-4870-a176-1b0f41e6441c", "AQAAAAEAACcQAAAAEF7iucmOKpAzmpCWsk7MrI5r3TazTG54Iq6qeH8w/7twyl0mRFoXpa5t4IQNdMw6Og==", "CC0F6F9B-03ED-48F1-B618-B2E078009296" });

            migrationBuilder.UpdateData(
                table: "Posts",
                keyColumn: "Id",
                keyValue: 1L,
                column: "ImageUrl",
                value: "images\\which-shape-is-the-best.jpg");

            migrationBuilder.UpdateData(
                table: "Posts",
                keyColumn: "Id",
                keyValue: 2L,
                column: "ImageUrl",
                value: "images\\best-way-to-board-a-plane.jpg");

            migrationBuilder.UpdateData(
                table: "Posts",
                keyColumn: "Id",
                keyValue: 3L,
                column: "ImageUrl",
                value: "images\\hexagon-bee-hive.jpg");

            migrationBuilder.CreateIndex(
                name: "IX_PostComments_UserId",
                table: "PostComments",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_PostRatings_UserId",
                table: "PostRatings",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_PostComments_AspNetUsers_UserId",
                table: "PostComments",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PostComments_AspNetUsers_UserId",
                table: "PostComments");

            migrationBuilder.DropTable(
                name: "PostRatings");

            migrationBuilder.DropIndex(
                name: "IX_PostComments_UserId",
                table: "PostComments");

            migrationBuilder.DropColumn(
                name: "ImageUrl",
                table: "Posts");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "PostComments");

            migrationBuilder.AlterColumn<long>(
                name: "ParentId",
                table: "Categories",
                type: "bigint",
                nullable: false,
                defaultValue: 0L,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 1L,
                column: "ConcurrencyStamp",
                value: "3a2a31dc-bdc0-4d99-ac90-4b47de58808f");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 2L,
                column: "ConcurrencyStamp",
                value: "7669462e-c04d-4d29-9977-26421ec15227");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 3L,
                column: "ConcurrencyStamp",
                value: "b26343d0-753f-4207-bbfc-2113735de996");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 1L,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "b57d4980-3f8a-4dbd-90ee-35551d457304", "AQAAAAEAACcQAAAAEF3oTUvV5q8mZ+Hg4sqmZIR3NO/dQp6sa3mmEIC+hfVmeysfo37jeQeZfI+uP+wfGw==", null });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 2L,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "fb724075-3806-44fa-a955-5fadc4ae513d", "AQAAAAEAACcQAAAAEJpPmybnof67flVPfArZb1DRne83W3yj+2awkm9/e7EHGZMpPbjbxymWUj6M25BUjQ==", null });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 3L,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "fb86eb15-85ae-4213-91ba-6981442f13b2", "AQAAAAEAACcQAAAAEOX4goggOtRrEKf5fSDvZmDmLSnrLXDtrIWLQbv2ZluXZnYhj0Db33KEYr5n6bbLGg==", null });
        }
    }
}
