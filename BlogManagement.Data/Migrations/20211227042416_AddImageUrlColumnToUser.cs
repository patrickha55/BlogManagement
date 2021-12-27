using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BlogManagement.Data.Migrations
{
    public partial class AddImageUrlColumnToUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ImageUrl",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 1L,
                column: "ConcurrencyStamp",
                value: "1cd6592e-66d8-4b09-8f66-c6f9908060d2");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 2L,
                column: "ConcurrencyStamp",
                value: "64ac304d-3eac-4249-a8dd-b770c8d0c30b");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 3L,
                column: "ConcurrencyStamp",
                value: "9e527318-fba6-4406-a472-e19a1b82f7d8");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 1L,
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "a8c0c6dc-89fa-4a19-b3f1-d1f4b4f30bbf", "AQAAAAEAACcQAAAAEKvpUU8AFBd/z5awefJ8riHsdl8AFT5dNRvcPJnRHLSfBG0p5NHI/MvKWMug3rikiQ==" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 2L,
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "3f62dbfb-7e18-466e-8055-8613b1f3deff", "AQAAAAEAACcQAAAAEHrTFNJhRg8QpsSL2/amwz00XhFYCZvfWebOL61GNRzLROMAUb5Qa0LSi7ddq+BcZw==" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 3L,
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "7a0771ee-2d90-4be5-87bd-d217d7c071d1", "AQAAAAEAACcQAAAAEB6rYFnMRNYyhMTVhTAw/6cfiX58/s0lG/oiFiE9IzM5kcJSY03C3XpNL2COC3IOvg==" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImageUrl",
                table: "AspNetUsers");

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
        }
    }
}
