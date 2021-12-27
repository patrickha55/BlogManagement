using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BlogManagement.Data.Migrations
{
    public partial class AddIdColumnToPostRatingsTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_PostRatings",
                table: "PostRatings");

            migrationBuilder.AddColumn<long>(
                name: "Id",
                table: "PostRatings",
                type: "bigint",
                nullable: false,
                defaultValue: 0L)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddUniqueConstraint(
                name: "AK_PostRatings_PostId_UserId",
                table: "PostRatings",
                columns: new[] { "PostId", "UserId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_PostRatings",
                table: "PostRatings",
                column: "Id");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 1L,
                column: "ConcurrencyStamp",
                value: "ba0fda5d-74ca-4ca7-b969-ea6c7b553e54");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 2L,
                column: "ConcurrencyStamp",
                value: "adcf2bfa-b5fb-4212-bc14-69b9a97570dc");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 3L,
                column: "ConcurrencyStamp",
                value: "2af140e0-715a-40c5-bd4d-e887dbfa0400");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 1L,
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "84dc0810-ae07-4731-b6e1-5f6b8d5db3e5", "AQAAAAEAACcQAAAAEASug34hPNhkBGhX6wBt9AhxwwqgrOjL4aJncWcvFMKJvrIvphDKlOe9MDqQeeLytw==" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 2L,
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "b972cc1d-5c84-4151-a129-c8bb9c61f2c0", "AQAAAAEAACcQAAAAEDgorZyJ5qOFvIJAMqpwfDfY9H3giQLtoU9B0if6GyGwG3nerggt4tf8jP1SA6k6hg==" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 3L,
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "4ba652ef-6d11-4236-b583-4a896fcb043c", "AQAAAAEAACcQAAAAED5MBFO1TuKAQDW7NpjaAg9NnMhBX0T1jsNd5ldHXR4UV6naGkELBZoujvTJs7ISng==" });

            migrationBuilder.UpdateData(
                table: "Posts",
                keyColumn: "Id",
                keyValue: 1L,
                column: "ImageUrl",
                value: "images/which-shape-is-the-best.jpg");

            migrationBuilder.UpdateData(
                table: "Posts",
                keyColumn: "Id",
                keyValue: 2L,
                column: "ImageUrl",
                value: "images/best-way-to-board-a-plane.jpg");

            migrationBuilder.UpdateData(
                table: "Posts",
                keyColumn: "Id",
                keyValue: 3L,
                column: "ImageUrl",
                value: "images/hexagon-bee-hive.jpg");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropUniqueConstraint(
                name: "AK_PostRatings_PostId_UserId",
                table: "PostRatings");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PostRatings",
                table: "PostRatings");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "PostRatings");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PostRatings",
                table: "PostRatings",
                columns: new[] { "PostId", "UserId" });

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
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "b013c30e-4d87-4061-8b3f-009be07e44c6", "AQAAAAEAACcQAAAAEEE0taJnUbOfePvbCzGApXWEj20Ms1cezQbmFMuugIOPwCX+E8ZACXdQAWLmGuP0zQ==" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 2L,
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "b2a02f2b-77e8-40bc-a921-9a2fb50f5574", "AQAAAAEAACcQAAAAEDDYl0KqWJdEwJ+48NPAm1b1vsWZSHDXlqRH8kOCpFIP6Hc1J21pJrX6XhRt25WEDw==" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 3L,
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "42194467-edc4-4870-a176-1b0f41e6441c", "AQAAAAEAACcQAAAAEF7iucmOKpAzmpCWsk7MrI5r3TazTG54Iq6qeH8w/7twyl0mRFoXpa5t4IQNdMw6Og==" });

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
        }
    }
}
