using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BlogManagement.Data.Migrations
{
    public partial class SeedPostsTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Posts",
                columns: new[] { "Id", "AuthorId", "Content", "CreatedAt", "MetaTitle", "ParentId", "PublishedAt", "Slug", "Summary", "Title", "UpdatedAt" },
                values: new object[] { 1L, 1L, null, new DateTime(2021, 12, 18, 0, 0, 0, 0, DateTimeKind.Unspecified), "Best Shape", null, null, "what-shape-is-the-best", "Find out which shape is the best shape.", "What shape is the best?", null });

            migrationBuilder.InsertData(
                table: "Posts",
                columns: new[] { "Id", "AuthorId", "Content", "CreatedAt", "MetaTitle", "ParentId", "PublishedAt", "Slug", "Summary", "Title", "UpdatedAt" },
                values: new object[] { 2L, 2L, null, new DateTime(2021, 12, 18, 0, 0, 0, 0, DateTimeKind.Unspecified), "Best Way To Board A Plane", null, null, "best-way-to-board-a-plane", "Find out which way is the best way to board a plane.", "What is the best way to board a plane?", null });

            migrationBuilder.InsertData(
                table: "Posts",
                columns: new[] { "Id", "AuthorId", "Content", "CreatedAt", "MetaTitle", "ParentId", "PublishedAt", "Slug", "Summary", "Title", "UpdatedAt" },
                values: new object[] { 3L, 3L, null, new DateTime(2021, 12, 18, 0, 0, 0, 0, DateTimeKind.Unspecified), "Hexagon", 1L, null, "hexagon", "Let's talk about hexagon", "The hexagon", null });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Posts",
                keyColumn: "Id",
                keyValue: 2L);

            migrationBuilder.DeleteData(
                table: "Posts",
                keyColumn: "Id",
                keyValue: 3L);

            migrationBuilder.DeleteData(
                table: "Posts",
                keyColumn: "Id",
                keyValue: 1L);
        }
    }
}
